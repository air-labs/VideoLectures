using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VideoLib
{
    public abstract class BatchCommand
    {
        public string Caption { get; set; }
        public abstract void Execute(BatchCommandContext context);
    }
}
