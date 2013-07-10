using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

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

            var list = new StreamWriter(args[0]+"\\FileList.txt");
            var bat=new StreamWriter(args[0]+"\\AssembleLow.bat");
            bat.WriteLine("del chunks\\new*.*");
            var concat = "";
            foreach (var e in tracks)
            {
                var z=new FileInfo(e);
                bat.WriteLine("ffmpeg -i chunks\\" + z.Name+" -vcodec copy -acodec libmp3lame -ar 44100 -ab 32k chunks\\"+"new-" + z.Name);
                list.WriteLine("file 'chunks\\" + z.Name + "'");
                if (concat != "") concat += "|";
                concat += "chunks\\new-" + z.Name;
            }
            bat.WriteLine("ffmpeg -i \"concat:" + concat + "\" -c copy result.avi");
            bat.Close();
            list.Close();
            File.WriteAllText(args[0] + "\\AssemblyHigh.bat",
                "ffmpeg -f concat -i FileList.txt -qscale 0 result.avi");



        }
    }
}
