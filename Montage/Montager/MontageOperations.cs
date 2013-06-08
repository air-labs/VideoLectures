using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Montager
{
    public class ChunkSource
    {
        public string File;
        public int StartTime;
        public int Duration;
        public override string ToString()
        {
            return string.Format("{0,-10}{1,-6}{2,-6}", File, StartTime, Duration);
        }
    }

    public class Chunk
    {
        public int Id;
        public string Info;
        public ChunkSource VideoSource;
        public ChunkSource AudioSource;
        public override string ToString()
        {
            return "V:" + VideoSource.ToString() + (AudioSource != null ? "A:" + AudioSource.ToString() : "");
        }

        public IEnumerable<BatchCommand> CreateCommand()
        {
            var  videoName=string.Format("chunk{0:D3}.mp4", Id);
            if (AudioSource == null)
                yield return new SliceVideoCommand
                    {
                        VideoInput = VideoSource.File,
                        StartTime = VideoSource.StartTime,
                        Duration = VideoSource.Duration,
                        VideoOutput = videoName
                    };
            else
            {
                var audioname=string.Format("audio{0:D3}.mp3",Id);
                yield return new ExtractAudioCommand
                {
                    VideoInput=AudioSource.File,
                    StartTime=AudioSource.StartTime,
                    Duration=AudioSource.Duration,
                    AudioOutput=audioname
                };
                yield return new SliceVideoAndMixAudioCommand
                {
                    VideoInput=VideoSource.File,
                    StartTime=VideoSource.StartTime,
                    Duration=VideoSource.Duration,
                    AudioInput=audioname,
                    VideoOutput=videoName
                };
            }

        }

    }
}
