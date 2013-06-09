﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VideoLib
{
    public enum MontageAction
    {
        StartFace,
        StartScreen,
        Face,
        Screen,
        Commit,
        Delete

    }

    public class MontageCommand
    {
        public int Time;
        public MontageAction Action;
    }
}
