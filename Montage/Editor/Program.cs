using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows;
namespace Editor
{
    class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                MessageBox.Show("Pass the argument to the program: the file to process or the directory with movies");
                return;
            }

            EditorModel model = null;
            DirectoryInfo folder = null;

            if (Directory.Exists(args[0]))
            {
                folder=new DirectoryInfo(args[0]);
                Environment.CurrentDirectory=folder.FullName;
                if (!File.Exists("times.txt"))
                {
                    MessageBox.Show("В каталоге " + folder.FullName + " должен быть файл times.txt с двумя строками: смещение второго видео относительно первого, и длина первого видео");
                    return;
                }
                using (var reader = new StreamReader("times.txt"))
                {
                    var shift = int.Parse(reader.ReadLine());
                    var length = int.Parse(reader.ReadLine());
                    model = new EditorModel { Shift = shift, TotalLength = length };
                    model.Chunks.Add(new ChunkData { StartTime = 0, Length = length, Mode = Mode.Undefined });
                    model.CurrentMode = Mode.Face;
                }
            }
            else if (File.Exists(args[0]))
            {
                var file = new FileInfo(args[0]);
                folder = file.Directory;
                Environment.CurrentDirectory = folder.FullName;
                model = new JavaScriptSerializer().Deserialize<EditorModel>(File.ReadAllText(file.FullName));
            }

            if (model == null)
            {
                MessageBox.Show("Ошибка инициализации");
                return;
            }

            var window = new MainWindow();
            window.Initialize(model,folder);
            new Application().Run(window);
        }
    }
}
