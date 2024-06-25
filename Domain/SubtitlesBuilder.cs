using Domain.Models;
using Domain.TextProcessing;

namespace Domain
{
    public class SubtitlesBuilder
    {
        private TimedText Text { get; set; } = TimedText.Empy;
        private ITextProcessor Processing { get; set; } = new DoNothing();

        public SubtitlesBuilder For(TimedText text)
        {
            Text = text;
            return this;
        }

        public SubtitlesBuilder Using(ITextProcessor processor)
        {
            Processing = Processing.Then(processor);
            return this;
        }
        
        public Subtitles Build()
        {
            // ITextProcessor parsing = new LinesTrimmer()
            //     .Then(new SentenceBreaker())
            //     .Then(new LinesBreaker(95, 45));

            TimedText processed = Text.Apply(Processing);
            TextDurationMeter durationMeter = new TextDurationMeter(processed);
            IEnumerable<SubtitleLine> subtitles = durationMeter.MeasureLines();
            return new Subtitles(subtitles);
        }
    }
}