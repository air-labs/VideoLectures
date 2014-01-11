using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

namespace Editor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        EditorModel model;

        public MainWindow()
        {
            InitializeComponent();
            FaceVideo.Source = new Uri("test.mp4", UriKind.Relative);
            FaceVideo.SpeedRatio = 1.5;
            FaceVideo.Volume = 0.1;
            FaceVideo.Position = TimeSpan.FromMilliseconds(40000);
            
            model = new EditorModel();
            model.TotalLength = 2000000;
            for (int i = 0; i < 10; i++)
            {
                model.Chunks.Add(new ChunkData
                {
                    StartTime = i * 10000,
                    Length = 10000,
                    Mode = (Mode)(i % 4)
                });
            }
            this.Timeline.DataContext = model;

            Timeline.MouseDown += Timeline_MouseDown;

            Timer t = new Timer();
            t.Interval = 10;
            t.Tick += (o, a) =>
                {
                    model.CurrentPosition = (int)FaceVideo.Position.TotalMilliseconds;
                };
            t.Start();
        }

        void Timeline_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var index = Timeline.ChunkIndex(e.GetPosition(Timeline));
            if (index == -1) return;
            FaceVideo.Position = TimeSpan.FromMilliseconds(model.Chunks[index].StartTime);
        }
    }
}
