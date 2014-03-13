﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Editor
{

    public class BorderMode : IEditorMode
    {
        const int Margin = 3000;

        EditorModel model;

        public MontageModel montage { get { return model.Montage; } }


        /*
         * Левая граница - это когда предыдущий чанк другого типа. Играется с левой границы до +Margin
         * Правая граница - это когда последующий чанк неактивен. Играется с -Margin до правой границы
         * Если области левой и правой границ перекрываются, делается пополам
         */
        IEnumerable<Border> GenerateBordersPreview()
        {
            for (int i = 1; i < montage.Chunks.Count; i++)
            {
                if (montage.Chunks[i].Mode != montage.Chunks[i-1].Mode)
                {
                    if (montage.Chunks[i - 1].IsActive)
                    {
                        yield return Border.Right(montage.Chunks[i].StartTime, Margin, i - 1, i);
                    }
                
                    if (montage.Chunks[i].IsActive)
                    {
                        yield return Border.Left(montage.Chunks[i].StartTime, Margin, i - 1, i);
                    }
                }
            }
        }

        void GenerateBorders()
        {
            var borders = GenerateBordersPreview().ToList();
            for (int i = 1; i < borders.Count; i++)
            {
                if (borders[i - 1].EndTime > borders[i].StartTime)
                {
                    var time = (borders[i - 1].EndTime + borders[i].StartTime) / 2;
                    borders[i - 1].EndTime = time;
                    borders[i].StartTime = time;
                }
            }
            montage.Borders.Clear();
            montage.Borders.AddRange(borders);

         
        }

        public BorderMode(EditorModel editorModel)
        {
            this.model = editorModel;
            GenerateBorders();
        }

        public void CheckTime()
        {
            CheckTime(model.WindowState.CurrentPosition);
        }

        public void CheckTime(int ms)
        {

            var index = montage.Chunks.FindChunkIndex(ms);
            if (index == -1)
            {
                model.WindowState.Paused = true;
                return;
            }

            if (montage.Chunks[index].IsNotActive)
            {
                while (index < montage.Chunks.Count && montage.Chunks[index].IsNotActive)
                    index++;

                if (index >= montage.Chunks.Count)
                {
                    model.WindowState.Paused = true;
                    return;
                }
                ms=model.WindowState.CurrentPosition = montage.Chunks[index].StartTime;
            }

            model.WindowState.FaceVideoIsVisible = montage.Chunks[index].Mode == Mode.Face;
            model.WindowState.DesktopVideoIsVisible = montage.Chunks[index].Mode == Mode.Screen;

            double speed = 2.5;
            var bindex = montage.Borders.FindBorder(ms);
            if (bindex != -1)
            {
                var border = montage.Borders[bindex];
                if (!border.IsLeftBorder)
                {
                    if (border.EndTime - ms < 2000) speed = 1;
                }
                else
                {
                    if (ms - border.StartTime < 1000) speed = 1;
                }

            }

            model.WindowState.SpeedRatio = speed;
            
        }


        public void MouseClick(int selectedLocation, MouseButtonEventArgs button)
        {
            model.WindowState.CurrentPosition = selectedLocation;
            CheckTime(selectedLocation);
        }


        public void ProcessKey(KeyEventArgs e)
        {
            var borderIndex = montage.Borders.FindBorder(model.WindowState.CurrentPosition);
            if (borderIndex == -1) return;
            int leftBorderIndex = -1;
            int rightBorderIndex = -1;
            if (montage.Borders[borderIndex].IsLeftBorder)
            {
                leftBorderIndex = borderIndex;
                if (borderIndex != 0 && !montage.Borders[borderIndex - 1].IsLeftBorder)
                    rightBorderIndex = borderIndex - 1;
            }
            else
            {
                rightBorderIndex = borderIndex;
                if (borderIndex != montage.Borders.Count - 1 && montage.Borders[borderIndex + 1].IsLeftBorder)
                    leftBorderIndex = borderIndex + 1;
            }

            var value = 200;
            if (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl))
                value = 50;
            

            switch (e.Key)
            {
                case Key.Q:
                    Shift(rightBorderIndex, value);
                    return;

                case Key.W:
                    Shift(rightBorderIndex, -value);
                    return;

                case Key.O:
                    Shift(leftBorderIndex, value);
                    return;

                case Key.P:
                    Shift(leftBorderIndex, -value);
                    return;

                case Key.Space:
                    model.WindowState.Paused = !model.WindowState.Paused;
                    return;
            }
        }

        void Shift(int borderIndex, int shiftSize)
        {
            if (borderIndex == -1) return;
            var border = montage.Borders[borderIndex];
            montage.Chunks.ShiftLeftBorderToRight(border.RightChunk, shiftSize);
            GenerateBorders();
            model.WindowState.CurrentPosition = montage.Borders[borderIndex].StartTime;
            if (montage.Borders[borderIndex].IsLeftBorder) model.WindowState.SpeedRatio = 1;
        }
    }
}
