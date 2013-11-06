using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using AForge.Neuro;
using AForge.Neuro.Learning;

namespace FunctionRegression
{
    static class Program
    {
        static Func<double, double> Function = z => Math.Sin(5*z*Math.PI)* z * z;
        static int[] Sizes = new int[] { 1, 30, 1 };








        static double[][] Inputs;
        static double[] Outputs;
        static double[][] Answers;
        static ConcurrentQueue<double> Errors = new ConcurrentQueue<double>();

        static void Learning()
        {
            Inputs = Enumerable
                        .Range(0, 20)
                        .Select(z => z / 20.0)
                        .Select(z => new[] { z })
                        .ToArray();
            Answers = Inputs
                        .Select(z => z[0])
                        .Select(Function)
                        .Select(z => new[] { z })
                        .ToArray();

            var network = new ActivationNetwork(
                new BipolarSigmoidFunction(),
                Sizes[0],
                Sizes.Skip(1).ToArray()
                );

            var teacher = new BackPropagationLearning(network);
            teacher.LearningRate = 1;
            teacher.Momentum = 0.001;

            while(true)
            {
                var watch = new Stopwatch();
                watch.Start();
                while(watch.ElapsedMilliseconds<1000)
                {
                    var currentError=teacher.RunEpoch(Inputs, Answers);
                    Errors.Enqueue(currentError);
                }
                watch.Stop();

                Outputs = Inputs
                            .Select(z => network.Compute(z)[0])
                            .ToArray();
                form.BeginInvoke(new Action(UpdateCharts));
            }
        }

        static Form form;
        static Series targetFunction;
        static Series computedFunction;
        static Series errorFunction;

        static void UpdateCharts()
        {
            targetFunction.Points.Clear();
            computedFunction.Points.Clear();
            for (int i = 0; i < Inputs.Length; i++)
            {
                targetFunction.Points.Add(new DataPoint(Inputs[i][0], Answers[i][0]));
                computedFunction.Points.Add(new DataPoint(Inputs[i][0], Outputs[i]));
            }

            double error;
            while (Errors.TryDequeue(out error))
                errorFunction.Points.Add(error);

        }


        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
           

            targetFunction = new Series()
            {
                ChartType = SeriesChartType.Line,
                Color = Color.Red,
                BorderWidth = 2
            };
            computedFunction = new Series()
            {
                ChartType = SeriesChartType.Line,
                Color = Color.Green,
                BorderWidth = 2
            };
            errorFunction = new Series()
            {
                ChartType = SeriesChartType.Line,
                Color = Color.Blue,
                BorderWidth = 2,
            };

            form = new Form()
            {
                Text = "Function regression",
                Size = new Size(800, 600),
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Controls =
                {
                    new Chart
                    {
                        ChartAreas = { new ChartArea() },
                        Series = { targetFunction, computedFunction },
                        Dock= DockStyle.Top
                    },
                    new Chart
                    {
                        ChartAreas = { new ChartArea() } ,
                        Series= { errorFunction },
                        Dock=DockStyle.Bottom
                    }
                }
            };

            new Action(Learning).BeginInvoke(null, null);

            Application.Run(form);
        }
    }
}
