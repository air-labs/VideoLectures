using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace VideoLib
{
    public class FileDeleteCommand : BatchCommand
    {
        string FileName;
        public FileDeleteCommand(string fileName)
        {
            FileName = fileName;
        }
        public override void Execute(BatchCommandContext context)
        {
            File.Delete(FileName);
        }

    }
}
