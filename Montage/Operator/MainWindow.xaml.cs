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
        bool started = false;

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

            TextHolder.Text = "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.";


            MontageCommandIO.Clear(filename);
      
        }


        void ShowStatus(string statusSource)
        {
            Status.SetResourceReference(Image.SourceProperty, statusSource);
            ((Storyboard)FindResource("statusFadeout")).Begin(Status);
        }

        void MainWindowKeyDown(object sender, KeyEventArgs e)
        {
            MontageAction action = MontageAction.Commit;
            var time = (DateTime.Now - begin).TotalMilliseconds;


            switch (e.Key)
            {
                case Key.Enter:
                    if (!started)
                    {
                        action = MontageAction.Start;
                        started = false;
                    }
                    else
                        action = MontageAction.Commit;
                    break;
                case Key.Decimal: action = MontageAction.Delete; break;
                case Key.NumPad1: action = MontageAction.Screen; break;
                case Key.NumPad2: action = MontageAction.Face; break;
                case Key.NumPad9:
                    this.Viewer.ScrollToVerticalOffset(Viewer.VerticalOffset - 10);
                    return;
                case Key.NumPad6:
                    this.Viewer.ScrollToVerticalOffset(Viewer.VerticalOffset + 10);
                    return;
                default:
                    ShowStatus("question");
                    return;
            }


            

            MontageCommandIO.AppendCommand(new MontageCommand { Time = (int)time, Action = action }, filename);

            if (action != MontageAction.Delete)
                ShowStatus("clapper");
            else
                ShowStatus("trash");


            if (action == MontageAction.Face)
                VideoSource.SetResourceReference(Image.SourceProperty, "face");
            if (action == MontageAction.Screen)
                VideoSource.SetResourceReference(Image.SourceProperty, "screen");


            lastCommit = DateTime.Now; 
            
         

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
