using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Editor
{
    public class ChunkData
    {
        public bool StartsNewEpisode { get; set; }
        public int StartTime { get; set; }
        public int Length { get; set; } //Надо убрать или это поле, или StartTime. Сейчас здесь дублирование!
        public Mode Mode { get; set; }

        public override string ToString()
        {
            return StartTime.ToString() + " " + Length.ToString();
        }
    }
}