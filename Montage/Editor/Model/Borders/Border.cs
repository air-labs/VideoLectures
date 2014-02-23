﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Editor
{
    public class Border
    {
        public bool IsLeftBorder { get; set; }
        public int StartTime { get; set; }
        public int EndTime { get; set; }

        public static Border Left(int border, int margin)
        {
            return new Border { IsLeftBorder = true, StartTime = border, EndTime = border + margin };
        }
        public static Border Right(int border, int margin)
        {
            return new Border { IsLeftBorder = false, StartTime = border - margin, EndTime = border };
        }
    }
}
