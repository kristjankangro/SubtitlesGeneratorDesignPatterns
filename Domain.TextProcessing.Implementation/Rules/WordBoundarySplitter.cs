namespace Domain.TextProcessing.Implementation;

public class WordBoundarySplitter : ITwoWaySplitter
{
    public WordBoundarySplitter(string pattern, string appendLeft, string prependRight)
    {
        Pattern = pattern;
        AppendLeft = appendLeft;
        PrependRight = prependRight;
    }

    public static ITwoWaySplitter AtPunctuation(string pattern) => new WordBoundarySplitter(pattern, "...", "... ");
    
    public static ITwoWaySplitter BeforeWord(string pattern) => new WordBoundarySplitter(pattern, "...", "..." + pattern);

    public string Pattern { get; }
    public string AppendLeft { get; }
    public string PrependRight { get; }
    public IEnumerable<(string left, string right)> ApplyTo(string line)
    {
        return AllIndexesOfPatternIn(line).Select(index => Split(line, index));
    }
    private IEnumerable<int> AllIndexesOfPatternIn(string line)
    {
        int pos = 0;
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