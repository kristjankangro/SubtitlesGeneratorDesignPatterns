using System.Text;
using System.Text.RegularExpressions;
using Domain;
using Domain.Models;

namespace Infrastructure.FileSystem;

public class TextFileReader : ITextReader
{
    public IEnumerable<TimedText> Read() => this.ParseSource();

    private FileInfo Source { get; }

    public TextFileReader(FileInfo source) => Source = source;

    private IEnumerable<TimedText> ParseSource()
    {
        if (this.Source is null) yield break;

        IList<string> pendingLines = new List<string>();
        TimeSpan lastKnownTime = TimeSpan.Zero;

        foreach (string line in File.ReadAllLines(this.Source.FullName, Encoding.UTF8))
        {
            if (this.Parse(line) is TimeSpan time)
            {
                if (pendingLines.Any())
                {
                    TimeSpan duration = time - lastKnownTime;
                    yield return new TimedText(pendingLines, lastKnownTime, duration);
                    pendingLines.Clear();
                }
                lastKnownTime = time;
            }
            else
            {
                string trimmed = line.Trim();
                if (trimmed.Length > 0)
                    pendingLines.Add(line);
            }
        }

        if (pendingLines.Any())
            throw new InvalidOperationException("Input text must end in a timestamp.");
    }

    private object Parse(string line)
    {
        Regex timePattern = new Regex(@"^\s*(?<minutes>\d+):(?<seconds>\d+)(?:\.(?<fractional>\d{1,3}))?\s*$");
        Match match = timePattern.Match(line);

        if (!match.Success) return line;

        int minutes = int.Parse(match.Groups["minutes"].Value);
        int seconds = int.Parse(match.Groups["seconds"].Value);
        int milliseconds = match.Groups["fractional"].Success
            ? int.Parse(match.Groups["fractional"].Value.PadRight(3, '0'))
            : 0;

        return new TimeSpan(0, 0, minutes, seconds, milliseconds);
    }
}