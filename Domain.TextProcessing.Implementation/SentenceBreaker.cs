namespace Domain.TextProcessing.Implementation;

public class SentenceBreaker : RuleBasedProcessor
{
    protected override IMultiwaySplitter Splitter { get; } = new []
        {
            RegexSplitter.LeftAndRightExtractor(@"^(?<left>[^\?*]+\?)\s*(?<right>.*)$"),
            RegexSplitter.LeftAndRightExtractor(@"^(?<left>[^\!*]+\!)\s*(?<right>.*)$"),
            RegexSplitter.LeftAndRightExtractor(@"^(?<left>(?:(?:\.\.\.)|[^\.])+)\.\s*(?<right>.*)$"),
            RegexSplitter.LeftAndRightExtractor(@"(?<left>^.*\.\.\.)(?=(?:\s+\p{Lu})|(?:\s+\p{Lt})|\s*$)\s*(?<right>.*)$"),
            RegexSplitter.LeftExtractor(@"^(?<left>.*(?<!\.))\.(?=$)(?<right>)$"),
            RegexSplitter.LeftExtractor(@"^(?<left>.*)(?:[\:\;\,]|\s+-\s*)(?<right>)$"),
        }.WithShortestLeft()
        .Repeat();
}