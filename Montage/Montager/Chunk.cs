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
        public string TemporalAudioFile { get { return string.Format("audio{0:D3}.mp3", Id); } }
        public string TemporalVideoFile { get { return string.Format("video{0:D3}.mp4", Id); } }
       public string OutputVideoFile { get { return string.Format("chunk{0:D3}.mp4", Id); } }

        public override string ToString()
        {
            return "V:" + VideoSource.ToString() + (AudioSource != null ? "A:" + AudioSource.ToString() : "");
        }

      

    }
}
