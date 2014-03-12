﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Editor
{
    public class Timeline : FrameworkElement
    {
        int RowHeight = 20;
        int msInRow = 300000;

        Brush[] fills = new Brush[] { Brushes.White, Brushes.MistyRose, Brushes.LightGreen, Brushes.LightBlue};
        Pen borderPen = new Pen(Brushes.Black, 1);
        Pen currentPen = new Pen(Brushes.Red, 3);
        Pen episode = new Pen(Brushes.Yellow,3);
        Pen border = new Pen(Brushes.Gray, 3) { EndLineCap = PenLineCap.Triangle };

        #region Размер
        protected override Size MeasureOverride(Size availableSize)
        {
            var totalLength = model.TotalLength;
            var rows = (int)Math.Ceiling(((double)totalLength) / msInRow);
            return new Size(availableSize.Width, rows * RowHeight + 5);
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            return base.ArrangeOverride(finalSize);
        }
        #endregion

        public Timeline()
        {
            DataContext = new EditorModel
            {
                Montage = new MontageModel
                {
                    TotalLength = 3600000,
                    Chunks = 
                 {
                     new ChunkData
                     {
                         Length=3000,
                          StartTime=0,
                          Mode=Mode.Undefined
                     },
                     new ChunkData
                     {
                         Length=10000,
                          StartTime=3000,
                          Mode=Mode.Screen
                     }
                 }
                }};
            
            DataContextChanged += (o, a) =>
                {
                    (a.NewValue as EditorModel).WindowState.PropertyChanged += Timeline_PropertyChanged;
                };
        }

        EditorModel editorModel { get { return (EditorModel)DataContext; } }
        MontageModel model { get { return editorModel.Montage;  } }

        protected void Timeline_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            InvalidateVisual();
        }

        IEnumerable<Rect> GetRects(ChunkData chunk)
        {
            double SWidth = ActualWidth / msInRow;

            var start = chunk.StartTime;
            var length = chunk.Length;

            int x = start % msInRow;
            int y = start / msInRow;

            while (true)
            {
                if (x + length <= msInRow)
                {
                    yield return new Rect(x*SWidth, y*RowHeight, length*SWidth, RowHeight);
                    yield break;
                }
                yield return new Rect(x * SWidth, y * RowHeight, (msInRow-x)* SWidth, RowHeight);
                length -= (msInRow - x);
                x = 0;
                y++;
            }
        }

        public int MsAtPoint(Point point)
        {
            var row = (int)point.Y / RowHeight;
            return (int)Math.Round(msInRow * (row + point.X / ActualWidth));
        }


        public Point GetCoordinate(int timeInMilliseconds)
        {
            int y = timeInMilliseconds / msInRow;
            double x = timeInMilliseconds % msInRow;
            return new Point(
                x * ActualWidth / msInRow,
                y * RowHeight);
        }

        void DrawLine(DrawingContext context, Pen pen, int startPoint, int endPoint, int verticalDisplacement)
        {
            var begin = GetCoordinate(startPoint);
            var end = GetCoordinate(endPoint);
            begin.Y += verticalDisplacement;
            end.Y += verticalDisplacement;
            if (begin.Y == end.Y)
            {
                context.DrawLine(pen, begin, end);
            }
            else
            {
                context.DrawLine(pen, begin, new Point(ActualWidth, begin.Y));
                context.DrawLine(pen, new Point(0, end.Y), end);
            }
        }
     

        protected override void OnRender(System.Windows.Media.DrawingContext drawingContext)
        {
           foreach (var c in model.Chunks)
               foreach (var r in GetRects(c))
               {
                   drawingContext.DrawRectangle(fills[(int)c.Mode], borderPen, r);
                   if (c.StartsNewEpisode)
                   {
                       var p = GetCoordinate(c.StartTime);
                       drawingContext.DrawLine(episode, p, new Point(p.X, p.Y + RowHeight));
                   }
               }

           if (model.Intervals != null)
           {
               foreach (var i in model.Intervals)
               {
                   if (!i.HasVoice)
                       DrawLine(drawingContext, border, i.StartTime, i.EndTime, RowHeight - 3);
               }
           }

            if (editorModel.WindowState.CurrentMode == EditorModes.Border)
                foreach (var e in model.Borders)
                {
                    DrawLine(drawingContext, border, e.StartTime, e.EndTime, 3);
                }

            var point=GetCoordinate(editorModel.WindowState.CurrentPosition);
            drawingContext.DrawLine(currentPen, point, new Point(point.X, point.Y + RowHeight));
        }
    }
}
