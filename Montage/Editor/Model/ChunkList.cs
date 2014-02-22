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
    }
}
