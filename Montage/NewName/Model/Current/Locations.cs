using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Editor
{
    public class Locations
    {
        readonly EditorModel model;
        internal Locations(EditorModel model) { this.model = model; }

        FileInfo Make(DirectoryInfo info, string fname)
        {
            return new FileInfo(Path.Combine(info.FullName, fname));
        }

        public FileInfo PraatExecutable { get { return Make(model.ProgramFolder, "praatcon.exe"); } }
        public FileInfo PraatScriptSource { get { return Make(model.ProgramFolder, "split_pauses.praat"); } }
     
        
        public FileInfo FaceVideo { get { return Make(model.VideoFolder, "face.mp4"); } }
        public FileInfo DesktopVideo { get { return Make(model.VideoFolder, "desktop.avi"); } }
        public FileInfo PraatVoice { get { return Make(model.VideoFolder, "voice.mp3"); } }
        
        public FileInfo PraatOutput { get { return Make(model.VideoFolder, "praat.output"); } }

        public const string LocalFileName = "montage.v2";
        public const string LocalFileNameV1 = "montage.editor";
        public const string GlobalFileName = "montage.global.txt";
    }
}
