namespace Domain.TextProcessing.Implementation.Rules;

public class PassThroughSplitter : ITwoWaySplitter
{
    public IEnumerable<(string left, string right)> ApplyTo(string line)
        => [(line, string.Empty)];
}