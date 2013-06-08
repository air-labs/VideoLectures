using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace VideoLib
{
    public abstract class FFMPEGCommand : BatchCommand
    {
        protected string Arguments;
        public override void Execute(BatchCommandContext context)
        {
            Process p = new Process();
            p.StartInfo.FileName = context.FFMPEGPath;
            p.StartInfo.Arguments = Arguments;
            p.Start();
            p.WaitForExit();
        }
    }

    public class ExtractAudioCommand : FFMPEGCommand
    {
        public ExtractAudioCommand(string input, string output)
        {
            Arguments = string.Format("-i {0}.mp4 -vn -ac 2 -ar 44100 -ab 320k -f mp3 {1}.mp3", input, output);
        }
    }

    public class AddAudioCommand : FFMPEGCommand
    {
        public AddAudioCommand(string audioInput, string videoInput, string videoOutput)
        {
            Arguments = string.Format("-i {1}.mp4 -i {0}.mp3 -vcodec copy -acodec copy {2}.mp4", audioInput, videoInput, videoOutput);
        }
    }

    public class SubVideoCommand : FFMPEGCommand
    {
        public SubVideoCommand(string inputVideo, string outputVideo, int from, int end)
        {
            Arguments = "ffmpeg -i 123.mp4 -ss 0.5 -t 2 1223.mp4";
        }
    }

}
