using System.Text.RegularExpressions;
using SubtitlesConverter.Common;

namespace Domain.TextProcessing.Implementation
{
    public class SentenceBreaker : ITextProcessor
    {
        private IEnumerable<(string pattern, string extract, string remove)> RuleSpecs { get; } = new[]
        {
            (@"^(?<remove>(?<extract>(\.\.\.|[^\.])+)\.)$", "${extract}", "${remove}"),
            (@"^(?<remove>(?<extract>[^\.]+),)$", "${extract}", "${remove}"),
            (@"^(?<remove>(?<extract>(\.\.\.|[^\.])+)\.)[^\.].*$", "${extract}", "${remove}"),
            (@"^(?<remove>(?<extract>[^:]+):).*$", "${extract}", "${remove}"),
            (@"^(?<extract>.+\?).*$", "${extract}", "${extract}"),
            (@"^(?<extract>.+\!).*$", "${extract}", "${extract}"),
        };

        private IEnumerable<ITwoWaySplitter> Rules { get; } = Enumerable.Empty<ITwoWaySplitter>();

        public IEnumerable<string> Execute(IEnumerable<string> text) => 
            text.SelectMany(this.Break);

        private IEnumerable<string> Break(string text)
        {
            string remaining = text.Trim();
            while (remaining.Length > 0)
            {
                (string extracted, string rest) =
                    this.FindShortestExtractionRule(this.RuleSpecs, remaining).First()
                        ;

                yield return extracted;
                remaining = rest;
            }
        }

        private IEnumerable<(string left, string right)> FindShortestExtractionRule(
            IEnumerable<(string pattern, string extractPattern, string removePattern)> rules, 
            string text) =>
            rules
                .Select(rule => (
                    pattern: new Regex(rule.pattern), 
                    extractPattern: rule.extractPattern, 
                    removePattern: rule.removePattern))
                .Select(rule => (
                    pattern: rule.pattern, 
                    match: rule.pattern.Match(text), 
                    extractPattern: rule.extractPattern, 
                    removePattern: rule.removePattern))
                .Where(rule => rule.match.Success)
                .Select(rule => (
                    extracted: rule.pattern.Replace(text, rule.extractPattern), 
                    remove: rule.pattern.Replace(text, rule.removePattern)))
                .Select(tuple => (
                    extracted: tuple.extracted, 
                    removedLength: tuple.remove.Length))
                .Select(tuple => (
                    left: tuple.extracted, 
                    right: text.Substring(tuple.removedLength).Trim()))
                .Concat(Rules.SelectMany(rule => rule.ApplyTo(text)))
                .DefaultIfEmpty((left: text, right: string.Empty))
                .WithMinimumOrEmpty(tuple => tuple.left.Length);
    }

    internal class RegexSplitter : ITwoWaySplitter
    {
        public RegexSplitter(Regex pattern)
        {
            Pattern = pattern;
        }

        private Regex Pattern { get; }
        public IEnumerable<(string left, string right)> ApplyTo(string line)
        {

        
        throw new NotImplementedException();
        }
    }
}
