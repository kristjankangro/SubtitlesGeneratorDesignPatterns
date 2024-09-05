namespace Domain.TextProcessing.Implementation;

internal static class RuleComposition
{
    public static ITwoWaySplitter WithShortestLeft(this IEnumerable<ITwoWaySplitter> splitters) =>
        new ShortestLeftWins(splitters);

    public static IMultiwaySplitter Repeat(this ITwoWaySplitter splitter) =>
        new TwoWayRepeater(splitter);
}