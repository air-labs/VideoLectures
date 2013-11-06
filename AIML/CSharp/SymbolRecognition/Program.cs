using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using AForge.Neuro;
using AForge.Neuro.Learning;

namespace SymbolRecognition
{
    static class Program
    {


        static BaseMaker baseMaker = new BaseMaker();
        static double[,] percentage; 

        static void Learning()
        {


            var network = new ActivationNetwork(
                new SigmoidFunction(),
                baseMaker.InputSize,
                100,
                baseMaker.OutputSize
                );

            var teacher = new BackPropagationLearning(network);
            teacher.LearningRate = 2;
            teacher.Momentum = 0.1;

            while (true)
            {
                var watch = new Stopwatch();
                watch.Start();
                while (watch.ElapsedMilliseconds < 200)
                {
                    teacher.RunEpoch(baseMaker.Inputs, baseMaker.Answers);
                }
                watch.Stop();
                percentage = new double[baseMaker.OutputSize, baseMaker.OutputSize];
                for (int i = 0; i < 10 * baseMaker.OutputSize; i++)
                {
                    var task = baseMaker.GenerateRandom();
                    var output = network.Compute(task.Item1);
                    var max = output.Max();
                    var maxIndex = Enumerable.Range(0, output.Length).Where(z => output[z] == max).First();
                    percentage[task.Item2, maxIndex]++;
                }


            }

        }

        static void Main()
        {
            baseMaker.Generate();
            percentage = new double[baseMaker.OutputSize, baseMaker.OutputSize];
            new Action(Learning).BeginInvoke(null, null);
            //baseMaker.ShowBitmap();

            
        }
    }
}
