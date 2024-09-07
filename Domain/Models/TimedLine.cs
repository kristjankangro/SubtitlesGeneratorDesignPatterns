namespace Domain.Models
{
    public class TimedLine
    {
        public string Content { get; }
        public TimeSpan Duration { get; }

        public TimedLine(string content, TimeSpan duration)
        {
            this.Content = content.Trim();
            this.Duration = duration;
        }

        public override string ToString() =>
            $"{this.Duration} --> {this.Content}";
    }
}