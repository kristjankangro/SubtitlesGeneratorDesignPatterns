using SubtitlesConverter.Common;

namespace Domain.TextProcessing.Implementation
{
    public class SentenceBreaker : ITextProcessor
    {
        private IEnumerable<ITwoWaySplitter> Rules { get; } = new []
        {
            RegexSplitter.LeftAndRightExtractor(@"^(?<left>[^\?*]+\?)\s*(?<right>.*)$"),
            RegexSplitter.LeftAndRightExtractor(@"^(?<left>[^\!*]+\!)\s*(?<right>.*)$"),
            RegexSplitter.LeftAndRightExtractor(@"^(?<left>(?:(?:\.\.\.)|[^\.])+)\.\s*(?<right>.*)$"),
            RegexSplitter.LeftAndRightExtractor(@"(?<left>^.*\.\.\.)(?=(?:\s+\p{Lu})|(?:\s+\p{Lt})|\s*$)\s*(?<right>.*)$"),
            RegexSplitter.LeftAndRightExtractor(@"^(?<left>.*(?<!\.))\.(?=$)(?<right>)$"),
            RegexSplitter.LeftAndRightExtractor(@"^(?<left>.*)(?:[\:\;\,]|\s+-\s*)(?<right>)$"),
        };

        public IEnumerable<string> Execute(IEnumerable<string> text) =>
            text.SelectMany(this.Break);

        private IEnumerable<string> Break(string text)
        {
            string remaining = text.Trim();
            while (remaining.Length > 0)
            {
                (string extracted, string rest) =
                    this.FindShortestExtractionRule(remaining).First()
                    ;

                yield return extracted;
                remaining = rest;
            }
        }

        private IEnumerable<(string left, string right)> FindShortestExtractionRule(
            string text) =>
                Rules.SelectMany(rule => rule.ApplyTo(text))
                .DefaultIfEmpty((left: text, right: string.Empty))
                .WithMinimumOrEmpty(tuple => tuple.left.Length);
    }
}