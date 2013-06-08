using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace VideoLib
{
    public static class MontageCommandIO
    {
        public static void Clear(string filename)
        {
            using (var writer = new StreamWriter(filename))
            {
                WriteVersion(writer);
            }
        }

        static void WriteVersion(StreamWriter writer)
        {
            writer.WriteLine("Montager v1");
            writer.WriteLine();
        }

        static void WriteCommand(StreamWriter writer, MontageCommand command)
        {
            writer.WriteLine(command.Time.ToString());
            writer.WriteLine(command.Action.ToString());
            writer.WriteLine();
        }


        public static void WriteCommands(IEnumerable<MontageCommand> commands, string fileName)
        {
            using (var writer = new StreamWriter(fileName))
            {
                WriteVersion(writer);
                foreach (var e in commands)
                    WriteCommand(writer, e);
            }
        }

        public static void AppendCommand(MontageCommand command, string filename)
        {
            using (var writer = new StreamWriter(filename, true))
            {
                WriteCommand(writer, command);
            }
        }

        public static List<MontageCommand> ReadCommands(string filename)
        {
            var list=new List<MontageCommand>();
            using (var reader = new StreamReader(filename))
            {
                reader.ReadLine();
                reader.ReadLine();
                while (true)
                {
                    var time = reader.ReadLine();
                    if (time == null) break;
                    var action = reader.ReadLine();
                    if (action == null) break;
                    reader.ReadLine();
                    list.Add(new MontageCommand { Time = int.Parse(time), Action = (MontageAction)Enum.Parse(typeof(MontageAction), action) });
                }
            }
            return list;
        }

    }
}
