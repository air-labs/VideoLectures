using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows;
namespace Editor
{
    class Program
    {
        public static string MontageFile="montage.editor";
        public static string TimesFile="times.txt";

       
        #region Obsolete

        //static int ParseMS(string s)
        //{
        //    var parts = s.Split('.');
        //    var result = 0;
        //    if (parts.Length > 0)
        //        result += int.Parse(parts[parts.Length - 1]);
        //    if (parts.Length > 1)
        //        result += int.Parse(parts[parts.Length - 2]) * 1000;
        //    if (parts.Length > 2)
        //        result += int.Parse(parts[parts.Length - 3]) * 60000;
        //    return result;
        //}

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
        //        model.CurrentMode = Mode.Face;
        //    }
        //    return true;
        //}

        //static bool InitFromFile(string f)
        //{
        //    if (!File.Exists(f)) return false;
           
        //    model = new JavaScriptSerializer().Deserialize<EditorModel>(File.ReadAllText(file.FullName));
        //    return true;
        //}
        #endregion

        [STAThread]
        public static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                MessageBox.Show("Pass the argument to the program: the directory with movies");
                return;
            }

            var rootFolder = new FileInfo(Assembly.GetExecutingAssembly().Location).Directory;
            var videoFolder = new DirectoryInfo(rootFolder.FullName + "\\" + args[0]);
            Environment.CurrentDirectory = videoFolder.FullName;
            var model = ModelIO.Load(rootFolder, videoFolder);
            var window = new MainWindow();
            window.Initialize(model);
            new Application().Run(window);
        }
    }
}
