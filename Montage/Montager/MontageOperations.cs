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
        public ChunkSource VideoSource;
        public ChunkSource AudioSource;
        public override string ToString()
        {
            return "V:" + VideoSource.ToString() + (AudioSource != null ? "A:" + AudioSource.ToString() : "");
        }

    }
}
