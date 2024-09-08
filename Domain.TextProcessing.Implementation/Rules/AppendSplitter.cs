namespace Domain.TextProcessing.Implementation;

internal class AppendSplitter : ITwoWaySplitter
{
    public AppendSplitter(ITwoWaySplitter head, ITwoWaySplitter tail)
    {
        Head = head;
        Tail = tail;
    }

    public ITwoWaySplitter Head { get; }
    public ITwoWaySplitter Tail { get; }

    public IEnumerable<(string left, string right)> ApplyTo(string line)
    {
        return Head.ApplyTo(line).Concat(Tail.ApplyTo(line));
    }
}