namespace Domain.TextProcessing.Implementation.LineProcessing;

internal class AppendRule : ILineProcessor
{
    private ILineProcessor Head { get; }
    private ILineProcessor Tail { get; }

    public AppendRule(ILineProcessor head, ILineProcessor tail)
    {
        this.Head = head;
        this.Tail = tail;
    }

    public IEnumerable<string> Execute(string line) =>
        this.Head.Execute(line)
            .SelectMany(this.Tail.Execute);
}