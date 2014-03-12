using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Editor;

namespace ModelCreator
{
    class Program
    {
        static void Main(string[] args)
        {
            var path=ModelIO.DebugSubdir("debug\\20");
            Directory.CreateDirectory(path);
            File.Delete(path + "\\montage.v2");
            var model = ModelIO.Load(path);
            model.Montage.Intervals=new List<Interval>();
            for (int i = 0; i < 10; i++)
            {
                model.Montage.Intervals.Add(new Interval(10 * i, 10 * i + 8, true));
                model.Montage.Intervals.Add(new Interval(10 * i+8, 10 * i + 10, false));

            }
            ModelIO.Save(model);
        }
    }
}
