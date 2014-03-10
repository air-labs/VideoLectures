using NewName.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace NewName
{
   

    class Program
    {
        

        static void Main(string[] args)
        {
            if (args.Length < 2)
            {
                Console.WriteLine(Help);
                return;
            }

            if (args[1].StartsWith("debug\\"))
            {
                args[1] = args[1].Replace("debug\\", "");
                var path = Path.Combine(Environment.CurrentDirectory, @"..\..\..\..\Model");
                Environment.CurrentDirectory = path;
            }

            
            AppDomain.CurrentDomain.UnhandledException += (sender, a) =>
                {
                    Console.Error.WriteLine((a.ExceptionObject as Exception).Message);
                 //   Environment.Exit(1); //TODO: раскомментить эту строчку для релиза
                };

            switch (args[0].ToLower())
            {
                case "praat": new Praat().DoWork(args[1]); return;
            }

            throw new Exception("Service " + args[0] + " is not recognized");
        }



        const string Help = @"
using NewName <service> <folder>

service: one of the available services:
- praat: uses praat software, analyzes input video and searches intervals of silence and speech

folder: a folder containing video";


    }
}
