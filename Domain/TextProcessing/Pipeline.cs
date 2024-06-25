using System.Collections.Generic;
using System.Linq;
using Domain.TextProcessing;

namespace SubtitlesConverter.Domain.TextProcessing
{
    public class Pipeline : ITextProcessor
    {
        private IEnumerable<ITextProcessor> Processors { get; }
        
        public Pipeline(IEnumerable<ITextProcessor> processors)
        {
            Processors = processors.ToList();
        }
        public Pipeline(params ITextProcessor[] processors)
            : this((IEnumerable<ITextProcessor>)processors){}

        

        public IEnumerable<string> Execute(IEnumerable<string> text)
        {
            return Processors.Aggregate(text, (current, processor) => processor.Execute(current));
        }
    }
}