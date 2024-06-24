using System.Collections.Generic;

namespace SubtitlesConverter.Domain
{
    public interface ITextProcessor
    {
        IEnumerable<string> Execute(IEnumerable<string> text);
    }
}