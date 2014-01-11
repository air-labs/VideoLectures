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
    }

    class EditorModel : INotifyPropertyChanged
    {
        public int TotalLength { get; set; }

        int currentPosition;
        public int CurrentPosition 
        {
            get { return currentPosition; }
            set { 
                currentPosition=value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("CurrentPosition"));
            }
        }

        public List<ChunkData> Chunks { get; private set; }

        public EditorModel()
        {
            Chunks = new List<ChunkData>();
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
