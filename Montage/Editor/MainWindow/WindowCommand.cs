using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Editor
{
    

    public class WindowCommand
    {
        public int? JumpToLocation;
        public bool Invalidate;
        public bool? Pause;
        public bool RequestProcessed = true;

        public WindowCommand AndInvalidate()
        {
            Invalidate = true;
            return this;
        }

        public static WindowCommand JumpTo(int ms)  { return new WindowCommand { JumpToLocation=ms }; }
        public static WindowCommand Processed { get { return new WindowCommand(); } }
        public static WindowCommand None { get { return new WindowCommand { RequestProcessed = false }; } }


    }
}
