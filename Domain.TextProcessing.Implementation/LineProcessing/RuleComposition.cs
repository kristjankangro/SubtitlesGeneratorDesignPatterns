namespace Domain.TextProcessing.Implementation.LineProcessing
{
    static class RuleComposition
    {
        public static ILineProcessor Append(this ILineProcessor head, ILineProcessor tail) =>
            new AppendRule(head, tail);

        public static ILineProcessor AppendAll(this IEnumerable<ILineProcessor> processors) =>
            processors.DefaultIfEmpty(new PassThrought())
                .Aggregate((result, current) => result.Append(current));

        public static ITextProcessor AsTextProcessor(this ILineProcessor lineProcessor) =>
            new LineRepeater(lineProcessor);
    }
}