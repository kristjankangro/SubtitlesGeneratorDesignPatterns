using Domain.TextProcessing;

namespace Domain.Models
{
    public class TimedText
    {
        public IEnumerable<string> Content { get; }
        public TimeSpan Offset { get;  }
        public TimeSpan Duration { get;  }
        
        public TimedText(IEnumerable<string> content, TimeSpan offset, TimeSpan duration)
            => (Content, Offset, Duration) = (content.ToList(), offset, duration);

        public static TimedText Empty { get; } = 
            new TimedText(Enumerable.Empty<string>(), TimeSpan.Zero, TimeSpan.Zero);
        
        public TimedText Apply(ITextProcessor processor)
            => new TimedText(processor.Execute(Content), Offset, Duration);


    }
}