namespace Domain.Models;

public class SubtitlesToSrtWriter : ISubtitlesVisitor
{
    public SubtitlesToSrtWriter(ITextWriter destination)
    {
        Destination = destination;
        LastOrdinal = 0;
    }

    private ITextWriter Destination { get;  } 
    private int LastOrdinal { get; set; }
    public void Visit(SubtitleLine line)
    {
        Destination.AppendLine(
            $"{LastOrdinal + 1}",
            $"{line.BeginOffset:hh\\:mm\\:ss\\,fff} --> {line.EndOffset:hh\\:mm\\:ss\\,fff}",
            $"{line.Content}",
            string.Empty);
       
        throw new NotImplementedException();
    }
}