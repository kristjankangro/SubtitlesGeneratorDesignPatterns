using Domain.TextProcessing.Implementation.LineProcessing;

namespace Domain.TextProcessing.Implementation;

public class SpecialLettersFilter
{
    public static ITextProcessor ReplaceSpecialLetters() =>
        new (string search, string replace)[]
            {
                ("’", "'"),
                ("…", "..."),
                ("“", "\""),
                ("”", "\""),
                ("–", "-"),
            }
            .Select(tuple => new Replace(tuple.search, tuple.replace))
            .AppendAll()
            .AsTextProcessor();
}