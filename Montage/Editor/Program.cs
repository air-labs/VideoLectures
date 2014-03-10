﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows;
using VideoLib;
namespace Editor
{
    class Program
    {
        public static string MontageFile="montage.editor";
        public static string TimesFile="times.txt";

       
       


        [STAThread]
        public static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                MessageBox.Show("Pass the argument to the program: the directory with movies");
                return;
            }

            

            var model = ModelIO.Load(ModelIO.DebugSubdir(args[0]));
            var window = new MainWindow();
            window.Initialize(model);
            new Application().Run(window);
        }
    }
}
