using System.Text;
using System.Text.RegularExpressions;
using Domain.Models;

namespace Domain;

public class TextFileReader : ITextReader
{
    public TextFileReader(FileInfo source)
    {
        Source = source;
    }

    public TimedText Read() => ParseSource();
    private FileInfo Source { get; }

    private TimedText ParseSource()
    {
        if (Source is null) return TimedText.Empty;
        TimeSpan? initTimespan = null;
        TimeSpan? finalTimespan = null;
        List<string> content = new List<string>();
        bool beginsTimestamp = true;
        bool endsTimestamp = false;

        foreach (string line in File.ReadAllLines(Source.FullName, Encoding.UTF8))
        {
            if (Parse(line) is TimeSpan time)
            {
                initTimespan = initTimespan ?? time;
                finalTimespan = time;
                endsTimestamp = true;
            }
            else
            {
                content.Add(line);
                beginsTimestamp = beginsTimestamp && initTimespan.HasValue;
                endsTimestamp = false;
            }
        }

        if (!beginsTimestamp || !endsTimestamp)
        {
            throw new InvalidOperationException("source file structure is incorrect");
        }

        TimeSpan duration = finalTimespan.Value.Subtract(initTimespan.Value);
        return new TimedText(content, duration);
    }

    private object Parse(string line)
    {
        Regex timePattern = new Regex(@"^\s*(?<minutes>\d+):(?<seconds>\d+)\s*$");
        Match match = timePattern.Match(line);

        if (!match.Success) return line;

        int minutes = int.Parse(match.Groups["minutes"].Value);
        int seconds = int.Parse(match.Groups["seconds"].Value);

        return new TimeSpan(0, minutes, seconds);

    }
}