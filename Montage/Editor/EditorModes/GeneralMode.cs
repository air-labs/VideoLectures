﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Editor
{
    public class GeneralMode : IEditorMode
    {
        EditorModel model;

        MontageModel montage { get { return model.Montage; } }

        public GeneralMode(EditorModel edModel)
        {
            this.model = edModel;
            model.WindowState.FaceVideoIsVisible = model.WindowState.DesktopVideoIsVisible = true;
        }

        public void CheckTime()
        {
        }


        public void MouseClick(int selectedLocation, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                var index = montage.Chunks.FindChunkIndex(selectedLocation);
                if (index == -1) return;
                model.WindowState.CurrentPosition=montage.Chunks[index].StartTime;
            }
            else
            {
                model.WindowState.CurrentPosition=selectedLocation;
            }
        }


        public void ProcessKey(System.Windows.Input.KeyEventArgs e)
        {
            var value = 0.0;
            if (Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightShift))
                value = -1;
            if (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl))
                value = -1.5;
           
            
            var ctrl = Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl);

            switch (e.Key)
            {
                case Key.D2:
                case Key.Left:
                    model.WindowState.CurrentPosition=((int)(model.WindowState.CurrentPosition - 1000 * Math.Pow(5, value)));
                    return;

                case Key.D3:
                case Key.Right:
                    model.WindowState.CurrentPosition = ((int)(model.WindowState.CurrentPosition + 1000 * Math.Pow(5, value)));
                    return;
                   
                case Key.D1:
                    PrevChunk();
                    return;

                case Key.D4:
                    NextChunk();
                    return;

                case Key.D0:
                    model.SetChunkMode(model.WindowState.CurrentPosition,Mode.Face, ctrl);
                    return;
                
                case Key.OemMinus:
                    model.SetChunkMode(model.WindowState.CurrentPosition,Mode.Screen, ctrl);
                    return;

                case Key.OemPlus:
                    model.SetChunkMode(model.WindowState.CurrentPosition,Mode.Drop, ctrl);
                    return;
                
                case Key.Back:
                    RemoveChunk();
                    return;

                case Key.Q:
                    ShiftLeft(-200);
                    return;

                case Key.W:
                    ShiftLeft(200);
                    return;

                case Key.E:
                    ShiftRight(-200);
                    return;
                
                case Key.R:
                    ShiftRight(200);
                    return;

                case Key.Up:
                    model.WindowState.SpeedRatio+=0.5;
                    return;

                case Key.Down:
                    model.WindowState.SpeedRatio -= 0.5;
                    return;

                case Key.Space:
                    model.WindowState.Paused = !model.WindowState.Paused;
                    return;


                case Key.D9:
                    var index = montage.Chunks.FindChunkIndex(model.WindowState.CurrentPosition);
                    if (index != -1)
                        montage.Chunks[index].StartsNewEpisode = !montage.Chunks[index].StartsNewEpisode;
                    return;

            }

        }

        void RemoveChunk()
        {
            var position = model.WindowState.CurrentPosition;
            var index = montage.Chunks.FindChunkIndex(position);
            if (index == -1) return;
            var chunk = montage.Chunks[index];
            chunk.Mode = Mode.Undefined;
            if (index != montage.Chunks.Count - 1 && montage.Chunks[index + 1].Mode == Mode.Undefined)
            {
                chunk.Length += montage.Chunks[index + 1].Length;
                montage.Chunks.RemoveAt(index + 1);
            }
            if (index != 0 && montage.Chunks[index - 1].Mode == Mode.Undefined)
            {
                chunk.StartTime = montage.Chunks[index - 1].StartTime;
                chunk.Length += montage.Chunks[index - 1].Length;
                montage.Chunks.RemoveAt(index - 1);
            }
        }

        void ShiftLeft(int delta)
        {
            var position = model.WindowState.CurrentPosition;
            var index = montage.Chunks.FindChunkIndex(position);
            if (index == -1 || index == 0) return;
            if (delta < 0 && montage.Chunks[index - 1].Length < -delta) return;
            if (delta > 0 && montage.Chunks[index].Length < delta) return ;
            montage.Chunks[index].StartTime += delta;
            montage.Chunks[index].Length -= delta;
            montage.Chunks[index - 1].Length += delta;

            model.WindowState.CurrentPosition = montage.Chunks[index].StartTime;
        }

        void ShiftRight(int delta)
        {
            var position = model.WindowState.CurrentPosition;
            var index = montage.Chunks.FindChunkIndex(position);
            if (index == -1 || index == montage.Chunks.Count - 1) return;
            if (delta < 0 && montage.Chunks[index].Length < -delta) return;
            if (delta > 0 && montage.Chunks[index + 1].Length < delta) return;
            montage.Chunks[index].Length += delta;
            montage.Chunks[index + 1].Length -= delta;
            montage.Chunks[index + 1].StartTime += delta;

            model.WindowState.CurrentPosition = montage.Chunks[index + 1].StartTime - 2000;
        }

        void NextChunk()
        {
            var index = montage.Chunks.FindChunkIndex(model.WindowState.CurrentPosition);
            index++;
            if (index < 0 || index >= montage.Chunks.Count) return;
            model.WindowState.CurrentPosition=montage.Chunks[index].StartTime;
        }

        void PrevChunk()
        {
            var index = montage.Chunks.FindChunkIndex(model.WindowState.CurrentPosition);
            index--;
            if (index < 0 || index >= montage.Chunks.Count) return;
            model.WindowState.CurrentPosition=montage.Chunks[index].StartTime;

        }

      

    }
}
