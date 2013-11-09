using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using AForge.Neuro;
using AForge.Neuro.Learning;
using Common;

namespace SelfOrganizingNetworks
{
    static class SelfOrganizing
    {
        static Random rnd = new Random(1);
        static double[][] Inputs;
        static int NetworkSize = 15;
        static Form form;
        static DistanceNetwork network;
        static SOMLearning learning;
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

        static void Learning()
        {
            while (!form.Created) ;
            int drawIterations = 100;

            for (int iterNum = 0; iterNum < iterationCount; iterNum++)
            {
                for (int i=0;i<Inputs.Length;i++)
                {
                    //learning.Run(Inputs[i]);
                    learning.Run(Inputs[rnd.Next(Inputs.Length)]);

                    if (i % drawIterations == 0)
                    {


                        var map = new MapElement[NetworkSize, NetworkSize];
                        int number = 0;
                        for (int x = 0; x < NetworkSize; x++)
                            for (int y = 0; y < NetworkSize; y++)
                            {
                                var neuron = network.Layers[0].Neurons[x * NetworkSize + y];
                                map[x, y] = new MapElement { X = (float)neuron.Weights[0], Y = (float)neuron.Weights[1], Id = number++ };
                            }

                        foreach (var e in Inputs)
                        {
                            network.Compute(e);
                            var winner = network.GetWinner();
                            map[winner / NetworkSize, winner % NetworkSize].IsActive = true;
                        }

                        var space = new int[W, H];
                        for (int x=0;x<W;x++)
                            for (int y = 0; y < H; y++)
                            {
                                var xx = (double)x / W;
                                var yy = (double)y / H;
                                network.Compute(new[] { xx, yy });
                                space[x, y] = network.GetWinner();
                            }


                       queue.Enqueue(new DrawingTask { map=map, space=space }); 
                      //  Thread.Sleep(100);
                    }
                }
            }
        }

        const int W=200;
        const int H = 200;
        static ConcurrentQueue<DrawingTask> queue = new ConcurrentQueue<DrawingTask>();
        static MapElement[,] map;
        static int[,] space;
        static int selected=-1;
        static MyUserControl pointsPanel;
        static MyUserControl networkPanel;
        static MyUserControl networkGraphControl;
       
        static Brush GetBrush(MapElement element)
        {
            if (element.Id == selected) return Brushes.Red;
            else if (element.IsActive) return Brushes.Green;
            else return Brushes.LightGray;
        }

        #region NetworkGraph
        static void DrawGraph(object sender, PaintEventArgs args)
        {
            if (map == null) return;
            var g = args.Graphics;
            var W = pointsPanel.ClientSize.Width - 20;
            var H = pointsPanel.ClientSize.Height - 20;
            var unit=2f/W;
            g.Clear(Color.White);
            g.TranslateTransform(10, 10);
            var pen=new Pen(Color.FromArgb(100,Color.LightGray));
            foreach (var e in map)
            {
                g.FillEllipse(GetBrush(e),
                    e.X*W-2,
                    e.Y*W-2,
                    4,
                    4);
                if (e.MapX != NetworkSize - 1)
                    g.DrawLine(pen, W * e.X, H * e.Y, W * map[e.MapX + 1, e.MapY].X, H * map[e.MapX + 1, e.MapY].Y);
                if (e.MapY != NetworkSize - 1)
                    g.DrawLine(pen, W * e.X, H * e.Y, W * map[e.MapX, e.MapY+1].X, H * map[e.MapX, e.MapY+1].Y);
            }
        }
        #endregion
        #region Points panel

        static void DrawPoints(object sender, PaintEventArgs aegs)
        {
            var g = aegs.Graphics;
            g.Clear(Color.White);

            if (space != null && map!=null)
            {
                for (int x=0;x<W;x++)
                    for (int y = 0; y < H; y++)
                    {
                        var n = map.Cast<MapElement>().Where(z => z.Id == space[x, y]).FirstOrDefault();
                        if (n == null) continue;
                        if (!n.IsActive) continue;
                        var color = Color.FromArgb(255 - n.MapX * 128 / NetworkSize, 255 - n.MapY * 128 / NetworkSize, 255);
                        g.FillRectangle(new SolidBrush(color), x, y, 1, 1);
                    }

            }


            foreach (var e in Inputs)
            {
                g.FillEllipse(Brushes.Black,
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
            for (int x = 0; x < NetworkSize; x++)
                for (int y = 0; y < NetworkSize; y++)
                {
                    var p = map[x, y].DisplayLocation = new Point(10+x * W / NetworkSize, 10+y * H / NetworkSize);


                    g.FillEllipse(
                        GetBrush(map[x,y]),
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
            public float X;
            public float Y;
            public int Id;
            public Point DisplayLocation;
            public bool IsActive;
            public int MapX { get { return Id / NetworkSize; } }
            public int MapY { get { return Id % NetworkSize; } }

        }

        class DrawingTask
        {
            public int[,] space;
            public MapElement[,] map;

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
                    n.Weights[0] = rnd.NextDouble() * 0.2 + 0.4;
                    n.Weights[1] = rnd.NextDouble() * 0.2 + 0.4;
                }
            learning = new SOMLearning(network, NetworkSize, NetworkSize);
            learning.LearningRadius = 3;
            learning.LearningRate = 0.2;



            GenerateInputs();
            
            pointsPanel = new MyUserControl() { Dock= DockStyle.Fill};
            pointsPanel.Paint += DrawPoints;
            networkPanel = new MyUserControl() { Dock = DockStyle.Fill  };
            networkPanel.Paint += DrawNetwork;
            networkGraphControl = new MyUserControl { Dock = DockStyle.Fill  };
            networkGraphControl.Paint += DrawGraph;
            var table = new TableLayoutPanel() { Dock = DockStyle.Fill, RowCount=2, ColumnCount=2 };
            table.Controls.Add(pointsPanel, 0, 0);
            table.Controls.Add(networkPanel, 0, 1);
            table.Controls.Add(networkGraphControl, 1, 0);
            table.RowStyles.Add(new RowStyle(SizeType.Percent, 50f));
            table.RowStyles.Add(new RowStyle(SizeType.Percent, 50f));
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));


            form = new Form()
            {
                ClientSize = new Size(2*W, 2*H),
                Controls = 
                {
                   table
                }
            };

            var timer = new System.Windows.Forms.Timer();
            timer.Tick += (s, a) =>
                {
                    DrawingTask task;
                    if (queue.TryDequeue(out task))
                    {
                        map = task.map;
                        space = task.space;
                        form.Invalidate(true);
                    }
                };
            timer.Interval = 10;
            timer.Start();


            new Action(Learning).BeginInvoke(null, null);
            Application.Run(form);

        }

      

       
    }
}
