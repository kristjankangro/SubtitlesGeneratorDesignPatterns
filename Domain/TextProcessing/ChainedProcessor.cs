using SubtitlesConverter.Domain;

namespace Domain.TextProcessing
{
    public class ChainedProcessor : ITextProcessor
    {
        public ChainedProcessor(ITextProcessor inner, ITextProcessor next)
        {
            Inner = inner;
            Next = next;
        }

        private ITextProcessor Inner { get; }
        private ITextProcessor Next { get; }

        public IEnumerable<string> Execute(IEnumerable<string> text) => Next.Execute(Inner.Execute(text));
    }
}