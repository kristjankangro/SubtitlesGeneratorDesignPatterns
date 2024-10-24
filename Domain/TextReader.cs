using Domain.Models;

namespace Domain;

internal class TextReader : ITextReader
{
    public static ITextReader Empty { get; } = new TextReader();

    private TextReader() { }

    public IEnumerable<TimedText> Read() => Enumerable.Empty<TimedText>();
    
}