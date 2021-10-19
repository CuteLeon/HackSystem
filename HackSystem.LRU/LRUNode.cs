namespace HackSystem.LRU;

public class LRUNode<TValue>
{
    public LRUNode(TValue value)
    {
        this.Value = value;
    }

    public TValue Value { get; init; }

    public bool IsHead { get => this.Next is null; }

    public bool IsTail { get => this.Previous is null; }

    public LRUNode<TValue>? Previous { get; protected set; }

    public LRUNode<TValue>? Next { get; protected set; }

    public void SetPrevious(LRUNode<TValue> previous)
    {
        if (this.IsTail)
        {
            this.Previous = previous;
            previous.Next = this;
        }
        else
        {
            this.Previous!.Next = previous;
            previous.Previous = this.Previous;
            previous.Next = this;
            this.Previous = previous;
        }
    }

    public LRUNode<TValue>? SetNext(LRUNode<TValue> next)
    {
        if (this.IsHead)
        {
            this.Next = next;
            next.Previous = this;
        }
        else
        {
            this.Next!.Previous = next;
            next.Next = this.Next;
            next.Previous = this;
            this.Next = next;
        }

        return this;
    }

    public void RemoveSelf()
    {
        if (this.IsHead && this.IsTail) return;

        if (!this.IsHead) this.Next!.Previous = this.Previous;
        if (!this.IsTail) this.Previous!.Next = this.Next;

        this.Next = null;
        this.Previous = null;
    }

    public void RemovePrevious()
    {
        if (this.IsTail) return;

        var previous = this.Previous!;
        this.Previous = previous.Previous;
        if (this.Previous is not null) this.Previous.Next = this;
        previous.Previous = null;
        previous.Next = null;
    }

    public void RemoveNext()
    {
        if (this.IsHead) return;

        var next = this.Next!;
        this.Next = next.Next;
        if (this.Next is not null) this.Next.Previous = this;
        next.Previous = null;
        next.Next = null;
    }

    public override string ToString()
        => this.Value?.ToString() ?? String.Empty;
}
