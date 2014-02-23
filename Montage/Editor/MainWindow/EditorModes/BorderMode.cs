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
        const int Margin = 3000;
        
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
                        yield return Border.Right(model.Chunks[i].StartTime, Margin);
                }
                else
                {
                    if (model.Chunks[i - 1].Mode != model.Chunks[i].Mode)
                        yield return Border.Left(model.Chunks[i].StartTime, Margin);
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

        enum State { Left, Right, Nowhere }

        Tuple<int, int> FindNearBorders(int ms)
        {

            var index = model.Chunks.FindChunkIndex(ms);
            int leftMargin = -1;
            int rightMargin = -1;
            for (int i = index - 1; i >= 0; i--)
                if (model.Chunks[i].Mode != model.Chunks[index].Mode)
                {
                    leftMargin =ms - model.Chunks[i].EndTime;
                    break;
                }
            for (int i = index + 1; i < model.Chunks.Count; i++)
                if (model.Chunks[i].IsNotActive)
                {
                    rightMargin = model.Chunks[i].StartTime - ms;
                    break;
                }
            if (leftMargin > Margin) leftMargin = -1;
            if (rightMargin > Margin) rightMargin = -1;
            return Tuple.Create(leftMargin, rightMargin);
        }

        int GetStartOfRightBorder(int BorderEnd)
        {
            var index = model.Chunks.FindChunkIndex(BorderEnd-Margin);
            if (index == -1) throw new ArgumentException();
            if (model.Chunks[index].IsNotActive) throw new ArgumentException();
            var margins = FindNearBorders(BorderEnd-Margin);
            if (margins.Item2 == -1) throw new ArgumentException();
            if (margins.Item1 == -1) return BorderEnd - Margin;
            return BorderEnd - (margins.Item1 + margins.Item2) / 2;
        }

        State DetermineState(int ms)
        {
            var index = model.Chunks.FindChunkIndex(ms);
            if (index == -1) return State.Nowhere;
            if (model.Chunks[index].IsNotActive) return State.Nowhere;

            var margins = FindNearBorders(ms);
            var leftMargin = margins.Item1;
            var rightMargin = margins.Item2;

            if (leftMargin == -1 && rightMargin == -1) return State.Nowhere;
            if (leftMargin > rightMargin) return State.Left;
            return State.Right;
        }

        

        int FindNextTime(int ms)
        {
            //предполагаем, что ms - Nowhere
            var index = model.Chunks.FindChunkIndex(ms);
            if (model.Chunks[index].IsNotActive) //значит, следующая граница левая
            {
                for (int i = index+1; i < model.Chunks.Count; i++)
                    if (model.Chunks[i].IsActive) return model.Chunks[i].StartTime;
            }
            if (model.Chunks[index].IsActive)
            {
                for (int i = index + 1; i < model.Chunks.Count; i++)
                    if (model.Chunks[i].IsNotActive) //это будет праваяграница
                        return GetStartOfRightBorder(model.Chunks[i].StartTime);
                    else if (model.Chunks[i].Mode != model.Chunks[index].Mode) //это будет левая граница
                        return model.Chunks[i].StartTime;
            }
            return 0;
        }

        int lastMSNearRightBorder;

        public Response CheckTime(int ms)
        {
            var currentState = DetermineState(ms);
            if (currentState != State.Nowhere) return Response.None;
            var nextTime = FindNextTime(ms);
            return Response.Jump.To(nextTime);
        }



        public Response MouseClick(int ms, MouseButtonEventArgs button)
        {
            var r=CheckTime(ms);
            if (r.Action == ResponseAction.None) return Response.Jump.To(ms);
            return r;
        }


        public Response ProcessKey(KeyEventArgs key)
        {
            throw new NotImplementedException();
        }
    }
}
