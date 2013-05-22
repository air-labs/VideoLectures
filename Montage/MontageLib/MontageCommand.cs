using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MontageLib
{
    public enum MontageAction
    {
        Face,
        Display,
        Commit,
        Delete

    }

    public class MontageCommand
    {
        public int Time;
        public MontageAction Action;
    }
}
