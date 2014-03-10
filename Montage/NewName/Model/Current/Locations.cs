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

        public FileInfo FaceVideo { get { return Make(model.VideoFolder, "face.mp4"); } }
        public FileInfo DesktopVideo { get { return Make(model.VideoFolder, "desktop.avi"); } }
        public FileInfo PraatVoice { get { return Make(model.VideoFolder, "voice.mp3"); } }
        public FileInfo PraatScriptSource { get { return Make(model.VideoFolder, "split_pauses.praat"); } }
        public FileInfo PraatExecutable { get { return Make(model.VideoFolder, "praatcon.exe"); } }
        public FileInfo PraatOutput { get { return Make(model.VideoFolder, "praat.output"); } }
    }
}
