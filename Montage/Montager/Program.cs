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
            if (args.Length < 1)
            {
                Console.WriteLine("Montager.exe <folder>");
                Console.ReadKey();
                return;
            }

            Environment.CurrentDirectory = args[0];
            var log = VideoLib.MontageCommandIO.ReadCommands("log.txt");
            var chunks = Montager.CreateChunks(log, "..\\face-converted.avi", "..\\desktop-converted.avi");
            

            File.WriteAllLines("ConcatFilesList.txt", chunks.Select(z => "file 'chunks\\"+z.OutputVideoFile+"'").ToList());


            File.WriteAllLines("Recode.bat", 
                new string[]
                {
                "ffmpeg -i face.mp4    -vf scale=1280:720 -r 30 -q:v 0 -acodec libmp3lame -ar 44100 -ab 32k face-converted.avi",
                "ffmpeg -i desktop.avi -vf scale=1280:720 -r 30 -qscale 0 -an desktop-converted.avi"
                });


            var batFile = new StreamWriter("MakeChunks.bat");
            batFile.WriteLine("rmdir /s /q chunks");
            batFile.WriteLine("mkdir chunks");
            batFile.WriteLine("cd chunks");

            var context = new BatchCommandContext
            {
                batFile=batFile
            };
            foreach (var e in Montager.Processing1(chunks, "result.avi"))
            {
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine(e.Caption);
                Console.ForegroundColor = ConsoleColor.Gray;
                e.Execute(context);
            }
            context.batFile.WriteLine("cd ..");
            context.batFile.Close();
        }
    }
}
