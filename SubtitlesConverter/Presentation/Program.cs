using System;
using System.IO;
using System.Reflection;
using Domain;
using Domain.Models;
using Domain.TextProcessing.Implementation;
using Infrastructure.FileSystem;
using static Domain.TextProcessing.Implementation.LinesTrimmer;
using static Domain.TextProcessing.Implementation.SpecialLettersFilter;

namespace SubtitlesConverter.Presentation
{
    class Program
    {
        private static string ToolName => Assembly.GetExecutingAssembly().GetName().Name;

        private static string UsageText =>
            $"{ToolName} <source file>.txt <output file>.srt";

        private static void ShowUsage() =>
            Console.WriteLine(UsageText);

        private static bool Verify(string[] args)
            => args.Length == 2
               && File.Exists(args[0]);

        static void Process(FileInfo source, FileInfo destination)
        {
            try
            {
                var subtitles = new SubtitlesBuilder()
                    .For(new TextFileReader(source))
                    .Using(ReplaceSpecialLetters())
                    .Using(RemoveWhiteSpace())
                    .Using(new SentencesBreaker())
                    .Using(RemoveLineEndings())
                    .Using(new LinesBreaker(95, 45))
                    .Build();

                subtitles.Accept(new SubtitlesToSrtWriter(new TextFileWriter(destination)));
                
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing text: {ex.Message}");
            }
        }

        static void Main(string[] args)
        {
            if (Verify(args))
                Process(new FileInfo(args[0]), new FileInfo(args[1]));
            else
                ShowUsage();
        }
    }
}