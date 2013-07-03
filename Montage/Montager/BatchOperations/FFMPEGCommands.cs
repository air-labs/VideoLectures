using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.IO;

namespace Montager
{

    public abstract class FFMPEGCommand : BatchCommand
    {
        public void ExecuteFFMPEG(BatchCommandContext context, string artuments)
        {
            context.batFile.WriteLine("ffmpeg " + context);
            
            /*Console.WriteLine("FFMPEG " + artuments);
            Console.WriteLine();
            var process = new Process();
            process.StartInfo.FileName = context.FFMPEGPath;
            process.StartInfo.Arguments = artuments;
            process.StartInfo.UseShellExecute = false;
            process.Start();
            process.WaitForExit();
            if (process.ExitCode != 0)
            {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine("ERROR");
                Console.ReadKey();
            }
            */
        }

        public string MS(int milliseconds)
        {
            return (milliseconds / 1000).ToString() + "." + milliseconds % 1000;
        }
    }


    public class ExtractAudioCommand : FFMPEGCommand
    {
        public string VideoInput;
        public string AudioOutput;
        public int StartTime;
        public int Duration;

        public override string Caption
        {
            get { return string.Format("Извлечение аудио из {0} в {1} ({2}-{3})", VideoInput, AudioOutput, StartTime, StartTime + Duration); }
        }

        public override void Execute(BatchCommandContext context)
        {
            ExecuteFFMPEG(context,
                string.Format("-i {0} -ss {1} -t {2} -ab 160k -ac 2 -ar 44100 -vn {3}",
                    VideoInput,
                    MS(StartTime),
                    MS(Duration),
                    AudioOutput));
        }
    }

    public abstract class ExtractVideoCommand : FFMPEGCommand
    {
        public string VideoInput;
        public string VideoOutput;
        public int StartTime;
        public int Duration;

        public override string Caption
        {
            get { return string.Format("Копирование видео из {0} в {1} ({2}-{3})", VideoInput, VideoOutput, StartTime, StartTime + Duration); }
        }
    }


    public class ExtractFaceVideoCommand : ExtractVideoCommand
    {
        public override void Execute(BatchCommandContext context)
        {
            ExecuteFFMPEG(context,
                string.Format("-i {0} -ss {1} -t {2} -vf scale=1024:576 -qscale 5 {3}",
                    VideoInput,
                    MS(StartTime),
                    MS(Duration),
                    VideoOutput));
        }
    }

    public class ExtractScreenVideoCommand : ExtractVideoCommand
    {
        public override void Execute(BatchCommandContext context)
        {
            ExecuteFFMPEG(context,
                string.Format("-i {0} -ss {1} -t {2} -vf scale=1024:576 -r 30 -qscale 5 {3}",
                    VideoInput,
                    MS(StartTime),
                    MS(Duration),
                    VideoOutput));
        }
    }


    public class MixVideoAudioCommand : FFMPEGCommand
    {
        public string AudioInput;
        public string VideoInput;
        public string VideoOutput;


        public override string Caption
        {
            get { return string.Format("Микширование видео из {0} и аудио из {1} в {2}", VideoInput, AudioInput, VideoOutput); }
        }

        public override void Execute(BatchCommandContext context)
        {

            ExecuteFFMPEG(context,
                string.Format("-i {1} -i {0} -acodec copy -vcodec copy {2}",
                    VideoInput,
                    AudioInput,
                    VideoOutput));
        }

    }

    public class ConcatCommand : FFMPEGCommand
    {
        public bool AudioOnly = false;
        public List<string> Files = new List<string>();
        public string Result;

        public override string Caption
        {
            get { return "Объединение файлов"; }
        }

        public override void Execute(BatchCommandContext context)
        {
            var temp="ConcatFilesList.txt";
            File.WriteAllText(temp, Files.Select(z => "file '" + z + "'").Aggregate((a, b) => a + "\r\n" + b));
            var args = "-f concat -i ConcatFilesList.txt ";
            if (AudioOnly)
                args += " -acodec copy ";
            else
                args += " -c copy ";
            args+=Result;
            ExecuteFFMPEG(context, args);
            //File.Delete(temp);
        }
    }




}
