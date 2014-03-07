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
        DirectoryInfo folder;

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
            this.folder = folder;

            FaceVideo.LoadedBehavior = MediaState.Manual;
            ScreenVideo.LoadedBehavior = MediaState.Manual;

            MouseDown += Timeline_MouseDown;
            PreviewKeyDown += MainWindow_KeyDown;
            Pause.Click += (a, b) => CmPause();

            var binding = new CommandBinding(Commands.Save);
            binding.Executed += Save;
            CommandBindings.Add(binding);


            Statistics.Click += ShowStatistics;


            SetMode(model.EditorMode);
            switch (model.EditorMode)
            {
                case EditorModes.Border: BordersMode.IsChecked = true; break;
                case EditorModes.General: GeneralMode.IsChecked = true; break;
            }

            BordersMode.Checked += (s, a) => { SetMode(EditorModes.Border); };
            GeneralMode.Checked += (s, a) => { SetMode(EditorModes.General); };

            Synchronizer.Click += (s, a) =>
                {
                    if (model.Shift != 0)
                    {
                        var response = MessageBox.Show("Вы уже синхронизировали это видео. Точно хотите пересинхронизировать?", "", MessageBoxButton.YesNoCancel);
                        if (response != MessageBoxResult.Yes) return;
                    }
                    model.Shift = model.CurrentPosition;
                    SetPosition(model.CurrentPosition);
                };

            Infos.Click += (s, a) =>
                {
                    if (model.Information.Episodes.Count == 0)
                        for (int i = 0; i < model.Chunks.Count(z => z.StartsNewEpisode); i++)
                            model.Information.Episodes.Add(new EpisodInfo());
                    var wnd = new InfoWindow();
                    wnd.DataContext = model.Information;
                    wnd.ShowDialog();
                };

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

            System.Windows.Forms.Timer t = new System.Windows.Forms.Timer();
            t.Interval = timerInterval;
            t.Tick += (s, a) => { CheckPlayTime(); };
            t.Start();
        }

        public MainWindow()
        {
            InitializeComponent();
          
        }

     
        void Save(object sender, ExecutedRoutedEventArgs e)
        {
            ModelIO.Save(folder.Parent, folder, model);
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
            var response = currentMode.ProcessKey(CreateState(), e);
            if (response.RequestProcessed)
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
        bool requestPause = false;


        void CmPause()
        {
            ProcessResponse(new WindowCommand { Pause = !paused });
        }

        void MakePause()
        {
            ProcessResponse(new WindowCommand { Pause = true });
        }
        void MakePlay()
        {
            ProcessResponse(new WindowCommand { Pause = false });
        }



        IEditorMode currentMode;// = new BorderMode();

        void CheckPlayTime()
        {
            if (videoAvailable)
                model.CurrentPosition = (int)FaceVideo.Position.TotalMilliseconds;
            else
            {
                if (paused) return;
                model.CurrentPosition += (int)(timerInterval * SpeedRatio);
            }

            if (requestPause)
            {
                MakePause();
                requestPause = false;
                return;
            }

            ProcessResponse(currentMode.CheckTime(CreateState()));
        }

        WindowState CreateState()
        {
            return new WindowState { Location = model.CurrentPosition, Paused = paused };
        }


        void ProcessResponse(WindowCommand r)
        {
            if (r.JumpToLocation.HasValue)
            {
                SetPosition(r.JumpToLocation.Value);
                Timeline.InvalidateVisual();
            }
            if (r.Invalidate)            
                Timeline.InvalidateVisual();
            if (r.Pause.HasValue)
            {
                paused = r.Pause.Value;
                if (!paused)
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
            }

        }
        void SetPosition(double ms)
        {
            model.CurrentPosition = (int)ms;

            if (!paused)
            {
                FaceVideo.Position = TimeSpan.FromMilliseconds(ms);
                ScreenVideo.Position = TimeSpan.FromMilliseconds(ms - model.Shift);
            }
            else
            {
                FaceVideo.Position = TimeSpan.FromMilliseconds(ms);
                ScreenVideo.Position = TimeSpan.FromMilliseconds(ms - model.Shift);
                MakePlay();
                requestPause = true;
            }
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
            ProcessResponse(currentMode.MouseClick(CreateState(), time, e));
        }
        #endregion 
    }
}
