using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace Editor
{
    public class ModelIO
    {

        static EditorModel ParseV1(FileInfo file)
        {
            return new JavaScriptSerializer().Deserialize<EditorModel>(File.ReadAllText(file.FullName));
        }


        public static EditorModel Load(DirectoryInfo rootFolder, DirectoryInfo videoFolder)
        {
            if (!rootFolder.Exists)
                throw new Exception("Root directory " + rootFolder.FullName + " is not found");
            if (!videoFolder.Exists)
                throw new Exception("Video directory " + rootFolder.FullName + " is not found");

            var fileV1 = videoFolder.GetFiles("montage.editor");
            if (fileV1.Length == 1)
                return ParseV1(fileV1[0]);

            var model = new EditorModel
            {
                Shift = 0,
                TotalLength = 90 * 60 * 1000 //TODO: как-то по-разумному определить это время
            };
            model.Chunks.Add(new ChunkData { StartTime = 0, Length = model.TotalLength, Mode = Mode.Undefined });
            return model;
        }
    }
}
