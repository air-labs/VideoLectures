using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewName
{
    public static class Shell
    {


        public static void Exec(string dir, string command, string args)
        {
            var process = new Process();
            var fullPath = Path.Combine(dir, command);
            process.StartInfo.FileName = fullPath;
            process.StartInfo.Arguments = args;
            process.StartInfo.UseShellExecute = true;
            process.Start();
            process.WaitForExit();
            if (process.ExitCode != 0)
                throw new ApplicationException(string.Format("Application returned an error code.\nApplication: {0}\nArguments:   {1}\nError code:  {2}\nCommand line to check:\n\"{0}\" {1}",
                    fullPath,
                    args,
                    process.ExitCode));
        }

        public static void Exec(FileInfo executable, string args)
        {
            Exec("", executable.FullName, args);
        }

        public static void Exec(FileInfo executable, string argumentFormat, params object[] arguments)
        {
             Exec(executable, string.Format(argumentFormat, arguments));
        }

        public static void Cmd(string argumentFormat, params object[] arguments)
        {
             Exec(Environment.SystemDirectory,"cmd", string.Format(argumentFormat,arguments));
        }

        public static void FFMPEG(string argumentFormat, params object[] arguments)
        {
             Exec(@"C:\ffmpeg\bin", "ffmpeg", string.Format(argumentFormat, arguments));
        }
    }
}
