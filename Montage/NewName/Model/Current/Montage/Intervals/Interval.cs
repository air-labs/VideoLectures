using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Editor
{

    public class Interval
    {
        public double StartTime { get; set; }
        public double EndTime { get; set; }
        public int StartTimeMS { get { return (int)Math.Round(StartTime * 1000); } }
        public int EndTimeMS { get { return (int)Math.Round(EndTime * 1000); } }

        public bool HasVoice { get; set; }

        public Interval() { }

        public Interval(double start, double end, bool hasVoice)
        {
            StartTime = start;
            EndTime = end;
            HasVoice = hasVoice;
        }
        public override string ToString()
        {
            return String.Format("({0}..{1} {2})", StartTime, EndTime, HasVoice ? "+" : "-");
        }
    }
}
