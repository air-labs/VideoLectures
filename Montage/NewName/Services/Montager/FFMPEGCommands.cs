using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewName.Services.Montager
{
    public abstract class FFMPEGCommand
    {
        public abstract void Execute(bool fast, bool print);

        public string MS(int milliseconds)
        {
            var dr = (milliseconds%1000).ToString();
            while (dr.Length < 3) dr = "0" + dr;

            return (milliseconds/1000).ToString() + "." + dr;
        }
    }

    public class ExtractAudioCommand : FFMPEGCommand
    {
        public FileInfo AudioInput;
        public FileInfo AudioOutput;
        public int StartTime;
        public int Duration;

        public override string ToString()
        {
            return string.Format("Извлечение аудио из {0} в {1} ({2}-{3})", AudioInput, AudioOutput, StartTime,
                StartTime + Duration);
        }

        public override void Execute(bool fast, bool print)
        {
            Shell.FFMPEG(
                print,
                fast ? "-i {0} -ss {1} -t {2} -acodec copy -vn {3}" : "-i {0} -ss {1} -t {2} -vn -q:v 0 {3}",
                AudioInput,
                MS(StartTime),
                MS(Duration),
                AudioOutput);
        }
    }

    public class ExtractVideoCommand : FFMPEGCommand
    {
        public FileInfo VideoInput;
        public FileInfo VideoOutput;
        public int StartTime;
        public int Duration;

        public override void Execute(bool fast, bool print)
        {
            Shell.FFMPEG(
                print,
                fast ? "-i {0} -ss {1} -t {2} -acodec copy -vcodec copy {3}" : "-i {0} -ss {1} -t {2} -q:v 0 {3}",
                VideoInput,
                MS(StartTime),
                MS(Duration),
                VideoOutput);
        }

        public override string ToString()
        {
            return string.Format("Копирование видео из {0} в {1} ({2}-{3})", VideoInput, VideoOutput, StartTime,
                StartTime + Duration);
        }
    }


    public class ExtractFaceVideoCommand : ExtractVideoCommand
    {
    }

    public class ExtractScreenVideoCommand : ExtractVideoCommand
    {
    }


    public class MixVideoAudioCommand : FFMPEGCommand
    {
        public FileInfo AudioInput;
        public FileInfo VideoInput;
        public FileInfo VideoOutput;


        public override string ToString()
        {
            return string.Format("Микширование видео из {0} и аудио из {1} в {2}", VideoInput, AudioInput, VideoOutput);
        }


        public override void Execute(bool fast, bool print)
        {
            Shell.FFMPEG(
                print,
                "-i {1} -i {0} -acodec copy -vcodec copy {2}",
                VideoInput,
                AudioInput,
                VideoOutput);
        }
    }

    public class ConcatCommand : FFMPEGCommand
    {
        public bool AudioOnly = false;
        public List<string> Files = new List<string>();
        public string Result;

        public override string ToString()
        {
            return "Объединение файлов";
        }

        public override void Execute(bool fast, bool print)
        {
            throw new NotImplementedException();
            /*
                var temp = "ConcatFilesList.txt";
                File.WriteAllText(temp, Files.Select(z => "file '" + z + "'").Aggregate((a, b) => a + "\r\n" + b));
                var args = "-f concat -i ConcatFilesList.txt ";
                if (AudioOnly)
                    args += " -acodec copy ";
                else
                    args += " -c copy ";
                args += Result;
                WriteFFMPEGCommand(context, args);
                //File.Delete(temp);
                */
        }
    }
}