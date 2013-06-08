﻿using System;
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
            Console.WriteLine("FFMPEG " + artuments);
            Console.WriteLine();
            var process = new Process();
            process.StartInfo.FileName = context.FFMPEGPath;
            process.StartInfo.Arguments = artuments;
            process.StartInfo.UseShellExecute = false;
            process.Start();
            process.WaitForExit();

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
                string.Format("-i {0} -ss {1} -t {2} -vn -ac 2 -ar 44100 -ab 320k -f mp3 {3}",
                    VideoInput,
                    MS(StartTime),
                    MS(Duration),
                    AudioOutput));
        }
    }

    public class SliceVideoCommand : FFMPEGCommand
    {
        public string VideoInput;
        public string VideoOutput;
        public int StartTime;
        public int Duration;

        public override string Caption
        {
            get { return string.Format("Копирование видео из {0} в {1} ({2}-{3})", VideoInput, VideoOutput, StartTime, StartTime + Duration); }
        }

        public override void Execute(BatchCommandContext context)
        {
            ExecuteFFMPEG(context,
                string.Format("-i {0} -ss {1} -t {2} {3}",
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
                string.Format("-i {0} -i {1} -vcodec copy -acodec copy {2}",
                    VideoInput,
                    AudioInput,
                    VideoOutput));
        }

    }

    public class ConcatCommand : FFMPEGCommand
    {
        public List<string> Files = new List<string>();
        public string Result;

        public override string Caption
        {
            get { return "Объединение файлов"; }
        }

        public override void Execute(BatchCommandContext context)
        {
            var temp="ConcatFilesList.txt";
            File.WriteAllText(temp, Files.Select(z => "file '" + z + "'").Aggregate((a, b) => a + "\n" + b));
            ExecuteFFMPEG(context, "-f concat -i ConcatFilesList.txt -c copy " + Result);
            File.Delete(temp);
        }    }


}
