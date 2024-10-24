using Domain.Models;
using Domain.TextProcessing;

namespace Domain;

public class SubtitlesBuilder
{
    private ITextReader Reader { get; set; } = TextReader.Empty;
    private ITextProcessor Processing { get; set; } = new DoNothing();

    public SubtitlesBuilder For(ITextReader reader)
    {
        Reader = reader;
        return this;
    }

    public SubtitlesBuilder Using(ITextProcessor processor)
    {
        Processing = Processing.Then(processor);
        return this;
    }
        
    public Subtitles Build()
    {
        var subs = new Subtitles();

        foreach (var block in Reader.Read())
        {
            TimedText processed = block.Apply(Processing);
            TextDurationMeter durationMeter = new TextDurationMeter(processed);
            IEnumerable<TimedLine> lines = durationMeter.MeasureLines();
            subs.Append(lines, block.Offset);
        }

        return subs;
    }
}