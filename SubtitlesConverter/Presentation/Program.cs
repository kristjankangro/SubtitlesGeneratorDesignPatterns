﻿using System;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using SubtitlesConverter.Domain;

namespace TextToSrt.Presentation
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
                Subtitles subtitles = Subtitles.Parse(text, clipDuration);
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
