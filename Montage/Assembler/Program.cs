using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

            var splitAfter = GetSplitChunkIDs(args[0], "log.txt");
         
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

                
            var bat=new StreamWriter(args[0]+"\\Assemble.bat");
            bat.WriteLine("del chunks\\new*.*");

            var re = new Regex(@"^\w+?(\d+)\.\w+$");
            var concat = new List<string>();
            var concatBuffer = new List<string>();
            int fileCounter = 0;
            
            foreach (var e in tracks)
            {
                var match = re.Match(e);
                int chunkNumber = int.Parse(match.Groups[1].Value);
                var z=new FileInfo(e);
                
                bat.WriteLine("ffmpeg -i chunks\\" + z.Name+" -vcodec copy -acodec libmp3lame -ar 44100 -ab 32k chunks\\"+"new-" + z.Name);

                if (splitAfter.Count() > 0 && chunkNumber > splitAfter.First())
                {
                    concatBuffer.Add(String.Format(
                        "ffmpeg -i \"concat:{0}\" -c copy result{1:D3}.avi",
                        String.Join("|",concat.ToArray()),
                        fileCounter
                        ));
                    concat.Clear();
                    splitAfter.RemoveAt(0);
                    fileCounter++;
                }
                concat.Add("chunks\\new-" + z.Name);
                
            }
            concatBuffer.Add(String.Format(
                        "ffmpeg -i \"concat:{0}\" -c copy result{1:D3}.avi",
                        String.Join("|", concat.ToArray()),
                        fileCounter
                        ));
            foreach (var c in concatBuffer)
                if (c.Length > 0)
                    bat.WriteLine(c);
            bat.Close();
        }

        static List<int> GetSplitChunkIDs(string directory, string logFilename)
        {
            var log = VideoLib.MontageCommandIO.ReadCommands(directory + "\\" + logFilename);
            return log.Commands.Where(x => x.Action == MontageAction.CommitAndSplit).Select(x => x.Id).ToList();
        }
    }
}
