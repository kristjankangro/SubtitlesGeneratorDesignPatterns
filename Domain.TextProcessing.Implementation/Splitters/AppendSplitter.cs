namespace Domain.TextProcessing.Implementation.Rules;

internal class AppendSplitter : ITwoWaySplitter
{
    public AppendSplitter(ITwoWaySplitter head, ITwoWaySplitter tail) 
        => (Head, Tail ) = (head, tail);

    public ITwoWaySplitter Head { get; }
    public ITwoWaySplitter Tail { get; }

    public IEnumerable<(string left, string right)> ApplyTo(string line)
        => Head.ApplyTo(line).Concat(Tail.ApplyTo(line));
}