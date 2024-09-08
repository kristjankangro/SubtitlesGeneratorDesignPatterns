namespace Domain.TextProcessing.Implementation;

internal static class RuleComposition
{
    public static ITwoWaySplitter WithShortestLeft(this IEnumerable<ITwoWaySplitter> splitters) =>
        new ShortestLeftWins(splitters);

    public static IMultiwaySplitter Repeat(this ITwoWaySplitter splitter) => new TwoWayRepeater(splitter);
    
    public static ITwoWaySplitter Append(this ITwoWaySplitter head, ITwoWaySplitter tail) => new AppendSplitter(head, tail);
    public static ITwoWaySplitter WithLeftMaxLengthOf(this ITwoWaySplitter rule, int length) => new LeftMaximumLength(rule, length);
}