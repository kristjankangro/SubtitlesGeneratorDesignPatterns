using System.Text.RegularExpressions;
using SubtitlesConverter.Common;

namespace Domain.TextProcessing.Implementation
{
    public class LinesBreaker : ITextProcessor
    {
        private readonly int MaxBreakLength;
        private readonly int MinBreakLenght;
        private IEnumerable<ITwoWaySplitter> Rules { get; }

        public LinesBreaker(int maxBreakLength, int minBreakLenght)
        {
            MaxBreakLength = maxBreakLength;
            MinBreakLenght = minBreakLenght;
            //todo 
            Rules = new[]
            {
                WordBoundarySplitter.AtPunctuation(", ")
                    .Append(WordBoundarySplitter.AtPunctuation("; "))
                    .Append(WordBoundarySplitter.AtPunctuation(" - ")),
                WordBoundarySplitter.BeforeWord(" and ")
                    .Append(WordBoundarySplitter.BeforeWord(" or ")),
                WordBoundarySplitter.BeforeWord(" to ")
                    .Append(WordBoundarySplitter.BeforeWord(" then ")),
                WordBoundarySplitter.AtPunctuation(" ")
            };
        }


        public IEnumerable<string> Execute(
            IEnumerable<string> text) =>
            text.SelectMany(line => this.Break(line));

        private IEnumerable<string> Break(string line)
        {
            string remaining = line;

            while (remaining.Length > 0)
            {
                if (remaining.Length <= MaxBreakLength)
                {
                    yield return remaining;
                    break;
                }

                bool broken = false;
                foreach (IEnumerable<ITwoWaySplitter> rules in this.Rules)
                {
                    IEnumerable<(string left, string right)> split =
                        this.TryBreakLongLine(remaining, rules)
                            .ToList();

                    if (split.Any())
                    {
                        (string left, string right) = split.First();
                        yield return left;
                        remaining = right;
                        broken = true;
                        break;
                    }
                }

                if (!broken)
                {
                    yield return remaining;
                    break;
                }
            }
        }

        private IEnumerable<(string left, string right)> TryBreakLongLine(
            string line,
            IEnumerable<ITwoWaySplitter> rules) =>
            rules.SelectMany(rule => this.Break(line, rule))
                .WithMinimumOrEmpty(split => MaxBreakLength - split.left.Length);

        private IEnumerable<(string left, string right)> Break(string line, ITwoWaySplitter rule) =>
            new Regex(rule.separatorPattern).Matches(line)
                .Select(match => (
                    left: line.Substring(0, match.Index) + rule.appendLeft,
                    right: rule.prependRight + line.Substring(match.Index + match.Length)))
                .Where(split =>
                    MinBreakLenght <= split.left.Length &&
                    split.left.Length <= MaxBreakLength);
    }
}