using System;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using Domain;
using Domain.Models;
using Domain.TextProcessing.Implementation;
using SubtitlesConverter.Domain.TextProcessing.Implementation;

namespace SubtitlesConverter.Presentation
{
    class Program
    {
        private static string ToolName => Assembly.GetExecutingAssembly().GetName().Name;

        private static string UsageText =>
            $"{ToolName} <source file>.txt <output file>.srt <clip duration: [hh:]mm:ss[.fff]>";

        static void ShowUsage() =>
            Console.WriteLine(UsageText);

        static bool Verify(string[] args) =>
            args.Length == 3 &&
            File.Exists(args[0]) && 
            Regex.IsMatch(args[2], @"\d+:[0-5][0-9]:[0-5][0-9](\.\d+)?");

        static void Process(FileInfo source, FileInfo destination, TimeSpan clipDuration)
        {
            try
            {
                string[] text = File.ReadAllLines(source.FullName);
                TimedText timed = new TimedText(text, clipDuration);
                Subtitles subtitles = new SubtitlesBuilder().For(timed)
                    .Using(new LinesTrimmer())
                    .Using(new SentenceBreaker())
                    .Using(new LinesBreaker(95,45))
                    .Build();
                subtitles.SaveAsSrt(destination);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing text: {ex.Message}");
            }
        }

        static void Main(string[] args)
        {
            if (Verify(args))
                Process(new FileInfo(args[0]), new FileInfo(args[1]), TimeSpan.Parse(args[2]));
            else
                ShowUsage();
        }
    }
}
