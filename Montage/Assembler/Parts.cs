using System;
using System.Collections.Generic;
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
        
        public IList<ProcessingItem> Items { get { return items.AsReadOnly(); } }

        public void FinalizePart()
        {
            items.Last().Transformations.Add(new FadeOut());
        }
    }
}
