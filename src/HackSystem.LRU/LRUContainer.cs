﻿using System.Collections.Concurrent;

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

    public bool ExistKey(TKey key)
        => this.Nodes.ContainsKey(key);

    public bool ExistValue(TValue value)
    {
        var key = this.KeySelector.Invoke(value);
        return this.Nodes.ContainsKey(key);
    }

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

    public bool Add(TValue value)
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
        return true;
    }

    public bool Remove(TValue value)
    {
        var key = this.KeySelector.Invoke(value);
        if (!this.Nodes.TryRemove(key, out var node))
            throw new KeyNotFoundException($"Not found key of {key}.");

        if (this.Head == node) this.Head = node.Previous;
        if (this.Tail == node) this.Tail = node.Next;

        node.RemoveSelf();
        return true;
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

    public TValue? GetPreviousValue(TValue value)
    {
        var key = this.KeySelector.Invoke(value);
        if (!this.Nodes.TryGetValue(key, out var node))
            throw new KeyNotFoundException($"Not found key of {key}.");
        return node.IsTail ? default : node.Previous!.Value;
    }

    public TValue? GetNextValue(TValue value)
    {
        var key = this.KeySelector.Invoke(value);
        if (!this.Nodes.TryGetValue(key, out var node))
            throw new KeyNotFoundException($"Not found key of {key}.");
        return node.IsHead ? default : node.Next!.Value;
    }

    public void MoveToAfter(TValue value, TValue nextValue)
    {
        var key = this.KeySelector.Invoke(value);
        var nextKey = this.KeySelector.Invoke(nextValue);
        if (!this.Nodes.TryGetValue(key, out var node))
            throw new KeyNotFoundException($"Not found key of {key}.");
        if (!this.Nodes.TryGetValue(nextKey, out var nextNode))
            throw new KeyNotFoundException($"Not found next key of {nextKey}.");

        if (this.Head == node) this.Head = node.Previous;
        if (this.Tail == node) this.Tail = node.Next;
        else if (this.Tail == nextNode) this.Tail = node;
        node.RemoveSelf();
        nextNode.SetPrevious(node);
    }
}
