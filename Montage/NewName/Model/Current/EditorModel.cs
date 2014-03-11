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
                NewStart = Math.Max(interval.EndTimeMS - Global.VoiceSettings.SilenceMargin, interval.MiddleTimeMS);
            }
            else if (rightChunk.Mode == Mode.Drop) // значит, с начала. 
            {
                NewStart = Math.Min(interval.StartTimeMS + Global.VoiceSettings.SilenceMargin, interval.MiddleTimeMS);
            }
            else
                NewStart = interval.MiddleTimeMS;

            if (Math.Abs(NewStart - rightChunk.StartTime) > Global.VoiceSettings.MaxDeviationWhenBorderingBySound)
                return;

            Montage.Chunks.ShiftLeftBorderToRight(rightChunkIndex, NewStart - rightChunk.StartTime);
        }
    }
}

