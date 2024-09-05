namespace Domain.TextProcessing.Implementation;

public class TwoWayRepeater : IMultiwaySplitter
{
    public TwoWayRepeater(ITwoWaySplitter splitter)
    {
        Splitter = splitter;
    }

    private ITwoWaySplitter Splitter { get; }

    public IEnumerable<string> ApplyTo(string textLine)
    {
        string remaining = textLine.Trim();
        while (remaining.Length > 0)
        {
            (string extracted, string rest) =
                this.Splitter.ApplyTo(remaining).First();

            yield return extracted;
            remaining = rest;
        }
    }
}