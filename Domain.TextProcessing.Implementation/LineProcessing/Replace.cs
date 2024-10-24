namespace Domain.TextProcessing.Implementation.LineProcessing;

class Replace : ILineProcessor
{
    private string SearchText { get; }
    private string ReplaceWith { get; }

    public Replace(string searchText, string replaceWith)
    {
        this.SearchText = searchText;
        this.ReplaceWith = replaceWith;
    }

    public IEnumerable<string> Execute(string line)
    {
        yield return line.Replace(this.SearchText, this.ReplaceWith);
    }
}