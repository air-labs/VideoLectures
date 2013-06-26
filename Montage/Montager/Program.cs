using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Montager
{
    class Program
    {
        static void DeletePattern(string pattern)
        {
            foreach (var e in Directory.GetFiles(".\\", pattern))
                File.Delete(e);

        }

        static void Main(string[] args)
        {
            Environment.CurrentDirectory = "..\\..\\..\\..\\Video\\Lectures";
            DeletePattern("video*.mp4");
            DeletePattern("audio*.mp3");
            DeletePattern("TotalVideo.mp4");
            DeletePattern("TotalAudio.mp3");


            var log = VideoLib.MontageCommandIO.ReadCommands("log.txt");
            var chunks=Montager.CreateChunks(log,"face.wmv","desktop.avi");
            var context = new BatchCommandContext
            {
                FFMPEGPath = "C:\\ffmpeg\\bin\\ffmpeg.exe"
            };
            foreach (var e in Montager.Processing2(chunks, "result.mp4"))
            {
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine(e.Caption);
                Console.ForegroundColor = ConsoleColor.Gray;
                e.Execute(context);
            }
        }
    }
}
