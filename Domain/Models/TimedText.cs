using Domain.TextProcessing;

namespace Domain.Models
{
    public class TimedText
    {
        public static TimedText Empty { get; } = new TimedText(Enumerable.Empty<string>(), TimeSpan.Zero);
        public IEnumerable<string> Content { get; }
        public TimeSpan Duration { get;  }
        public TimedText(IEnumerable<string> content, TimeSpan duration)
        {
            Content = content.ToList();
            Duration = duration;
        }

        public TimedText Apply(ITextProcessor processor) => 
            new TimedText(processor.Execute(this.Content), this.Duration);


    }
}