namespace Domain;

public interface ITextWriter
{
    void Write(IEnumerable<string> lines);
    void AppendLine(params string[] lines);
}