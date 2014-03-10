using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

        static EditorModel model = null;
        static DirectoryInfo folder = null;

        static int ParseMS(string s)
        {
            var parts = s.Split('.');
            var result = 0;
            if (parts.Length > 0)
                result += int.Parse(parts[parts.Length - 1]);
            if (parts.Length > 1)
                result += int.Parse(parts[parts.Length - 2]) * 1000;
            if (parts.Length > 2)
                result += int.Parse(parts[parts.Length - 3]) * 60000;
            return result;
        }

        //static bool InitFromFolder(string f)
        //{
        //    folder = new DirectoryInfo(f);
        //    if (!folder.Exists) return false;
            
        //    Environment.CurrentDirectory = folder.FullName;

        //    if (File.Exists(MontageFile))
        //        return InitFromFile(MontageFile);

        //    if (!File.Exists("times.txt"))
        //    {
        //        MessageBox.Show("В каталоге " + folder.FullName + " должен быть файл times.txt с двумя строками: смещение второго видео относительно первого, и длина первого видео");
        //        return false;
        //    }
        //    using (var reader = new StreamReader("times.txt"))
        //    {
        //        var shift = ParseMS(reader.ReadLine());
        //        var length = ParseMS(reader.ReadLine());
        //        model = new EditorModel { Shift = shift, TotalLength = length };
        //        model.Chunks.Add(new ChunkData { StartTime = 0, Length = length, Mode = Mode.Undefined });
        //    }
        //    return true;
        //}


        static bool InitFromFile(string f)
        {
            if (!File.Exists(f)) return false;
            var file = new FileInfo(f);
            folder = file.Directory;
            Environment.CurrentDirectory = folder.FullName;
            model = new JavaScriptSerializer().Deserialize<EditorModel>(File.ReadAllText(file.FullName));
            // fix for existing work
            try
            {
                model.Intervals.AddRange(SilenceSplitter.GetIntervals(SilenceSplitter.TextGridFilename));
            }
            catch {
                MessageBox.Show(String.Format("Не удалось загрузить файл {0}. Запустите Splitter.exe", SilenceSplitter.TextGridFilename));
            }
            return true;
        }

        [STAThread]
        public static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                MessageBox.Show("Pass the argument to the program: the file to process or the directory with movies");
                return;
            }

            folder=new DirectoryInfo(args[0]);

            if (!InitFromFile(args[0]+"\\montage.editor"))
            {
                model=new EditorModel
                {
                    Shift=0,
                    TotalLength = 90*60*1000 //TODO: как-то по-разумному определить это время
                };
                model.Chunks.Add(new ChunkData { StartTime=0, Length=model.TotalLength, Mode= Mode.Undefined });      
            }

            Environment.CurrentDirectory = folder.FullName;
            var window = new MainWindow();
            window.Initialize(model,folder);
            new Application().Run(window);
        }
    }
}
