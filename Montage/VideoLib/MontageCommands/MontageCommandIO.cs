﻿using System;
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
                WriteFSync(writer);
            }
        }

        static void WriteVersion(StreamWriter writer)
        {
            writer.WriteLine("Montager v1");
            writer.WriteLine();
        }

        static void WriteFSync(StreamWriter writer, int sync=0)
        {
            writer.WriteLine("Please write the time of sync signal in face video in the next line");
            writer.WriteLine(sync);
            writer.WriteLine();
        }


        static void WriteCommand(StreamWriter writer, MontageCommand command)
        {
            writer.WriteLine(command.Time.ToString());
            writer.WriteLine(command.Action.ToString());
            writer.WriteLine();
        }


        public static void WriteCommands(MontageLog log, string fileName)
        {
            using (var writer = new StreamWriter(fileName))
            {
                WriteVersion(writer);
                WriteFSync(writer, log.FaceFileSync);
                foreach (var e in log.Commands)
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

        public static MontageLog ReadCommands(string filename)
        {
            var log = new MontageLog();
            using (var reader = new StreamReader(filename))
            {
                reader.ReadLine(); //version
                reader.ReadLine(); //empty line
                reader.ReadLine(); //fsync prompt
                log.FaceFileSync = int.Parse(reader.ReadLine());
                reader.ReadLine(); //empty line
                while (true)
                {
                    var time = reader.ReadLine();
                    if (time == null) break;
                    var action = reader.ReadLine();
                    if (action == null) break;
                    reader.ReadLine();
                    log.Commands.Add(new MontageCommand { Time = int.Parse(time), Action = (MontageAction)Enum.Parse(typeof(MontageAction), action) });
                }
            }
            return log;
        }

    }
}
