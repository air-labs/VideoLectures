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
        int SWidth = 5;

        Brush[] fills = new Brush[] { Brushes.White, Brushes.DarkRed, Brushes.DarkGreen, Brushes.DarkBlue };
        Pen borderPen = new Pen(Brushes.Black, 1);
        Pen currentPen = new Pen(Brushes.Red, 3);


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
 

        protected override Size MeasureOverride(Size availableSize)
        {
            var totalLength = model.TotalLength;
            var secs = 1 + totalLength / 1000;
            var rows = (secs * SWidth) / availableSize.Width;
            return new Size(availableSize.Width, rows * RowHeight+5);
        }

        IEnumerable<Rect> GetRects(ChunkData chunk)
        {
            var secondsInRow = (int)Math.Round(ActualWidth / SWidth);
            if (secondsInRow <= 0) secondsInRow = 10;

            var start = chunk.StartTime/1000;
            var length = chunk.Length / 1000;

            int x = start % secondsInRow;
            int y = start / secondsInRow;

            while (true)
            {
                if (x + length <= secondsInRow)
                {
                    yield return new Rect(x*SWidth, y*RowHeight, length*SWidth, RowHeight);
                    yield break;
                }
                yield return new Rect(x * SWidth, y * RowHeight, (secondsInRow-x)* SWidth, RowHeight);
                length -= (secondsInRow - x);
                x = 0;
                y++;
            }
        }



        protected override Size ArrangeOverride(Size finalSize)
        {
            return base.ArrangeOverride(finalSize);
        }

        public int ChunkIndexAtPoint(Point point)
        {
            for (int i=0;i<model.Chunks.Count;i++)
                foreach(var e in GetRects(model.Chunks[i]))
                    if (e.Contains(point)) return i;
            return -1;
        }

        public int MsAtPoint(Point point)
        {
            var secondsInRow = (int)Math.Round(ActualWidth / SWidth);
            return (int)(1000 * ((point.X / SWidth) + (secondsInRow * (int)(point.Y / RowHeight))));
        }

        public Point GetCoordinate(int timeInMilliseconds)
        {
            var msInRow = (int)Math.Round(1000*ActualWidth / SWidth);

            double x = ( timeInMilliseconds % msInRow )/1000.0;
            int y = timeInMilliseconds / msInRow;
            return new Point(
                x * SWidth,
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
