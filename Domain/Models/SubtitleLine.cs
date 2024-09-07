namespace Domain.Models;

public class SubtitleLine
{
    public SubtitleLine(TimeSpan beginOffset, TimeSpan endOffset, string content)
    {
        BeginOffset = beginOffset;
        EndOffset = endOffset;
        Content = content ?? string.Empty;
    }

    public TimeSpan BeginOffset { get;  }
    public TimeSpan EndOffset { get;  }
    public string Content { get;  }
}