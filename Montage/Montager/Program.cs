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
            DeletePattern("video???.*");
            DeletePattern("chunk???.*");
            DeletePattern("audio???.mp3");
            DeletePattern("TotalVideo.mp4");
            DeletePattern("TotalAudio.mp3");
            DeletePattern("result.*");

            //Привести исходный desk-файл к тому же формату, что и video
            //ffmpeg -i desktop.avi -vf scale=1280:720 -r 30 -qscale 0 desk1.avi

            var log = VideoLib.MontageCommandIO.ReadCommands("log.txt");
            var chunks=Montager.CreateChunks(log,"face.mp4","desk1.mpeg");
            var context = new BatchCommandContext
            {
                FFMPEGPath = "C:\\ffmpeg\\bin\\ffmpeg.exe"
            };
            foreach (var e in Montager.Processing1(chunks, "result.avi"))
            {
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine(e.Caption);
                Console.ForegroundColor = ConsoleColor.Gray;
                e.Execute(context);
            }
        }
    }
}
