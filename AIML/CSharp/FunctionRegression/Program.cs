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
using Common;

namespace FunctionRegression
{
    static class Program
    {
        static int BaseSize = 50;
        static Func<double, double> Function = z => z * Math.Sin(5*Math.PI*z)*0.5+0.5;
        static int[] Sizes = new int[] { 1, 40, 40, 1 };


        static double[][] Inputs;
        static double[] Outputs;
        static double[][] Answers;
        static ConcurrentQueue<double> Errors = new ConcurrentQueue<double>();


        static BackPropagationLearning teacher;
        static ActivationNetwork network;
        static Random rnd = new Random();


        static double[] GetWeigts()
        {
            return network.Layers.SelectMany(z => z.Neurons).SelectMany(z => z.Weights).ToArray();
        }


        static void Learning()
        {
            Inputs = Enumerable
                        .Range(0, BaseSize)
                        .Select(z => z / (double)BaseSize)
                        .Select(z => new[] { z })
                        .ToArray();
            Answers = Inputs
                        .Select(z => z[0])
                        .Select(Function)
                        .Select(z => new[] { z })
                        .ToArray();

            network = new ActivationNetwork(
                new SigmoidFunction(),
                Sizes[0],
                Sizes.Skip(1).ToArray()
                );

            foreach (var l in network.Layers)
                foreach (var n in l.Neurons)
                    for (int i = 0; i < n.Weights.Length; i++)
                        n.Weights[i] = rnd.NextDouble() * 2 - 1;


            teacher = new BackPropagationLearning(network);
            teacher.LearningRate = 2;
            teacher.Momentum = 0.1;

            while(true)
            {
                var watch = new Stopwatch();
                watch.Start();
                while(watch.ElapsedMilliseconds<200)
                {
                    Errors.Enqueue(teacher.RunEpoch(Inputs, Answers));
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
        static HistoryChart history;


        static void UpdateCharts()
        {
            targetFunction.Points.Clear();
            computedFunction.Points.Clear();
            for (int i = 0; i < Inputs.Length; i++)
            {
                targetFunction.Points.Add(new DataPoint(Inputs[i][0], Answers[i][0]));
                computedFunction.Points.Add(new DataPoint(Inputs[i][0], Outputs[i]));
            }

            history.AddRange(Errors);
            double error;
            while (Errors.TryDequeue(out error));
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

            history = new HistoryChart
                    {
                        HistoryLength = 1000,
                        DataFunction =
                        {
                            Color = Color.Blue
                        },
                        Dock = DockStyle.Bottom
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
                    history
                }
            };

            new Action(Learning).BeginInvoke(null, null);

            Application.Run(form);
        }
    }
}
