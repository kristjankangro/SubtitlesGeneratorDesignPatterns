namespace Domain.TextProcessing.Implementation;

public class LeftMaximumLength(ITwoWaySplitter rule, int length) : ITwoWaySplitter
{
    private ITwoWaySplitter Rule { get; } = rule;
    private int Length { get; } = length;

    public IEnumerable<(string left, string right)> ApplyTo(string line) => 
        Rule.ApplyTo(line).Where(tuple => tuple.left.Length < Length);
}