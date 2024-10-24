using Domain.TextProcessing.Implementation.Rules;

namespace Domain.TextProcessing.Implementation.Splitters
{
    internal static class RuleComposition
    {
        public static ITwoWaySplitter WithShortestLeft(this ITwoWaySplitter splitter) =>
            new ShortestLeftWins(splitter);

        public static ITwoWaySplitter WithLongestLeft(this ITwoWaySplitter splitter) =>
            new LongestLeftWins(splitter);

        public static ITwoWaySplitter FirstWins(this ITwoWaySplitter rule) =>
            new FirstWins(rule);

        public static IMultiwaySplitter Repeat(this ITwoWaySplitter splitter) =>
            new TwoWayRepeater(splitter);

        public static ITwoWaySplitter Append(this ITwoWaySplitter head, ITwoWaySplitter tail) =>
            new AppendSplitter(head, tail);

        public static ITwoWaySplitter WithLeftNotShorterThan(this ITwoWaySplitter rule, int minLength) =>
            new LeftMinimumLength(rule, minLength);

        public static ITwoWaySplitter WithLeftNotLongerThan(this ITwoWaySplitter rule, int maxLength) =>
            new LeftMaximumLength(rule, maxLength);
    }
}