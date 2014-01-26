﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using VideoLib;
using Montager;

namespace Assembler
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 1)
            {
                Console.WriteLine("Assembler <dir>");
                Console.ReadKey();
                return;
            }

            Directory.SetCurrentDirectory(args[0]);  // to avoid ugly arg[0]+"\\blahblah"
         
            XDocument doc = XDocument.Load("list.xspf");

            var tracks = doc
                        .Elements()
                        .Where(z => z.Name.LocalName == "playlist")
                        .Elements()
                        .Where(z => z.Name.LocalName == "trackList")
                        .Elements()
                        .Select(z => z.Elements().Where(x => x.Name.LocalName == "location").FirstOrDefault())
                        .Select(z => z.Value)
                        .Select(z => z.Substring(8, z.Length - 8))
                        .Select(z => z.Replace("/", "\\"))
                        .Select(z=> new FileInfo(z).Name)
                        .ToList();

            var log = MontageCommandIO.ReadCommands("log.txt");

            var parts = CreateParts(tracks, log);

            // TODO: serialize parts into AVS and BAT. take care of HIGH/LOW profiles.

            var batFile = new StreamWriter("AssemblyHigh.bat");
            var context = new BatchCommandContext
            {
                batFile = batFile,
                lowQuality = false
            };

            foreach (var part in parts.Parts)
            {
                part.FinalizePart();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine(String.Format("========  {0}  ========", part.PartNumber));
                Console.ForegroundColor = ConsoleColor.Gray;
                foreach (var item in part.Items)
                {
                    Console.WriteLine(item.Caption);
                    item.WriteToBatch(context);
                }
            }

            batFile.Close();

            /*

            var high = new StreamWriter(args[0] + "\\AssemblyHigh.bat");
            for (int i = 0; i < resList.Count; i++)
            {
                var list = resList[i];
                var listFile = new StreamWriter(args[0] + "\\FileList"+i.ToString()+".txt");
                var concat = "";
                foreach (var file in list)
                {
                    if (concat != "") concat += "|";
                    concat += "chunks\\new-" + file;
                    listFile.WriteLine("file 'chunks\\" + file + "'");
                }
                listFile.Close();
                high.WriteLine("ffmpeg -f concat -i FileList" + i.ToString() + ".txt -qscale 0 result-" + i.ToString() + ".avi");

            }
            high.Close();
            */
            
            //var bat=new StreamWriter(args[0]+"\\AssemblyLow.bat");
            //bat.WriteLine("del chunks\\new*.*");
            //var concat = "";
            //foreach (var e in tracks)
            //{
            //    var z=new FileInfo(e);
            //    bat.WriteLine("ffmpeg -i chunks\\" + z.Name+" -vcodec copy -acodec libmp3lame -ar 44100 -ab 32k chunks\\"+"new-" + z.Name);
            //    list.WriteLine("file 'chunks\\" + z.Name + "'");
            //    if (concat != "") concat += "|";
            //    concat += "chunks\\new-" + z.Name;
            //}
            //bat.WriteLine("ffmpeg -i \"concat:" + concat + "\" -c copy result.avi");
            //bat.Close();
            //list.Close();
            //File.WriteAllText(args[0] + "\\AssemblyHigh.bat",
            //    "ffmpeg -f concat -i FileList.txt -qscale 0 result.avi");



        }
        public static PartsList CreateParts(List<string> tracks, MontageLog log)
        {
            // chunk numbers to split after
            var breakChunkNumbers = log.Commands
                .Where(z => z.Action == MontageAction.CommitAndSplit)
                .Select(z => z.Id)
                .ToList();

            var chunks = Montager.Montager.CreateChunks(log, "", "");
            var isFace = new Dictionary<int, bool>()
            {
                {0, true}  // starts with 'face'
            };
            foreach (var chunk in chunks)
                if (!isFace.Keys.Contains(chunk.Id))
                    isFace.Add(chunk.Id, chunk.IsFaceChunk);

            var parts = new PartsList(breakChunkNumbers);
            parts.MakeParts(tracks, isFace);

            return parts;
        }


    }
}
