using System.Text.RegularExpressions;

namespace Domain.TextProcessing.Implementation;

internal class RegexSplitter : ITwoWaySplitter
{
    private Regex Pattern { get; }
    private Func<Match, string> ExtractLeft { get; }
    private Func<Match, string> ExtractRight { get; }

    public RegexSplitter(Regex pattern, Func<Match, string> extractLeft, Func<Match, string> extractRight)
    {
        Pattern = pattern;
        ExtractLeft = extractLeft;
        ExtractRight = extractRight;
    }

    public static ITwoWaySplitter LeftAndRightExtractor(string pattern) =>
        new RegexSplitter(new Regex(pattern),
            match => match.Groups["left"].Value,
            match => match.Groups["right"].Value
        );

    public IEnumerable<(string left, string right)> ApplyTo(string line) => 
        Pattern.Matches(line).Select(match => (ExtractLeft(match), ExtractRight(match)));
}