﻿using System.IO;

namespace NewName.Services.Assembler
{
    class AvsIntro : AvsNode
    {
        public FileInfo ImageFile { get; set; }

        public FileInfo VideoReference { get; set; }
        
        public int Duration = 5000;

        public override void SerializeToContext(AvsContext context)
        {
            id = context.Id;
            var reference = new AvsChunk() {ChunkFile = VideoReference};
            reference.SerializeToContext(context);
            context.AddData(string.Format(Format, Id, reference.Id, ImageFile, Duration));
            /*
             var script = String.Format(@"
                        video = DirectShowSource(""{0}"")
                        Intro(video, ""{1}"", {2})
                        ", pathToReference, pathToImage, EffectDuration);
             */
        }
        protected override string Format { get { return "{0} = Intro({1}, \"{2}\", {3})"; } }
    }
}