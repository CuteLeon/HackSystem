using System.Collections.Concurrent;

namespace HackSystem.LRU;

public class LRUContainer<TValue> : LRUContainer<TValue, TValue>
    where TValue : notnull
{
    public LRUContainer()
        : base(new Func<TValue, TValue>(value => value))
    {
    }
}

public class LRUContainer<TKey, TValue>
    where TKey : notnull
{
    protected Func<TValue, TKey> KeySelector { get; init; }
    protected ConcurrentDictionary<TKey, LRUNode<TValue>> Nodes { get; init; } = new();

    public TValue? HeadValue { get => this.Head is null ? default : this.Head!.Value; }
    public TValue? TailValue { get => this.Tail is null ? default : this.Tail!.Value; }

    protected LRUNode<TValue>? Head { get; set; }
    protected LRUNode<TValue>? Tail { get; set; }

    public LRUContainer(Func<TValue, TKey> keySelector)
    {
        this.KeySelector = keySelector;
    }

    public void Clear()
    {
        this.Head = null;
        this.Tail = null;
        this.Nodes.Clear();
    }

    public bool Exists(TKey key)
        => this.Nodes.ContainsKey(key);

    public bool TryGetValue(TKey key, out TValue? value)
    {
        var result = this.Nodes.TryGetValue(key, out var node);
        value = result ? node!.Value : default;
        return result;
    }

    public void Add(TValue value)
    {
        var key = this.KeySelector.Invoke(value);
        var node = new LRUNode<TValue>(value);
        if (!this.Nodes.TryAdd(key, node))
            throw new InvalidOperationException($"Failed to add key of {key}");

        if (this.Head is null)
        {
            this.Tail = this.Head = node;
        }
        else
        {
            this.Head.SetNext(node);
            this.Head = node;
        }
    }

    public void Remove(TValue value)
    {
        var key = this.KeySelector.Invoke(value);
        if (!this.Nodes.TryRemove(key, out var node))
            throw new KeyNotFoundException($"Not found key of {key}.");

        if (this.Head == node) this.Head = node.Previous;
        if (this.Tail == node) this.Tail = node.Next;

        node.RemoveSelf();
    }

    public void BringToHead(TValue value)
    {
        var key = this.KeySelector.Invoke(value);
        if (!this.Nodes.TryGetValue(key, out var node))
            throw new KeyNotFoundException($"Not found key of {key}.");

        if (this.Head == node) return;
        node.RemoveSelf();
        this.Head!.SetNext(node);
        this.Head = node;
    }
}
