using Domain.TextProcessing.Implementation.Rules;
using Domain.TextProcessing.Implementation.Splitters;

namespace Domain.TextProcessing.Implementation
{
    public class LinesBreaker : RuleBasedProcessor
    {
        protected override IMultiwaySplitter Splitter { get; }

        public LinesBreaker(int maxLineLength, int minLengthToBreakInto)
        {
            this.Splitter =
                new PassThroughSplitter()
                    .WithLeftNotLongerThan(maxLineLength)
                    .Append(
                        WordBoundarySplitter.AtPunctuation(", ")
                            .Append(WordBoundarySplitter.AtPunctuation("; "))
                            .Append(WordBoundarySplitter.AtPunctuation(" - "))
                            .WithLeftNotLongerThan(maxLineLength)
                            .WithLeftNotShorterThan(minLengthToBreakInto)
                            .WithLongestLeft())
                    .Append(
                        WordBoundarySplitter.BeforeWord(" and ")
                            .Append(WordBoundarySplitter.BeforeWord(" or "))
                            .WithLeftNotLongerThan(maxLineLength)
                            .WithLeftNotShorterThan(minLengthToBreakInto)
                            .WithLongestLeft())
                    .Append(
                        WordBoundarySplitter.BeforeWord(" to ")
                            .Append(WordBoundarySplitter.BeforeWord(" then "))
                            .WithLeftNotLongerThan(maxLineLength)
                            .WithLeftNotShorterThan(minLengthToBreakInto)
                            .WithLongestLeft())
                    .Append(
                        WordBoundarySplitter.AtPunctuation(" ")
                            .WithLeftNotLongerThan(maxLineLength)
                            .WithLeftNotShorterThan(minLengthToBreakInto)
                            .WithLongestLeft())
                    .FirstWins()
                    .Repeat();
        }
    }
}