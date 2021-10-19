using System.Collections.Concurrent;

namespace HackSystem.LRU;

public class LRUContainer<TValue> : LRUContainer<TValue, TValue>
    where TValue : notnull
{
    public LRUContainer(int capacity = 100)
        : base(new Func<TValue, TValue>(value => value), capacity)
    {
    }
}

public class LRUContainer<TKey, TValue>
    where TKey : notnull
{
    protected Func<TValue, TKey> KeySelector { get; init; }
    protected ConcurrentDictionary<TKey, LRUNode<TValue>> Nodes { get; init; } = new();

    public int Capacity { get; init; }
    public int Count { get => this.Nodes.Count; }

    public TValue? HeadValue { get => this.Head is null ? default : this.Head!.Value; }
    public TValue? TailValue { get => this.Tail is null ? default : this.Tail!.Value; }

    protected LRUNode<TValue>? Head { get; set; }
    protected LRUNode<TValue>? Tail { get; set; }

    public LRUContainer(Func<TValue, TKey> keySelector, int capacity = 100)
    {
        this.Capacity = capacity > 0 ? capacity : throw new ArgumentOutOfRangeException($"Capacity should not be or less than 0.");
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

    public IEnumerable<TValue> GetValuesFromHead()
    {
        var node = this.Head;
        while (node != null)
        {
            yield return node.Value;
            node = node.Previous;
        }
    }

    public IEnumerable<TValue> GetValuesFromTail()
    {
        var node = this.Tail;
        while (node != null)
        {
            yield return node.Value;
            node = node.Next;
        }
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

        while (this.Count > this.Capacity)
        {
            this.Remove(this.Tail!.Value);
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

    public bool BringToHead(TValue value)
    {
        var key = this.KeySelector.Invoke(value);
        if (!this.Nodes.TryGetValue(key, out var node))
            throw new KeyNotFoundException($"Not found key of {key}.");

        if (this.Head == node) return false;
        if (this.Tail == node) this.Tail = node.Next;
        node.RemoveSelf();
        this.Head!.SetNext(node);
        this.Head = node;
        return true;
    }
}
