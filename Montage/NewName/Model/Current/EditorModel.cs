using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Editor
{

    public class EditorModel
    {
        public DirectoryInfo RootFolder { get; set; }
        public DirectoryInfo VideoFolder { get; set; }
        public DirectoryInfo ProgramFolder { get; set; }

        public Locations Locations { get; private set; }

        public GlobalData Global { get; set; }
        public MontageModel Montage { get; set; }

        public WindowState WindowState { get; set; }

        public EditorModel()
        {
            Montage = new MontageModel();
            Locations = new Locations(this);
            WindowState = new WindowState();
            Global = new GlobalData();
        }


        public void CorrectBorderBetweenChunksBySound(int leftChunkIndex)
        {
            if (leftChunkIndex < 0) return;
            var rightChunkIndex = leftChunkIndex + 1;
            if (rightChunkIndex >= Montage.Chunks.Count) return;
            var leftChunk = Montage.Chunks[leftChunkIndex];
            var rightChunk = Montage.Chunks[rightChunkIndex];
            if (leftChunk.Mode == Mode.Undefined || rightChunk.Mode == Mode.Undefined) return;
            if (leftChunk.Mode == rightChunk.Mode) return;
            var interval = Montage.Intervals
                .Where(z => !z.HasVoice && z.StartTime < rightChunk.StartTime)
                .LastOrDefault();
            if (interval == null) return;


            int NewStart = rightChunk.StartTime;
            if (leftChunk.Mode == Mode.Drop) // значит, нужно начинать с конца интервала. Мы включаем в Drop как можно больше паузы
            {
                NewStart = Math.Max(interval.EndTime - Global.VoiceSettings.SilenceMargin, interval.MiddleTimeMS);
            }
            else if (rightChunk.Mode == Mode.Drop) // значит, с начала. 
            {
                NewStart = Math.Min(interval.StartTime + Global.VoiceSettings.SilenceMargin, interval.MiddleTimeMS);
            }
            else
                NewStart = interval.MiddleTimeMS;


            var delta = rightChunk.StartTime-NewStart;
            if (-delta > Global.VoiceSettings.MaxDeviationWhenBorderingBySound) return;

            Montage.Chunks.ShiftLeftBorderToRight(rightChunkIndex, delta);
        }
    }
}

