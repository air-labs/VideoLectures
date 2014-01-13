using System;
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
        int RowHeight = 10;
        int msInRow = 300000;

        Brush[] fills = new Brush[] { Brushes.White, Brushes.DarkRed, Brushes.DarkGreen, Brushes.DarkBlue };
        Pen borderPen = new Pen(Brushes.Black, 1);
        Pen currentPen = new Pen(Brushes.Red, 3);

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
            };
            DataContextChanged += (o, a) =>
                {
                    if (a.NewValue is INotifyPropertyChanged)
                        (a.NewValue as INotifyPropertyChanged).PropertyChanged += Timeline_PropertyChanged;
                };
        }

        EditorModel model { get { return (EditorModel)DataContext; } }

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

        protected override void OnRender(System.Windows.Media.DrawingContext drawingContext)
        {
           foreach (var c in model.Chunks)
                foreach (var r in GetRects(c))
                    drawingContext.DrawRectangle(fills[(int)c.Mode], borderPen, r);

            var point=GetCoordinate(model.CurrentPosition);
            drawingContext.DrawLine(currentPen, point, new Point(point.X, point.Y + RowHeight));
        }
    }
}
