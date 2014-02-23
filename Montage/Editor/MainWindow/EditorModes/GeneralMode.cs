using System;
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

        public GeneralMode(EditorModel model)
        {
            this.model = model;
        }

        public Response CheckTime(int ms)
        {
            return Response.None;
        }


        public Response MouseClick(int ms, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                var index = model.Chunks.FindChunkIndex(ms);
                if (index == -1) return Response.None;
                return Response.Jump.To(model.Chunks[index].StartTime);
            }
            else
            {
                return Response.Jump.To(ms);
            }
        }


        public Response ProcessKey(System.Windows.Input.KeyEventArgs e)
        {
            var value = 0;
            if (Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightShift))
                value = -1;
            if (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl))
                value = 1;
            var ctrl = Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl);

            switch (e.Key)
            {
                case Key.NumPad7:
                case Key.Left:
                    return Response.Jump.To((int)(model.CurrentPosition - 1000 * Math.Pow(5, value)));

                case Key.Subtract:
                case Key.Right:
                    return Response.Jump.To((int)(model.CurrentPosition + 1000 * Math.Pow(5, value)));
                
                case Key.NumPad1:
                    model.CurrentMode = Mode.Screen;
                    return Commit(model.CurrentMode, ctrl);
                   
                case Key.NumPad2:
                    model.CurrentMode = Mode.Face;
                    return Commit(model.CurrentMode, ctrl);
                
                case Key.Enter:
                    return Commit(model.CurrentMode, ctrl);
                
                case Key.Decimal:
                    return Commit(Mode.Drop, ctrl);
                
                case Key.NumPad0:
                    return RemoveChunk();
                
                case Key.NumPad8:
                    return ShiftLeft(-200);
                
                case Key.NumPad5:
                    return ShiftLeft(200);
                
                case Key.NumPad9:
                    return ShiftRight(200);
                
                case Key.NumPad6:
                    return ShiftRight(-200);
                
                case Key.NumPad4:
                    return PrevChunk();

                case Key.Add:
                    return NextChunk();

                case Key.Multiply:
                    var index = model.Chunks.FindChunkIndex(model.CurrentPosition);
                    if (index != -1)
                        model.Chunks[index].StartsNewEpisode = !model.Chunks[index].StartsNewEpisode;
                    return Response.Processed.AndInvalidate();    

            }
            return Response.None;
        }

        Response RemoveChunk()
        {
            var position = model.CurrentPosition;
            var index = model.Chunks.FindChunkIndex(position);
            if (index == -1) return Response.None;
            var chunk = model.Chunks[index];
            chunk.Mode = Mode.Undefined;
            if (index != model.Chunks.Count - 1 && model.Chunks[index + 1].Mode == Mode.Undefined)
            {
                chunk.Length += model.Chunks[index + 1].Length;
                model.Chunks.RemoveAt(index + 1);
            }
            if (index != 0 && model.Chunks[index - 1].Mode == Mode.Undefined)
            {
                chunk.StartTime = model.Chunks[index - 1].StartTime;
                chunk.Length += model.Chunks[index - 1].Length;
                model.Chunks.RemoveAt(index - 1);
            }
            return Response.Processed.AndInvalidate();
        }

        Response ShiftLeft(int delta)
        {
            var position = model.CurrentPosition;
            var index = model.Chunks.FindChunkIndex(position);
            if (index == -1 || index == 0) return Response.None;
            if (delta < 0 && model.Chunks[index - 1].Length < -delta) return Response.None;
            if (delta > 0 && model.Chunks[index].Length < delta) return Response.None;
            model.Chunks[index].StartTime += delta;
            model.Chunks[index].Length -= delta;
            model.Chunks[index - 1].Length += delta;
            return Response.Jump.To(model.Chunks[index].StartTime).AndInvalidate();
        }

        Response ShiftRight(int delta)
        {
            var position = model.CurrentPosition;
            var index = model.Chunks.FindChunkIndex(position);
            if (index == -1 || index == model.Chunks.Count - 1) return Response.None;
            if (delta < 0 && model.Chunks[index].Length < -delta) return Response.None;
            if (delta > 0 && model.Chunks[index + 1].Length < delta) return Response.None;
            model.Chunks[index].Length += delta;
            model.Chunks[index + 1].Length -= delta;
            model.Chunks[index + 1].StartTime += delta;
            return Response.Jump.To(model.Chunks[index + 1].StartTime - 2000).AndInvalidate();
        }

        Response NextChunk()
        {
            var index = model.Chunks.FindChunkIndex(model.CurrentPosition);
            index++;
            if (index < 0 || index >= model.Chunks.Count) return Response.None;
            return Response.Jump.To(model.Chunks[index].StartTime);
        }

        Response PrevChunk()
        {
            var index = model.Chunks.FindChunkIndex(model.CurrentPosition);
            index--;
            if (index < 0 || index >= model.Chunks.Count) return  Response.None;
            return Response.Jump.To(model.Chunks[index].StartTime);

        }

        Response Commit(Mode mode, bool ctrl)
        {
            var position = model.CurrentPosition;
            var index = model.Chunks.FindChunkIndex(position);
            if (index == -1) return Response.None;
            var chunk = model.Chunks[index];
            if (chunk.Mode == Mode.Undefined && chunk.Length > 500 && !ctrl)
            {
                var chunk1 = new ChunkData { StartTime = chunk.StartTime, Length = position - chunk.StartTime, Mode = mode };
                var chunk2 = new ChunkData { StartTime = position, Length = chunk.Length - chunk1.Length, Mode = Mode.Undefined };
                model.Chunks.RemoveAt(index);
                model.Chunks.Insert(index, chunk1);
                model.Chunks.Insert(index + 1, chunk2);
            }
            else
            {
                chunk.Mode = mode;
            }
            return Response.Processed.AndInvalidate();
         }


    }
}
