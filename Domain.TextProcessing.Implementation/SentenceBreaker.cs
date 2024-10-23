using Domain.TextProcessing.Implementation.Rules;
using SubtitlesConverter.Domain.TextProcessing.Implementation.Rules;

namespace Domain.TextProcessing.Implementation
{
    public class SentencesBreaker : RuleBasedProcessor
    {
        protected override IMultiwaySplitter Splitter { get; } = 
            RegexSplitter.LeftAndRightExtractor(@"^(?<left>[^\?*]+\?)\s*(?<right>.*)$")
                .Append(RegexSplitter.LeftAndRightExtractor(@"^(?<left>[^\!*]+\!)\s*(?<right>.*)$"))
                .Append(RegexSplitter.LeftAndRightExtractor(@"^(?<left>(?:(?:\.\.\.)|[^\.])+)\.\s*(?<right>.*)$"))
                .Append(RegexSplitter.LeftAndRightExtractor(
                    @"(?<left>^.*\.\.\.)(?=(?:\s+\p{Lu})|(?:\s+\p{Lt})|\s*$)\s*(?<right>.*)$"))
                .Append(RegexSplitter.LeftExtractor(@"^(?<left>.*(?<!\.))\.$"))
                .Append(RegexSplitter.LeftExtractor(@"^(?<left>.*)(?:[\:\;\,]|\s+-\s*)$"))
                .Append(new PassThroughSplitter())
                .WithShortestLeft()
                .Repeat();
    }
}