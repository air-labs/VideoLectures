using System;
using System.IO;
using System.Web.UI.WebControls;

namespace NewName.Services.Assembler
{
    class AvsChunk : AvsNode
    {
        public FileInfo ChunkFile { get; set; }

        public override void SerializeToContext(AvsContext context)
        {
            context.AddData(String.Format(template, Id, ChunkFile));
        }
        
        private const string template = "{0} = DirectShowSource(\"{1}\")";
    }
}