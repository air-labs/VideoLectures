using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Editor;
using NewName.Services.Assembler;

namespace NewName.Services
{
    enum AssemblerMode
    {
        Run, Print
    }
    class AssemblerService : Service
    {

        public override string Name
        {
            get { return "assembler"; }
        }

        public override string Description
        {
            get { return DescriptionString; }
        }

        public override string Help
        {
            get { return HelpString; }
        }

        public void DoWork(EditorModel model, bool print)
        {
            var avsContext = new AvsContext();
            var avsList = new List<AvsNode>();
            foreach (var chunk in model.Montage.Chunks)
            {
                avsList.Add(new AvsChunk {ChunkFile = model.Locations.Make(model.ChunkFolder, chunk.ChunkFilename)});
            }
            //avsList.Add(new AvsFadeIn {Payload = avsList[avsList.Count-1]});
            //avsList.Add(new AvsFadeOut {Payload = avsList[avsList.Count-1]});

            var a = avsList[avsList.Count - 2];
            var b = avsList[avsList.Count - 1];
            avsList.Add(new AvsCrossFade {FadeFrom = a, FadeTo = b});

            var img = new FileInfo("c:\\image.png");
            avsList.Add(new AvsIntro { ImageFile = img, VideoReference = model.Locations.Make(model.ChunkFolder, model.Montage.Chunks[0].ChunkFilename)});

            var concat = new AvsConcatList {Items = avsList};

            concat.SerializeToContext(avsContext);
            
            Console.WriteLine(avsContext.Data);
        }

        public override void DoWork(string[] args)
        {
            if (args.Length < 3)
                throw (new ArgumentException(String.Format("Insufficient args")));
            var folder = args[1];
            AssemblerMode mode;
            if (!Enum.TryParse(args[2], true, out mode))
                throw (new ArgumentException(String.Format("Unknown mode: {0}", args[2])));
            var print = mode == AssemblerMode.Print;

            var model = ModelIO.Load(folder);
            DoWork(model, print);
            // ModelIO.Save(model);
        }
        const string DescriptionString =
            @"AssemblerService service. Assembles chunks with effects using Avisynth.";
        const string HelpString =
            @"<folder> <mode> [fast]

folder: directory containing video
mode: run or print. Execute commands or write them to stdout";
    }
}