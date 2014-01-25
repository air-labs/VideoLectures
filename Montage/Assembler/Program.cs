using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using VideoLib;

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

         
            XDocument doc = XDocument.Load(args[0]+"\\list.xspf");

            var relative = new FileInfo(args[0]).DirectoryName + "\\";

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
                        .ToArray();

            var commands = MontageCommandIO.ReadCommands(args[0]+"\\log.txt").Commands;

            var breaks = commands
                .Where(z => z.Action == MontageAction.CommitAndSplit)
                .Select(z => z.Id)
                .ToArray();


            List<List<int>> adjacent = new List<List<int>>();
            adjacent.Add(new List<int>());

            bool isFaceNow = true;  // we start with "face"
            foreach(var command in commands) {
                switch (command.Action)
                {
                    case MontageAction.Commit:  // append if "face"
                        if (isFaceNow)
                            adjacent[adjacent.Count - 1].Add(command.Id);
                        continue;
                    case MontageAction.Face:  // append and set the flag
                        isFaceNow = true;
                        adjacent[adjacent.Count - 1].Add(command.Id);
                        continue;
                    case MontageAction.Screen:  // stop current chain and reset the flag
                        adjacent.Add(new List<int>());
                        isFaceNow = false;
                        continue;
                    case MontageAction.CommitAndSplit:  // just stop current chain
                        adjacent.Add(new List<int>());
                        continue;
                    default:  // ??? ok?
                        continue;
                }
            }
            var crossFadeBetween = adjacent.Where(a => a.Count() > 0).ToList();
            
            /*
             * CrossFade between (a,b) affects only (b)
             * it keeps b's length
             * and requires (a) just for the last frame
             */

            var resList = new List<List<string>>();
            resList.Add(new List<string>());
            int current=0;
            foreach (var e in tracks)
            {
                int number = int.Parse(e.Substring(5, 3));
                if (current < breaks.Length && number > breaks[current])
                {
                    current++;
                    resList.Add(new List<string>());
                }
                resList[resList.Count - 1].Add(e);
            }


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
    }
}
