using Domain.TextProcessing.Implementation.Rules;
using Domain.TextProcessing.Implementation.Splitters;

namespace Domain.TextProcessing.Implementation
{
    public class SentencesBreaker : RuleBasedProcessor
    {
        protected override IMultiwaySplitter Splitter { get; } = 
            RegexSplitter.LeftAndRightExtractor(
                    @"^(?<left>(?:[^\.^\?^\!]|[\.\?\!](?!(?:\s+(?:\p{Lu}|\p{Lt}))|[\.\?\!]))+[\.\?\!])\s+(?<right>.*)$")
                .Append(new PassThroughSplitter())
                .WithShortestLeft()
                .Repeat();
    }
}