using Domain.Models;

namespace Domain;

public interface ITextReader
{
    IEnumerable<TimedText> Read();
}