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
        public DirectoryInfo RootFolder { get; set; }
        public DirectoryInfo VideoFolder { get; set; }


        public EditorModel()
        {
            Montage=new MontageModel();
        }
    }
    

}
