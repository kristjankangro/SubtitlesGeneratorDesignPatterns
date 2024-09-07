using System.Text;

namespace Domain;

public class TextFileWriter : ITextWriter
{
    public TextFileWriter(FileInfo destination)
    {
        Destination = destination;
    }

    private FileInfo Destination { get; }

    public void Write(IEnumerable<string> lines)
    {
        File.WriteAllLines(Destination.FullName, lines, Encoding.UTF8);
    }

    public void AppendLine(params string[] lines)
    {
        File.AppendAllLines(Destination.FullName, lines);
    }
}
