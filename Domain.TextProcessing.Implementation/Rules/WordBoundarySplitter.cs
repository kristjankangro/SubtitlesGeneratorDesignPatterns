namespace Domain.TextProcessing.Implementation.Rules;

public class WordBoundarySplitter : ITwoWaySplitter
{
    private string Pattern { get; }
    private string AppendLeft { get; }
    private string PrependRight { get; }
    
    public WordBoundarySplitter(string pattern, string appendLeft, string prependRight)
    {
        Pattern = pattern;
        AppendLeft = appendLeft;
        PrependRight = prependRight;
    }

    public static ITwoWaySplitter AtPunctuation(string pattern)
        => new WordBoundarySplitter(pattern, "...", "... ");
    
    public static ITwoWaySplitter BeforeWord(string pattern)
        => new WordBoundarySplitter(pattern, "...", "..." + pattern);

    public IEnumerable<(string left, string right)> ApplyTo(string line)
        => AllIndexesOfPatternIn(line).Select(index => Split(line, index));

    private IEnumerable<int> AllIndexesOfPatternIn(string line)
    {
        var pos = 0;
        while (pos < line.Length)
        {
            pos = line?.IndexOf(this.Pattern, pos, StringComparison.OrdinalIgnoreCase) ?? -1;
            if (pos < 0) break;
            yield return pos;
            pos += this.Pattern.Length;
        }
    }

    private (string left, string right) Split(string line, int matchPos) =>
    (
        line.Substring(0, matchPos) + this.AppendLeft,
        this.PrependRight + line.Substring(matchPos + this.Pattern.Length)
    );
}