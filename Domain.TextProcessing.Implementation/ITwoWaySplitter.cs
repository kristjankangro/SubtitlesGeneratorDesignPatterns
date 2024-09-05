namespace Domain.TextProcessing.Implementation;

public interface ITwoWaySplitter
{
    IEnumerable<(string left, string right)> ApplyTo(string line);
}