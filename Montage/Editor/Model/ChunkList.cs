using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Editor
{
    public static class ChunkListExtensions 
    {
        public static int FindChunkIndex(this List<ChunkData> data, int ms)
        {
            for (int i = 0; i < data.Count; i++)
            {
                var e = data[i];
                if (e.StartTime <= ms && (e.StartTime + e.Length) >= ms) return i;
            }
            return -1;
        }

        public static void ShiftLeftBorderToRight(this List<ChunkData> data, int chunkIndex, int delta)
        {
            if (chunkIndex == 0) throw new Exception("This is leftmost chunk, cannot shift");
            data[chunkIndex - 1].Length -= delta;
            data[chunkIndex].StartTime -= delta;
            data[chunkIndex].Length += delta;
        }

        public static void ShiftRightBorderToRight(this List<ChunkData> data, int chunkIndex, int delta)
        {
            if (chunkIndex == data.Count - 1) throw new Exception("This is rightmost chunk, cannot shift");
            data.ShiftLeftBorderToRight(chunkIndex + 1, delta);
        }

    }
}
