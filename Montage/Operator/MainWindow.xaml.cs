using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using VideoLib;
using System.Windows.Media.Animation;

namespace Operator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer clockTimer;
        string filename = "log";
    
        public MainWindow()
        {
            InitializeComponent();
         
            Status.Opacity = 0;

            begin=lastCommit = DateTime.Now;
            clockTimer = new DispatcherTimer();
            clockTimer.Interval = new TimeSpan(0, 0, 1);
            clockTimer.Tick += new EventHandler(TimerTick);
            clockTimer.Start();
            KeyDown += new KeyEventHandler(MainWindowKeyDown);

            MontageCommandIO.Clear(filename);
      
        }

        void MainWindowKeyDown(object sender, KeyEventArgs e)
        {
            MontageAction action = MontageAction.Commit;
            var time = (DateTime.Now - begin).TotalMilliseconds;


            switch (e.Key)
            {
                case Key.D0: action = MontageAction.Start; break;
                case Key.D1: action = MontageAction.Commit; break;
                case Key.D2: action = MontageAction.Delete; break;
                case Key.D3: action = MontageAction.Screen; break;
                case Key.D4: action = MontageAction.Face; break;
                default: return;
            }

            MontageCommandIO.AppendCommand(new MontageCommand { Time = (int)time, Action = action }, filename);

            if (action != MontageAction.Delete)
                Status.SetResourceReference(Image.SourceProperty, "clapper");
            else
                Status.SetResourceReference(Image.SourceProperty, "trash");

            if (action == MontageAction.Face)
                VideoSource.SetResourceReference(Image.SourceProperty, "face");
            if (action == MontageAction.Screen)
                VideoSource.SetResourceReference(Image.SourceProperty, "screen");


            lastCommit = DateTime.Now; 
            
            var statusFadeout = (Storyboard)FindResource("statusFadeout");
            statusFadeout.Begin(Status);

            var clockStoryboard = (Storyboard)FindResource("clock");
            clockStoryboard.Begin();
            TimerTick(null, EventArgs.Empty);
            clockTimer.Stop();
            clockTimer.Start();


        }

        DateTime begin;
        DateTime lastCommit;

        void TimerTick(object sender, EventArgs e)
        {
            var interval=(DateTime.Now-lastCommit);
            Clock.Content = string.Format("{0:D2}:{1:D2}", interval.Minutes, interval.Seconds);
        }
    }
}
