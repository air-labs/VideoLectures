using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Editor
{

    public class BorderMode : IEditorMode
    {
        const int Margin = 2000;

        EditorModel model;


        /*
         * Левая граница - это когда предыдущий чанк другого типа. Играется с левой границы до +Margin
         * Правая граница - это когда последующий чанк неактивен. Играется с -Margin до правой границы
         * Если области левой и правой границ перекрываются, делается пополам
         */
        IEnumerable<Border> GenerateBordersPreview()
        {
            for (int i = 1; i < model.Chunks.Count; i++)
            {
                if (model.Chunks[i].IsNotActive)
                {
                    if (model.Chunks[i - 1].IsActive)
                        yield return Border.Right(model.Chunks[i].StartTime, Margin, i - 1, i);
                }
                else
                {
                    if (model.Chunks[i - 1].Mode != model.Chunks[i].Mode)
                        yield return Border.Left(model.Chunks[i].StartTime, Margin, i - 1, i);
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
            model.Borders.Clear();
            model.Borders.AddRange(borders);
        }

        public BorderMode(EditorModel model)
        {
            this.model = model;
            GenerateBorders();
        }

        public Response CheckTime(int ms)
        {
            if (model.Borders.FindBorder(ms) != -1) return Response.None;
            foreach (var e in model.Borders)
                if (e.StartTime >= ms) return Response.Jump.To(e.StartTime);
            return Response.Stop;
        }


        public Response MouseClick(int ms, MouseButtonEventArgs button)
        {
            var r = CheckTime(ms);
            if (r.Action == ResponseAction.None) return Response.Jump.To(ms);
            return r;
        }


        public Response ProcessKey(KeyEventArgs e)
        {
            var borderIndex = model.Borders.FindBorder(model.CurrentPosition);
            int leftBorderIndex = -1;
            int rightBorderIndex = -1;
            if (model.Borders[borderIndex].IsLeftBorder)
            {
                leftBorderIndex = borderIndex;
                if (borderIndex != 0 && !model.Borders[borderIndex - 1].IsLeftBorder)
                    rightBorderIndex = borderIndex - 1;
            }
            else
            {
                rightBorderIndex = borderIndex;
                if (borderIndex != model.Borders.Count - 1 && model.Borders[borderIndex + 1].IsLeftBorder)
                    leftBorderIndex = borderIndex + 1;
            }

            switch (e.Key)
            {
                case Key.NumPad4:
                    return Shift(rightBorderIndex, 200);

                case Key.NumPad5:
                    return Shift(rightBorderIndex, -200);

                case Key.NumPad6:
                    return Shift(leftBorderIndex, 200);

                case Key.Add:
                    return Shift(leftBorderIndex, -200);

            }
            return Response.None;
        }

        Response Shift(int borderIndex, int shiftSize)
        {
            if (borderIndex == -1) return Response.None;
            var border = model.Borders[borderIndex];
            model.Chunks.ShiftLeftBorderToRight(border.RightChunk, shiftSize);
            GenerateBorders();
            return Response.Jump.To(model.Borders[borderIndex].StartTime).AndInvalidate();
        }
    }
}
