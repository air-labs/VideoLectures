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
        EditorModel editorModel;

        MontageModel model { get { return editorModel.Montage; } }

        public GeneralMode(EditorModel edModel)
        {
            this.editorModel = edModel;
        }

        public WindowCommand CheckTime()
        {
            return WindowCommand.None;
        }


        public WindowCommand MouseClick(int selectedLocation, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                var index = model.Chunks.FindChunkIndex(selectedLocation);
                if (index == -1) return WindowCommand.None;
                return WindowCommand.JumpTo(model.Chunks[index].StartTime);
            }
            else
            {
                return WindowCommand.JumpTo(selectedLocation);
            }
        }


        public WindowCommand ProcessKey(System.Windows.Input.KeyEventArgs e)
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
                    return WindowCommand.JumpTo((int)(editorModel.WindowState.CurrentPosition - 1000 * Math.Pow(5, value)));

                case Key.D3:
                case Key.Right:
                    return WindowCommand.JumpTo((int)(editorModel.WindowState.CurrentPosition + 1000 * Math.Pow(5, value)));

                case Key.D1:
                    return PrevChunk();

                case Key.D4:
                    return NextChunk();

                case Key.D0:
                    return Commit(Mode.Face, ctrl);
                
                
                case Key.OemMinus:
                    return Commit(Mode.Screen, ctrl);
                
                case Key.OemPlus:
                    return Commit(Mode.Drop, ctrl);
                
                case Key.Back:
                    return RemoveChunk();
                
                case Key.Q:
                    return ShiftLeft(-200);
                
                case Key.W:
                    return ShiftLeft(200);

                case Key.E:
                    return ShiftRight(-200);
                
                case Key.R:
                    return ShiftRight(200);
                
                


                case Key.D9:
                    var index = model.Chunks.FindChunkIndex(editorModel.WindowState.CurrentPosition);
                    if (index != -1)
                        model.Chunks[index].StartsNewEpisode = !model.Chunks[index].StartsNewEpisode;
                    return WindowCommand.Processed.AndInvalidate();    

            }
            return WindowCommand.None;
        }

        WindowCommand RemoveChunk()
        {
            var position = editorModel.WindowState.CurrentPosition;
            var index = model.Chunks.FindChunkIndex(position);
            if (index == -1) return WindowCommand.None;
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
            return WindowCommand.Processed.AndInvalidate();
        }

        WindowCommand ShiftLeft(int delta)
        {
            var position = editorModel.WindowState.CurrentPosition;
            var index = model.Chunks.FindChunkIndex(position);
            if (index == -1 || index == 0) return WindowCommand.None;
            if (delta < 0 && model.Chunks[index - 1].Length < -delta) return WindowCommand.None;
            if (delta > 0 && model.Chunks[index].Length < delta) return WindowCommand.None;
            model.Chunks[index].StartTime += delta;
            model.Chunks[index].Length -= delta;
            model.Chunks[index - 1].Length += delta;
            return WindowCommand.JumpTo(model.Chunks[index].StartTime).AndInvalidate();
        }

        WindowCommand ShiftRight(int delta)
        {
            var position = editorModel.WindowState.CurrentPosition;
            var index = model.Chunks.FindChunkIndex(position);
            if (index == -1 || index == model.Chunks.Count - 1) return WindowCommand.None;
            if (delta < 0 && model.Chunks[index].Length < -delta) return WindowCommand.None;
            if (delta > 0 && model.Chunks[index + 1].Length < delta) return WindowCommand.None;
            model.Chunks[index].Length += delta;
            model.Chunks[index + 1].Length -= delta;
            model.Chunks[index + 1].StartTime += delta;
            return WindowCommand.JumpTo(model.Chunks[index + 1].StartTime - 2000).AndInvalidate();
        }

        WindowCommand NextChunk()
        {
            var index = model.Chunks.FindChunkIndex(editorModel.WindowState.CurrentPosition);
            index++;
            if (index < 0 || index >= model.Chunks.Count) return WindowCommand.None;
            return WindowCommand.JumpTo(model.Chunks[index].StartTime);
        }

        WindowCommand PrevChunk()
        {
            var index = model.Chunks.FindChunkIndex(editorModel.WindowState.CurrentPosition);
            index--;
            if (index < 0 || index >= model.Chunks.Count) return  WindowCommand.None;
            return WindowCommand.JumpTo(model.Chunks[index].StartTime);

        }

        WindowCommand Commit(Mode mode, bool ctrl)
        {
            var position = editorModel.WindowState.CurrentPosition;
            var index = model.Chunks.FindChunkIndex(position);
            if (index == -1) return WindowCommand.None;
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
            editorModel.CorrectBorderBetweenChunksBySound(index - 1);
            editorModel.CorrectBorderBetweenChunksBySound(index);
            return WindowCommand.Processed.AndInvalidate();
         }


    }
}
