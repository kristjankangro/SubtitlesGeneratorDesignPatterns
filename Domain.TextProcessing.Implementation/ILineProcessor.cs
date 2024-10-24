namespace Domain.TextProcessing.Implementation;

internal interface ILineProcessor
{
    IEnumerable<string> Execute(string line);
}