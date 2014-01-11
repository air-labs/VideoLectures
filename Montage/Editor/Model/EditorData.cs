using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Editor
{
    enum Mode
    {
        Undefined,
        Drop,
        Screen,
        Face
    }

    class ChunkData
    {
        public int StartTime { get; set; }
        public int Length { get; set; }
        public Mode Mode { get; set; }

        public override string ToString()
        {
            return StartTime.ToString() + " " + Length.ToString();
        }
    }

    class EditorModel : INotifyPropertyChanged
    {
        public int TotalLength { get; set; }
        public int Shift { get; set; }

        Mode currentMode;
        public Mode CurrentMode
        {
            get { return currentMode; }
            set { 
                currentMode=value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("CurrentMode")); 
            }
        }

        int currentPosition;
        public int CurrentPosition 
        {
            get { return currentPosition; }
            set { 
                currentPosition=value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("CurrentPosition"));
            }
        }

        public int FindChunkIndex(int ms)
        {
            for (int i = 0; i < Chunks.Count; i++)
            {
                var e = Chunks[i];
                if (e.StartTime <= ms && (e.StartTime + e.Length) >= ms) return i;
            }
            return -1;
        }

        public List<ChunkData> Chunks { get; private set; }

        public EditorModel()
        {
            Chunks = new List<ChunkData>();
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
