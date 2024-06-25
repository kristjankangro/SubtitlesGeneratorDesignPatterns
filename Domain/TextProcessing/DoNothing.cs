using SubtitlesConverter.Domain;

namespace Domain.TextProcessing
{
    public class DoNothing : ITextProcessor
    {
        public IEnumerable<string> Execute(IEnumerable<string> text)
        {
            return text;
        }
    }
}