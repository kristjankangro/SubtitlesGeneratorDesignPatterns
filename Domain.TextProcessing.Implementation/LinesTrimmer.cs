using Domain.TextProcessing.Implementation.LineProcessing;

namespace Domain.TextProcessing.Implementation;

public class LinesTrimmer
{
    public static ITextProcessor RemoveWhiteSpace()
        => new Trim().AsTextProcessor();

    public static ITextProcessor RemoveLineEndings()
        => new Trim()
            .Append(RegexRule.LineExtractor(@"^(?<line>.*)\.(?<!\.\.)$"))
            .Append(RegexRule.LineExtractor(@"^(?<line>.*)(?:[\:\;\,]| -)$"))
            .AsTextProcessor();
}