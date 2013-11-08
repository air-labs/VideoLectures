using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using AForge.Neuro;
using AForge.Neuro.Learning;
using Common;

namespace SelfOrganizingNetworks
{
    static class Program
    {
        static Random rnd = new Random(1);
        static double[][] Inputs;
        static int NetworkSize = 15;
        static Form form;
        static DistanceNetwork network;
        static SOMLearning learning;
        static Timer timer;
        static int iterationCount=500;

        static void GenerateInputs(List<double[]> points, double x, double y, int count, double radius)
        {
            for (int i = 0; i < count; i++)
            {
                var xx = x + rnd.NextDouble() * 2 * radius - radius;
                var yy = y + rnd.NextDouble() * 2 * radius - radius;
                points.Add(new[] { xx, yy });
            }

        }

        static void GenerateInputs()
        {
            var list = new List<double[]>();
            GenerateInputs(list, 0.2, 0.2, 50, 0.2);
            GenerateInputs(list, 0.8, 0.5, 50, 0.2);
            GenerateInputs(list, 0.2, 0.8, 50, 0.2);
            Inputs = list.ToArray();
        }

        static void Learning(object sender, EventArgs sd)
        {
            iterationCount--;
            if (iterationCount < 0 ) timer.Stop();
            learning.RunEpoch(Inputs);


            map = new MapElement[NetworkSize, NetworkSize];
            int number = 0;
            for (int x = 0; x < NetworkSize; x++)
                for (int y = 0; y < NetworkSize; y++)
                {
                    var neuron = network.Layers[0].Neurons[x * NetworkSize + y];
                    map[x, y] = new MapElement { X = neuron.Weights[0], Y = neuron.Weights[1], Id= number++ };
                }

            foreach (var e in Inputs)
            {
                network.Compute(e);
                var winner = network.GetWinner();
                map[winner / NetworkSize, winner % NetworkSize].IsActive = true;
            }

            form.BeginInvoke(new Action<bool>(form.Invalidate), true);
        }

        static MapElement[,] map;
        static int selected=-1;
        static MyUserControl pointsPanel;
        static MyUserControl networkPanel;
        static MyUserControl networkGraphControl;

        #region NetworkGraph
        static void DrawGraph(object sender, PaintEventArgs args)
        {
            var g = args.Graphics;
            var W = pointsPanel.ClientSize.Width - 20;
            var H = pointsPanel.ClientSize.Height - 20;
            var unit=1f/W;
            g.Clear(Color.White);
            g.TranslateTransform(10, 10);
            g.ScaleTransform(W, H);
            foreach (var e in map)
            {
                g.FillEllipse(e.IsActive?Brushes.Green:Brushes.LightGray,
                    (float)e.X-unit,
                    (float)e.Y-unit,
                    2*unit,
                    2*unit);
            }
        }
        #endregion
        #region Points panel

        static void DrawPoints(object sender, PaintEventArgs aegs)
        {
            var g = aegs.Graphics;
            var W = pointsPanel.ClientSize.Width-20 ;
            var H = pointsPanel.ClientSize.Height - 20;
            g.Clear(Color.White);
            g.TranslateTransform(10, 10);
            foreach (var e in Inputs)
            {
                g.FillEllipse(Brushes.Blue,
                    (int)(W* e[0]) - 2,
                    (int)(H * e[1]) - 2,
                    4, 4);
            }
            if (map == null) return;
            if (selected == -1) return;
            var m = map.Cast<MapElement>().Where(z => z.Id == selected).First();
            g.DrawEllipse(Pens.Red,
               (int)(m.X * W) - 20,
               (int)(m.Y * H) - 20,
               40, 40);

            
        }

        static void pointsPanel_MouseMove(object sender, MouseEventArgs e)
        {
            var x = (double)(e.X - 10) / pointsPanel.Width;
            var y = (double)(e.Y - 10) / pointsPanel.Height;
            network.Compute(new[] { x, y });
            selected = network.GetWinner();
            form.Invalidate(true);
        }
        #endregion
        #region Network panel
        static void DrawNetwork(object sender, PaintEventArgs aegs)
        {
            if (map == null) return;
            var g = aegs.Graphics;
            var W = networkPanel.ClientSize.Width - 20;
            var H = networkPanel.ClientSize.Height - 20;
 
            for (int x = 0; x < NetworkSize; x++)
                for (int y = 0; y < NetworkSize; y++)
                {
                    var p = map[x, y].DisplayLocation = new Point(10+x * W / NetworkSize, 10+y * H / NetworkSize);

                    Brush color=null;
                    if (selected==map[x,y].Id)
                        color=Brushes.Red;
                    else if (map[x,y].IsActive)
                        color=Brushes.Green;
                    else
                        color=Brushes.LightGray;

                    g.FillEllipse(
                        color,
                        p.X - 5,
                        p.Y - 5,
                        10, 10);
                }
        }

        static void MouseMove(object sender, MouseEventArgs e)
        {
            if (map == null) return;
            foreach (var m in map)
            {
                if (Math.Abs(e.X - m.DisplayLocation.X) < 5 && Math.Abs(e.Y - m.DisplayLocation.Y) < 5)
                {
                    if (selected!=m.Id)
                    {
                        selected = m.Id;
                        form.Invalidate(true);
                     }
                    return;
                }
            }
            if (selected != -1)
            {
                selected = -1;
                form.Invalidate(true);
            }

        }
        #endregion

        class MapElement
        {
            public double X;
            public double Y;
            public int Id;
            public Point DisplayLocation;
            public bool IsActive;
            public int MapX { get { return Id / NetworkSize; } }
            public int MapY { get { return Id % NetworkSize; } }

        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            network = new DistanceNetwork(2, NetworkSize * NetworkSize);
            for (int x=0;x<NetworkSize;x++)
                for (int y = 0; y < NetworkSize; y++)
                {
                    var n = network.Layers[0].Neurons[x * NetworkSize + y];
                    n.Weights[0] = (double)x / NetworkSize;
                    n.Weights[1] = (double)y / NetworkSize;
                }
            learning = new SOMLearning(network, NetworkSize, NetworkSize);
            learning.LearningRadius = 3;
            learning.LearningRate = 0.2;



            GenerateInputs();
            
            pointsPanel = new MyUserControl() { Dock= DockStyle.Fill};
            pointsPanel.Paint += DrawPoints;
            networkPanel = new MyUserControl() { Dock = DockStyle.Fill  };
            networkPanel.Paint += DrawNetwork;
            networkPanel.MouseMove += MouseMove;
            pointsPanel.MouseMove += pointsPanel_MouseMove;
            networkGraphControl = new MyUserControl { Dock = DockStyle.Fill  };
            var table = new TableLayoutPanel() { Dock = DockStyle.Fill, RowCount=2, ColumnCount=2 };
            table.Controls.Add(pointsPanel, 0, 0);
            table.Controls.Add(networkPanel, 0, 1);
            table.Controls.Add(networkGraphControl, 1, 0);
            table.RowStyles[0].Height = 300;// table.RowStyles[1].Height = table.ColumnStyles[0].Width = table.ColumnStyles[1].Width = 300;
            
            form = new Form()
            {
                Size = new Size(600, 600),
                Controls = 
                {
                   table
                }
            };
            timer = new Timer();
            timer.Tick += Learning;
            timer.Interval = 200;
            timer.Start();
            Application.Run(form);

        }

      

       
    }
}
