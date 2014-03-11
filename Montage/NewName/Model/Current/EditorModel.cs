using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Editor
{

    public class EditorModel
    {
        public MontageModel Montage { get; set; }
        public WindowState WindowState { get; set; }
        public DirectoryInfo RootFolder { get; set; }
        public DirectoryInfo VideoFolder { get; set; }
        public DirectoryInfo ProgramFolder { get; set; }
        public Locations Locations { get; private set; }
        public GlobalData Global { get; set; }

        public EditorModel()
        {
            Montage=new MontageModel();
            Locations = new Locations(this);
        }


    }
    

}
