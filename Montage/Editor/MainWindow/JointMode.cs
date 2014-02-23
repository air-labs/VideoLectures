using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Editor
{

    public class JointMode
    {
        const int Margin = 3000;

        /*
         * Левая граница - это когда предыдущий чанк другого типа. Играется с левой границы до +Margin
         * Правая граница - это когда последующий чанк неактивен. Играется с -Margin до правой границы
         * Если области левой и правой границ перекрываются, делается пополам
         */

        EditorModel model;

        enum State { Left, Right, Nowhere }

        Tuple<int, int> FindNearJoints(int index)
        {
            

            int leftMargin = -1;
            int rightMargin = -1;
            for (int i = index - 1; i >= 0; i--)
                if (model.Chunks[i].Mode != model.Chunks[index].Mode)
                {
                    leftMargin = model.Chunks[index].StartTime - model.Chunks[i].EndTime;
                    break;
                }
            for (int i = index + 1; i < model.Chunks.Count; i++)
                if (model.Chunks[i].IsNotActive)
                {
                    rightMargin = model.Chunks[i].StartTime - model.Chunks[index].EndTime;
                    break;
                }
            if (leftMargin > Margin) leftMargin = -1;
            if (rightMargin > Margin) rightMargin = -1;
            return Tuple.Create(leftMargin, rightMargin);
        }

        int GetStartOfRightJoint(int jointEnd)
        {
            var index = model.Chunks.FindChunkIndex(jointEnd-Margin);
            if (index == -1) throw new ArgumentException();
            if (model.Chunks[index].IsNotActive) throw new ArgumentException();
            var margins = FindNearJoints(index);
            if (margins.Item2 == -1) throw new ArgumentException();
            if (margins.Item1 == -1) return jointEnd - Margin;
            return jointEnd - (margins.Item1 + margins.Item2) / 2;
        }

        State DetermineState(int ms)
        {
            var index = model.Chunks.FindChunkIndex(ms);
            if (index == -1) return State.Nowhere;
            if (model.Chunks[index].IsNotActive) return State.Nowhere;

            var margins = FindNearJoints(index);
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
                for (int i = 0; i < model.Chunks.Count; i++)
                    if (model.Chunks[i].IsActive) return model.Chunks[i].StartTime;
            }
            if (model.Chunks[index].IsActive)
            {
                for (int i = 0; i < model.Chunks.Count; i++)
                    if (model.Chunks[i].IsNotActive) //это будет праваяграница
                        return GetStartOfRightJoint(model.Chunks[i].StartTime);
                    else if (model.Chunks[i].Mode != model.Chunks[index].Mode) //это будет левая граница
                        return model.Chunks[i].StartTime;
            }
            return 0;
        }

        int lastMSNearRightBorder;

        public Response GotTo(int ms)
        {
            var currentState = DetermineState(ms);
            if (currentState != State.Nowhere) return Response.None;
            var nextTime = FindNextTime(ms);
            return Response.Jump.To(nextTime);
        }

    }
}
