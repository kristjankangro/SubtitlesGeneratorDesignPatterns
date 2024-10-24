namespace Domain.TextProcessing.Implementation.LineProcessing;

internal class LineRepeater : ITextProcessor
{
    private ILineProcessor LineProcessor { get; }
     
    public LineRepeater(ILineProcessor lineProcessor) => LineProcessor = lineProcessor;

    public IEnumerable<string> Execute(IEnumerable<string> text) =>
        text.SelectMany(this.LineProcessor.Execute);
}