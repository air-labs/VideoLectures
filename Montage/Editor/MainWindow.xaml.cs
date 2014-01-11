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

            FaceVideo.Source = new Uri(folder.FullName+"\\face.mp4", UriKind.Absolute);
            FaceVideo.SpeedRatio = 1;
            FaceVideo.Volume = 0.1;

            ScreenVideo.Source = new Uri(folder.FullName + "\\desktop.mp4", UriKind.Absolute);
            ScreenVideo.SpeedRatio = 1;
            ScreenVideo.Volume = 0.0001;

            SetPosition(model.CurrentPosition);

            this.DataContext = model;
            Timeline.DataContext = model;

            Timer t = new Timer();
            t.Interval = 10;
            t.Tick += (o, a) =>
            {
                CheckPlayTime();
            };
            t.Start();
        }

        public MainWindow()
        {
            InitializeComponent();
            FaceVideo.LoadedBehavior = MediaState.Manual;
            ScreenVideo.LoadedBehavior = MediaState.Manual;


            MouseDown += Timeline_MouseDown;
            PreviewKeyDown += MainWindow_KeyDown;
            Pause.Click += Pause_Click;

            var binding = new CommandBinding(Commands.Save);
            binding.Executed += Save;
            CommandBindings.Add(binding);

            binding = new CommandBinding(Commands.Export);
            binding.Executed += Export;
            CommandBindings.Add(binding);
            
        }

        bool paused = true;

        void Pause_Click(object sender, RoutedEventArgs e)
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
            foreach (var e in model.Chunks)
            {
                var cmd = new MontageCommand();
                cmd.Id = id++;
                cmd.Time = e.StartTime + e.Length;
                switch (e.Mode)
                {
                    case Mode.Drop: cmd.Action = MontageAction.Delete; break;
                    case Mode.Face: cmd.Action = MontageAction.Face; break;
                    case Mode.Screen: cmd.Action = MontageAction.Screen; break;
                    case Mode.Undefined: cmd.Action = MontageAction.Delete; break;
                }
                MontageCommandIO.AppendCommand(cmd, file);
            }
        }

        void MainWindow_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            var value = 0;
            if (Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightShift))
                value = -1;
            if (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl))
                value = 1;


            switch (e.Key)
            {
                case Key.Left:
                    SetPosition(model.CurrentPosition - 1000 * Math.Pow(5, value));
                    e.Handled = true;
                    break;
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
                    Commit(model.CurrentMode);
                    e.Handled = true;
                    break;
                case Key.NumPad2:
                    model.CurrentMode = Mode.Face;
                    Commit(model.CurrentMode);
                    e.Handled = true;
                    break;
                case Key.Enter:
                    Commit(model.CurrentMode);
                    e.Handled = true;
                    break;
                case Key.Decimal:
                    Commit(Mode.Drop);
                    e.Handled = true;
                    break;
                case Key.NumPad0:
                    var position = model.CurrentPosition;
                    var index = model.FindChunkIndex(position);
                    if (index == -1) return;
                    var chunk = model.Chunks[index];
                    chunk.Mode = Mode.Undefined;
                    Timeline.InvalidateVisual();
                    e.Handled = true;
                    break;
            }


        }

        void Commit(Mode mode)
        {
            var position=model.CurrentPosition;
            var index = model.FindChunkIndex(position);
            if (index == -1) return;
            var chunk = model.Chunks[index];
            if (chunk.Mode == Mode.Undefined && chunk.Length > 500)
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

        void CheckPlayTime()
        {
            var pos=(int)FaceVideo.Position.TotalMilliseconds;
            if (OnlyGood.IsChecked.Value)
            {
                bool bad=false;
                var index=model.FindChunkIndex(pos);
                if (index != -1)
                {
                    for (; index < model.Chunks.Count; index++)
                        if (model.Chunks[index].Mode == Mode.Drop)
                            bad = true;
                        else break;
                    if (bad)
                        SetPosition(model.Chunks[index].StartTime);
                }
            }
            model.CurrentPosition = pos;
        }

        void SetPosition(double ms)
        {
            FaceVideo.Position = TimeSpan.FromMilliseconds(ms);
            ScreenVideo.Position = TimeSpan.FromMilliseconds(ms + model.Shift);
        }

        void ChangeRatio(double ratio)
        {
            FaceVideo.SpeedRatio=FaceVideo.SpeedRatio*ratio;
        }

        void Timeline_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                var index = model.FindChunkIndex(Timeline.MsAtPoint(e.GetPosition(Timeline)));
                if (index == -1) return;
                SetPosition(model.Chunks[index].StartTime);
                
            }
            else
            {
                SetPosition(Timeline.MsAtPoint(e.GetPosition(Timeline)));
            }
        }
    }
}
