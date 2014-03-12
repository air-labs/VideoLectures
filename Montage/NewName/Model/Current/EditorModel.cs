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
            
            var interval = Montage.Intervals
                .Where(z => !z.HasVoice && z.DistanceTo(rightChunk.StartTime) < Global.VoiceSettings.MaxDistanceToSilence)
                .FirstOrDefault();
            if (interval == null) return;

            var leftDistance = Math.Abs(interval.StartTime - rightChunk.StartTime);
            var rightDistance = Math.Abs(interval.EndTime - rightChunk.StartTime);
            var distance = interval.DistanceTo(rightChunk.StartTime);
            bool LeftIn = leftDistance < Global.VoiceSettings.MaxDistanceToSilence;
            bool RightIn = rightDistance  < Global.VoiceSettings.MaxDistanceToSilence;

            if (!LeftIn && !RightIn) return;

            int NewStart = rightChunk.StartTime;
            if (LeftIn && RightIn)
            {
                //значит, оба конца интервала - близко от точки сечения, и точку нужно передвинуть на середину интервада
                NewStart = interval.MiddleTimeMS;
            }
            else if (LeftIn && !RightIn)
            {
                //значит, только левая граница где-то недалеко. 
                NewStart = interval.StartTime + Global.VoiceSettings.SilenceMargin;
            }
            else if (!LeftIn && RightIn)
            {
                NewStart = interval.EndTime - Global.VoiceSettings.SilenceMargin;
            }

            //не вылезли за границы интервала при перемещении
            if (interval.DistanceTo(NewStart) > 0) return;

            //не выскочили за границы чанков при перемещении
            if (!rightChunk.Contains(NewStart) && !leftChunk.Contains(NewStart)) return;

            Montage.Chunks.ShiftLeftBorderToRight(rightChunkIndex, rightChunk.StartTime-NewStart);
        }
    }
}

