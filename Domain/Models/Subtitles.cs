namespace Domain.Models
{
    public class Subtitles
    {
        private IList<SubtitleLine> Lines { get; }

        public void Append(IEnumerable<TimedLine> lines, TimeSpan offset)
        {
            var begin = offset;
            foreach (var line in lines)
            {
                var end = begin + line.Duration;
                this.Lines.Add(new SubtitleLine(begin, end, line.Content));
                begin = end;
            }
        }
        
        public void Accept(ISubtitlesVisitor visitor)
        {
            foreach (var line in Lines)
            {
                visitor.Visit(line);
            }
        }

    }
}