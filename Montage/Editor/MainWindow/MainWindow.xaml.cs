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
       

        void SetMode(EditorModes mode)
        {
            model.EditorMode = mode;
            switch (mode)
            {
                case EditorModes.Border: currentMode = new BorderMode(model); break;
                case EditorModes.General: currentMode = new GeneralMode(model); break;
            }
            Timeline.InvalidateVisual();
        }

        internal void Initialize(EditorModel model, DirectoryInfo folder)
        {
            this.model = model;
            SetMode(model.EditorMode);

            switch (model.EditorMode)
            {
                case EditorModes.Border: BordersMode.IsChecked = true; break;
                case EditorModes.General: GeneralMode.IsChecked = true; break;
            }

            BordersMode.Checked += (s, a) => { SetMode(EditorModes.Border); };
            GeneralMode.Checked += (s, a) => { SetMode(EditorModes.General); };

            

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

            
            Statistics.Click += ShowStatistics;
        }

     
        void Save(object sender, ExecutedRoutedEventArgs e)
        {
            using (var stream = new StreamWriter("montage.editor"))
            {
                stream.WriteLine(new JavaScriptSerializer().Serialize(model));
            }
            Export(sender, e);
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
            var response = currentMode.ProcessKey(e);
            if (response.Action != ResponseAction.None)
            {
                ProcessResponse(response);
                e.Handled = true;
                return;
            }
 
            switch (e.Key)
            {
                case Key.Up:
                    ChangeRatio(1.25);
                    e.Handled = true;
                    break;
                case Key.Down:
                    ChangeRatio(0.8);
                    e.Handled = true;
                    break;
                case Key.Space:
                    CmPause();
                    e.Handled = true;
                    break;
            }
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


        IEditorMode currentMode;// = new BorderMode();

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
