using SubtitlesConverter.Common;

namespace Domain.TextProcessing.Implementation.Rules
{
    internal class LongestLeftWins : ITwoWaySplitter
    {
        private ITwoWaySplitter Splitter { get; }
        
        public LongestLeftWins(ITwoWaySplitter splitter) => this.Splitter = splitter;

        public IEnumerable<(string left, string right)> ApplyTo(string line)
            => this.Splitter.ApplyTo(line)
                .WithMaximumOrEmpty(tuple => tuple.left.Length);
    }
}
