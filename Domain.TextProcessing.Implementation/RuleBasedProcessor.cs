namespace Domain.TextProcessing.Implementation;

public abstract class RuleBasedProcessor : ITextProcessor
{
    protected abstract IMultiwaySplitter Splitter { get; }
    public IEnumerable<string> Execute(IEnumerable<string> text) => 
        text.SelectMany(Splitter.ApplyTo);
}