using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.IO;

namespace Montager
{

    public class BatchCommandContext
    {
        public string FFMPEGPath;
        public StreamWriter batFile;
        public bool LD;
    }

    public abstract class BatchCommand
    {
        public abstract string Caption { get; }
        public abstract void Execute(BatchCommandContext context);
    }


}
