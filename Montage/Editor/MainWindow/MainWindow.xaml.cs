using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using VideoLib;

namespace Editor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        EditorModel model;


        internal void Initialize(EditorModel model, DirectoryInfo folder)
        {
            this.model = model;
            currentMode = new JointMode(model);
            //currentMode = new GeneralMode();

            var facePath = folder.FullName+"\\face.mp4";
            videoAvailable = File.Exists(facePath);

            FaceVideo.Source = new Uri(facePath, UriKind.Absolute);
            FaceVideo.SpeedRatio = 1;
            FaceVideo.Volume = 0.1;

            ScreenVideo.Source = new Uri(folder.FullName + "\\desktop.avi", UriKind.Absolute);
            ScreenVideo.SpeedRatio = 1;
            ScreenVideo.Volume = 0.0001;
            
            SetPosition(model.CurrentPosition);

            this.DataContext = model;
            Timeline.DataContext = model;

            Timer t = new Timer();
            t.Interval = timerInterval;
            t.Tick += (s, a) => { CheckPlayTime(); };
            t.Start();

            
        }

        public MainWindow()
        {
            InitializeComponent();
            FaceVideo.LoadedBehavior = MediaState.Manual;
            ScreenVideo.LoadedBehavior = MediaState.Manual;


            MouseDown += Timeline_MouseDown;
            PreviewKeyDown += MainWindow_KeyDown;
            Pause.Click += (a, b) => CmPause();

            var binding = new CommandBinding(Commands.Save);
            binding.Executed += Save;
            CommandBindings.Add(binding);

            binding = new CommandBinding(Commands.Export);
            binding.Executed += Export;
            CommandBindings.Add(binding);

            Statistics.Click += ShowStatistics;
        }

     
        void Save(object sender, ExecutedRoutedEventArgs e)
        {
            using (var stream = new StreamWriter("montage.editor"))
            {
                stream.WriteLine(new JavaScriptSerializer().Serialize(model));
            }
        }

        void Export(object sender, ExecutedRoutedEventArgs ce)
        {
            var file="log.txt";
            MontageCommandIO.Clear(file);
            MontageCommandIO.AppendCommand(new MontageCommand { Action = MontageAction.StartFace, Id = 1, Time = 0 }, file);
            MontageCommandIO.AppendCommand(new MontageCommand { Action = MontageAction.StartScreen, Id = 2, Time = model.Shift },file);
            int id = 3;

        
            var list = model.Chunks.ToList();
            list.Add(new ChunkData
            {
                StartTime = list[list.Count - 1].StartTime + list[list.Count - 1].Length,
                Length = 0,
                Mode = Mode.Undefined
            });



            var oldMode = Mode.Drop;

            for (int i=0;i<list.Count;i++)
            {
                var e = list[i];
                bool newEp = false; 
                if (e.Mode != oldMode || e.StartsNewEpisode || i==list.Count-1)
                {
                    var cmd = new MontageCommand();
                    cmd.Id = id++;
                    cmd.Time = e.StartTime;
                    switch (oldMode)
                    {
                        case Mode.Drop: cmd.Action = MontageAction.Delete; break;
                        case Mode.Face: cmd.Action = MontageAction.Commit; break;
                        case Mode.Screen: cmd.Action = MontageAction.Commit; break;
                        case Mode.Undefined: cmd.Action = MontageAction.Delete; break;
                    }
                    MontageCommandIO.AppendCommand(cmd, file);
                    oldMode = e.Mode;
                    newEp = true;
                }
                if (e.StartsNewEpisode)
                {
                    MontageCommandIO.AppendCommand(
                        new MontageCommand { Id = id++, Action = MontageAction.CommitAndSplit, Time = e.StartTime },
                        file
                        );
                    newEp = true;
                }
                if (newEp)
                {
                    if (e.Mode == Mode.Face || e.Mode == Mode.Screen)
                    {
                        var cmd = new MontageCommand();
                        cmd.Id = id++;
                        cmd.Time = e.StartTime;
                        switch (e.Mode)
                        {
                            case Mode.Face: cmd.Action = MontageAction.Face; break;
                            case Mode.Screen: cmd.Action = MontageAction.Screen; break;
                        }
                        MontageCommandIO.AppendCommand(cmd, file);
                    }

                }
                
            }
            
        }

        void ShowStatistics(object sender, EventArgs e)
        {
            var times = new List<int>();
            var current = 0;
            foreach (var c in model.Chunks)
            {
                if (c.StartsNewEpisode)
                {
                    times.Add(current);
                    current = 0;
                }
                if (c.Mode == Mode.Face || c.Mode == Mode.Screen)
                    current += c.Length;
            }
            times.Add(current);
            times.Add(times.Sum());
            var text = times
                .Select(z => TimeSpan.FromMilliseconds(z))
                .Select(z => z.Minutes.ToString() + ":" + z.Seconds.ToString()+"\n")
                .Aggregate((a, b) => a + b);
            System.Windows.MessageBox.Show(text);
        }

        #region Keydown и все, что с ним связано

        void MainWindow_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
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
                    SetPosition(model.CurrentPosition - 1000 * Math.Pow(5, value));
                    e.Handled = true;
                    break;
                case Key.Subtract:
                 case Key.Right:
                    SetPosition(model.CurrentPosition + 1000 * Math.Pow(5, value));
                    e.Handled = true;
                    break;
                case Key.Up:
                    ChangeRatio(1.25);
                    e.Handled = true;
                    break;
                case Key.Down:
                    ChangeRatio(0.8);
                    e.Handled = true;
                    break;
                case Key.NumPad1:
                    model.CurrentMode = Mode.Screen;
                    Commit(model.CurrentMode, ctrl);
                    e.Handled = true;
                    break;
                case Key.NumPad2:
                    model.CurrentMode = Mode.Face;
                    Commit(model.CurrentMode, ctrl);
                    e.Handled = true;
                    break;
                case Key.Enter:
                    Commit(model.CurrentMode, ctrl);
                    e.Handled = true;
                    break;
                case Key.Decimal:
                    Commit(Mode.Drop, ctrl);
                    e.Handled = true;
                    break;
                case Key.NumPad0:
                    RemoveChunk();
                    e.Handled = true;
                    break;
                case Key.NumPad8:
                    ShiftLeft(-200);
                    e.Handled = true;
                    break;
                case Key.NumPad5:
                    ShiftLeft(200);
                    e.Handled = true;
                    break;
                case Key.NumPad9:
                    ShiftRight(200);
                    e.Handled = true;
                    break;
                case Key.NumPad6:
                    ShiftRight(-200);
                    e.Handled = true;
                    break;
                case Key.NumPad4:
                    PrevChunk();
                    e.Handled = true;
                    break;
                case Key.Add:
                    NextChunk();
                    e.Handled = true;
                    break;
                case Key.Multiply:
                    var index = model.Chunks.FindChunkIndex(model.CurrentPosition);
                    if (index != -1)
                    {
                        model.Chunks[index].StartsNewEpisode = !model.Chunks[index].StartsNewEpisode;
                        Timeline.InvalidateVisual();
                    }
                    e.Handled = true;
                    break;
                case Key.Space:
                    CmPause();
                    e.Handled = true;
                    break;

            }


        }

        void RemoveChunk()
        {
            var position = model.CurrentPosition;
            var index = model.Chunks.FindChunkIndex(position);
            if (index == -1) return;
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
            Timeline.InvalidateVisual();
        }

        void ShiftLeft(int delta)
        {
            var position = model.CurrentPosition;
            var index = model.Chunks.FindChunkIndex(position);
            if (index == -1 || index == 0) return;
            if (delta < 0 && model.Chunks[index - 1].Length < -delta) return;
            if (delta > 0 && model.Chunks[index].Length < delta) return;
            model.Chunks[index].StartTime += delta;
            model.Chunks[index].Length -= delta;
            model.Chunks[index - 1].Length += delta;
            Timeline.InvalidateVisual();
            SetPosition(model.Chunks[index].StartTime);
        }

        void ShiftRight(int delta)
        {
            var position = model.CurrentPosition;
            var index = model.Chunks.FindChunkIndex(position);
            if (index == -1 || index == model.Chunks.Count-1) return;
            if (delta < 0 && model.Chunks[index].Length < -delta) return;
            if (delta > 0 && model.Chunks[index+1].Length < delta) return;
            model.Chunks[index].Length += delta;
            model.Chunks[index + 1].Length -= delta;
            model.Chunks[index + 1].StartTime += delta;
            Timeline.InvalidateVisual();
            SetPosition(model.Chunks[index+1].StartTime-2000);
        }

        void NextChunk()
        {
            var index = model.Chunks.FindChunkIndex(model.CurrentPosition);
            if (index == -1) return;
            index++;
            for (; index < model.Chunks.Count-1; index++)
            {
                if (!OnlyGood.IsChecked.HasValue || !OnlyGood.IsChecked.Value) break;
                if (model.Chunks[index].Mode != Mode.Drop) break;
            }
            if (index < 0 || index >= model.Chunks.Count) return;
            SetPosition(model.Chunks[index].StartTime);
        }

        void PrevChunk()
        {
            var index = model.Chunks.FindChunkIndex(model.CurrentPosition);
            if (index == -1) return;
            if (model.CurrentPosition - model.Chunks[index].StartTime < 1000)
                SetPosition(model.Chunks[index].StartTime);
            index--;
            for (; index > 0; index--)
            {
                if (!OnlyGood.IsChecked.HasValue || !OnlyGood.IsChecked.Value) break;
                if (model.Chunks[index].Mode != Mode.Drop) break;
            }
            if (index < 0 || index >= model.Chunks.Count) return;
            SetPosition(model.Chunks[index].StartTime);
  
        }

        void Commit(Mode mode, bool ctrl)
        {
            var position=model.CurrentPosition;
            var index = model.Chunks.FindChunkIndex(position);
            if (index == -1) return;
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
            Timeline.InvalidateVisual();
        }
        #endregion 

        #region Навигация

        double SpeedRatio = 1;
        bool videoAvailable;
        int timerInterval = 10;
        bool paused = true;

        void CmPause()
        {
            if (paused)
            {
                FaceVideo.Play();
                ScreenVideo.Play();
                Pause.Content = "Pause";
            }
            else
            {
                FaceVideo.Pause();
                ScreenVideo.Pause();
                Pause.Content = "Play";
            }
            paused = !paused;
        }


        IEditorMode currentMode;// = new JointMode();

        void CheckPlayTime()
        {
            if (videoAvailable)
                model.CurrentPosition = (int)FaceVideo.Position.TotalMilliseconds;
            else if (!paused)
                model.CurrentPosition += (int)(timerInterval * SpeedRatio);

            //if (OnlyGood.IsChecked.Value)
            //{
            //    bool bad=false;
            //    var index=model.Chunks.FindChunkIndex(pos);
            //    if (index != -1)
            //    {
            //        for (; index < model.Chunks.Count; index++)
            //            if (model.Chunks[index].Mode == Mode.Drop)
            //                bad = true;
            //            else break;
            //        if (bad)
            //            SetPosition(model.Chunks[index].StartTime);
            //    }
            //}

            ProcessResponse(currentMode.CheckTime(model.CurrentPosition));
        }


        void ProcessResponse(Response r)
        {
            if (r.Action == ResponseAction.Jump)
            {
                SetPosition(r.JumpWhere);
                Timeline.InvalidateVisual();
            }
            if (r.Invalidate)            
                Timeline.InvalidateVisual();
            if (r.Action == ResponseAction.Stop)
                CmPause();

        }
        void SetPosition(double ms)
        {
            model.CurrentPosition = (int)ms;
            FaceVideo.Position = TimeSpan.FromMilliseconds(ms);
            ScreenVideo.Position = TimeSpan.FromMilliseconds(ms - model.Shift);
        }

        void ChangeRatio(double ratio)
        {
            SpeedRatio *= ratio;
            FaceVideo.SpeedRatio=SpeedRatio;
            ScreenVideo.SpeedRatio = FaceVideo.SpeedRatio;

        }

        void Timeline_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var time = Timeline.MsAtPoint(e.GetPosition(Timeline));
            ProcessResponse(currentMode.MouseClick(time, e));
        }
        #endregion 
    }
}
