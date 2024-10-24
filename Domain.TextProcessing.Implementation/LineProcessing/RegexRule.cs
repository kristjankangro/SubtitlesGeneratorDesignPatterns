using System.Text.RegularExpressions;

namespace Domain.TextProcessing.Implementation.LineProcessing;

internal class RegexRule : ILineProcessor
{
    private Regex Pattern { get; }
    private Func<Match, string> ExtractionRule { get; }

    private RegexRule(Regex pattern, Func<Match, string> extractionRule)
    {
        this.Pattern = pattern;
        this.ExtractionRule = extractionRule;
    }

    public static ILineProcessor LineExtractor(string pattern) =>
        new RegexRule(new Regex(pattern), match => match.Groups["line"].Value);

    public IEnumerable<string> Execute(string line) =>
        this.Pattern.Matches(line)
            .Where(match => match.Success)
            .Select(this.ExtractionRule)
            .DefaultIfEmpty(line);
}