namespace Domain.TextProcessing.Implementation.LineProcessing
{
    internal class Trim : ILineProcessor
    {
        public IEnumerable<string> Execute(string line) =>
            new[] {line.Trim()}.Where(remainder => remainder.Length > 0);
    }
}