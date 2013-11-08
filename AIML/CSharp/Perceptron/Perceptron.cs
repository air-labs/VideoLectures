using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using AForge.Neuro;
using AForge.Neuro.Learning;

namespace Perceptron
{
    static class Program
    {
        static Func<bool, bool, bool> Function = (x, y) => x && y;
        static List<int> LearningOrder = new List<int>
        {
             1,  1,
            -1,  1,
             1, -1,
            -1, -1,
        };


        static int LearningOrderPointer = 0;

        static ActivationNetwork network;
        static PerceptronLearning learning;

        static Form form;

        static double[] weights;
        static float S;
        static double K;
        static double C;
        static PointF Plot(double x, double y)
        {
            return new PointF((float)(S*(2+x)),(float)(S*(2-y)));
        }

        static PointF Plot(double x)
        {
            return Plot(x, K * x + C);
        }

        static void DrawSpace(object sender, PaintEventArgs e)
        {
            if (weights == null) return;
            S = Math.Min(form.ClientSize.Width, form.ClientSize.Height)/4f;
            K = -weights[1] / weights[2];
            C = -weights[0] / weights[2];
            
            var g = e.Graphics;
            g.Clear(Color.White);

            var arrowPen = new Pen(Color.Black, 2) { EndCap = LineCap.ArrowAnchor };
            g.DrawLine(arrowPen, Plot(-2, 0), Plot(2, 0));
            g.DrawLine(arrowPen, Plot(0, -2), Plot(0, 2));
            var linePen = new Pen(Color.Black, 2) { DashStyle = DashStyle.Dash };
            g.DrawLine(linePen, Plot(-2), Plot(2));

            for (int x=-1;x<=1;x+=2)
                for (int y = -1; y <= 1; y += 2)
                {
                    var result = weights[1] * x + weights[2] * y + weights[0];
                    var brush = result < 0 ? Brushes.White : Brushes.Black;
                    var center = Plot(x, y);
                    var ellipse=new RectangleF(center.X-10,center.Y-10,20,20);
                    g.FillEllipse(brush, ellipse);
                    g.DrawEllipse(Pens.Black, ellipse);
                }
        }

        static double D(bool b)
        {
            return b?1:-1;
        }

        static void NextSample(object sender, EventArgs e)
        {
            var x=LearningOrder[LearningOrderPointer];
            var y=LearningOrder[LearningOrderPointer];
            LearningOrderPointer+=2;
            if (LearningOrderPointer >= LearningOrder.Count) LearningOrderPointer = 0;
            
            var input = new double[] { x,y };
            var output = new double[] { Function(x > 0, y > 0) ? 1 : -1 };
            learning.Run(input, output);


            weights = new double[3];
            weights[0] = ((ActivationNeuron)network.Layers[0].Neurons[0]).Threshold;
            weights[1]= network.Layers[0].Neurons[0].Weights[0];
            weights[2]=network.Layers[0].Neurons[0].Weights[1];
            form.Invalidate();
        }

        [STAThread]
        static void Main()
        {
            network = new ActivationNetwork(new SigmoidFunction(), 2, 1);
            ((ActivationNeuron)network.Layers[0].Neurons[0]).Threshold = 0.5;
            network.Layers[0].Neurons[0].Weights[0] = 0.3;
            network.Layers[0].Neurons[0].Weights[1] = 0.2;
       

            learning = new PerceptronLearning(network);
            var button=new Button { Top=0, Left=0 };
            button.Click+=NextSample;
            
            form = new MyForm
            {
                Controls = { button }
            };
            form.Paint+=DrawSpace;

            var timer = new System.Windows.Forms.Timer();
            timer.Interval = 100;
            timer.Tick += NextSample;
            timer.Start();
            Application.Run(form);
        }

        class MyForm : Form
        {
            public MyForm()
            {
                DoubleBuffered = true;

            }
        }
    }
}

