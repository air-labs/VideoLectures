﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Editor
{

    public class Interval
    {
        public int StartTime { get; set; }
        public int EndTime { get; set; }
        public int MiddleTimeMS { get { return (StartTime+EndTime)/2; } }

        public bool HasVoice { get; set; }

        public Interval() { }

        public Interval(int start, int end, bool hasVoice)
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
