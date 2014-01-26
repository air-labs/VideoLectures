using Montager;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoLib;

namespace Assembler
{
    class PartsList
    {
        private readonly List<int> breakChunkNumbers;

        private readonly List<Part> parts = new List<Part>();
        
        public PartsList(List<int> breakChunkNumbers)
        {
            this.breakChunkNumbers = breakChunkNumbers;
        }
        
        public void MakeParts(List<string> tracks, Dictionary<int, bool> isFace)
        {
            int prevChunkNumber = 0;
            foreach (var chunkFilename in tracks)
            {
                int chunkNumber = int.Parse(chunkFilename.Substring(5, 3));
                var part = GetActivePart(chunkNumber);
                part.AddItem(chunkFilename, needCrossFade(isFace[chunkNumber], isFace[prevChunkNumber]));
                prevChunkNumber = chunkNumber;
            }
        }

        public IList<Part> Parts { get { return parts.AsReadOnly(); } }
        
        private Part GetActivePart(int currentChunkNumber)
        {
            if(parts.Count == 0)
                parts.Add(new Part(parts.Count));
            var currentPartIndex = parts.Count-1;
            if (currentPartIndex < breakChunkNumbers.Count && currentChunkNumber > breakChunkNumbers[currentPartIndex])
                parts.Add(new Part(parts.Count));
            return parts.Last();
        }

        private static bool needCrossFade(bool isFaceCurrent, bool isFacePrev)
        {
            return isFaceCurrent && isFacePrev;
        }
    }

    class Part
    {
        public int PartNumber {get; private set;}

        private readonly List<ProcessingItem> items = new List<ProcessingItem>();

        public Part(int partNumber)
        {
            this.PartNumber = partNumber;
        }

        public void AddItem(string chunkFilename, bool needCrossFade)
        {
            var item = new ProcessingItem { SourceFilename = chunkFilename };
            if (items.Count == 0)
            {
                // beginning of 'part'
                var intro = new ProcessingItem { SourceFilename = String.Format("intro_for_{0}.avi", PartNumber) };
                // doesn't exist, actually. used to build resulting Filename

                intro.Transformations.Add(new Intro());  // will generate image with text
                intro.Transformations.Add(new FadeIn());  // apply FadeIn on it
                items.Add(intro);

                item.Transformations.Add(new CrossFade { VideoPrev = intro.ResultFilename });  // crossFade with intro clip
            }
            else
            {
                // part is not empty, maybe add crossFade?
                if (needCrossFade)
                    item.Transformations.Add(new CrossFade { VideoPrev = items.Last().ResultFilename });
            }

            items.Add(item);
        }
        
        // public IList<ProcessingItem> Items { get { return items.AsReadOnly(); } }

        public void WritePartToBatch(BatchCommandContext context) {
            FinalizePart();

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(String.Format("========  {0}  ========", PartNumber));
            Console.ForegroundColor = ConsoleColor.Gray;
            
            // pre-processing
            foreach (var item in items)
            {
                Console.WriteLine(item.Caption);
                item.WriteToBatch(context);  // writes effects to .avs AND encoding command to .bat
            }

            if (!context.lowQuality)
            {
                // processing (concatenation)
                var listFile = new StreamWriter(String.Format("concat_{0}.txt", PartNumber));
                foreach (var item in items)
                    listFile.WriteLine(String.Format("file '{0}'", item.ResultFilename));
                listFile.Close();

            }
            else
            {
                // recode to "low quality" and concatenate
                var listFile = new StreamWriter(String.Format("concat_{0}.txt", PartNumber));
                Directory.CreateDirectory(recodeDir);
                foreach (var item in items) {
                    var name = Path.GetFileName(item.ResultFilename);
                    var newName = Path.Combine(recodeDir, name);
                    context.batFile.WriteLine(String.Format("ffmpeg -i {0} -vcodec copy -acodec libmp3lame -ar 44100 -ab 32k {1}", item.ResultFilename, newName));
                    listFile.WriteLine(String.Format("file '{0}'", item.ResultFilename));
                }
                listFile.Close();
            }
            context.batFile.WriteLine(String.Format("ffmpeg -f concat -i concat_{0}.txt -qscale 0 result-{0}{1}.avi", PartNumber, context.lowQuality ? "_low" : ""));
                

            // post-processing
        }

        private void FinalizePart()
        {
            items.Last().Transformations.Add(new FadeOut());
        }

        private const string recodeDir = "new";
    }
}
