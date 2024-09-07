namespace Domain.Models;

public interface ISubtitlesVisitor
{
    void Visit(SubtitleLine line);
}