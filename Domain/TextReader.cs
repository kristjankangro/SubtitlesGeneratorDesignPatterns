using System.Text;
using System.Text.RegularExpressions;
using Domain.Models;

namespace Domain;

internal class TextReader : ITextReader
{
    public static ITextReader Empty { get; } = new TextReader();

    private TextReader()
    {
    }

    public TimedText Read() => TimedText.Empty;
    
}