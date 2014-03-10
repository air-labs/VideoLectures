﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoLib;

namespace Editor
{

    

   public class EditorModel : INotifyPropertyChanged
    {
        public int TotalLength { get; set; }
        public int Shift { get; set; }

        public EditorModes EditorMode { get; set; }

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



        public List<ChunkData> Chunks { get; set; }
        public List<Border> Borders { get; set; }
        public List<Interval> Intervals { get; set; }
        

        public VideoInformation Information { get; set; }

        public EditorModel()
        {
            Chunks = new List<ChunkData>();
            Borders = new List<Border>();
            Intervals = new List<Interval>();
            Information = new VideoInformation();
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
