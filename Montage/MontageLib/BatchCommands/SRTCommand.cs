using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VideoLib
{
    public class SRTCommand : BatchCommand
    {
        public SRTCommand(string Text, int start, int end)
        {
        }

        public override void Execute(BatchCommandContext context)
        {
            throw new NotImplementedException();
        }
    }
}
