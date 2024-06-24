using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using SubtitlesConverter.Domain.Models;

namespace SubtitlesConverter.Domain
{
    internal class TextDurationMeter
    {
        private TimedText Text { get; }

        internal TextDurationMeter(TimedText text)
        {
            Text = text;
        }

        public IEnumerable<SubtitleLine> MeasureLines() => MeasureLines(GetFullTextWeight());

        private IEnumerable<SubtitleLine> MeasureLines(double fullTextWeight) =>
            Text.Content
                .Select(line => (
                    line: line,
                    relativeWeight: this.CountReadableLetters(line) / fullTextWeight))
                .Select(tuple => (
                    line: tuple.line,
                    milliseconds: Text.Duration.TotalMilliseconds * tuple.relativeWeight))
                .Select(tuple => new SubtitleLine(tuple.line, TimeSpan.FromMilliseconds(tuple.milliseconds)));
        
        private double GetFullTextWeight() => Text.Content.Sum(CountReadableLetters);
        
        private int CountReadableLetters(string text) =>
            Regex.Matches(text, @"\w+")
                .Sum(match => match.Value.Length);
    }
}