using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Editor;

namespace NewName.Services.Montager
{
    class ProcessingCommands
    {   

        #region Первый способ обработки
        public static IEnumerable<FFMPEGCommand> Processing1(EditorModel model, List<ChunkData> chunks)
        {
            return chunks.SelectMany(z => Commands1(model, z));
        }

        public static IEnumerable<FFMPEGCommand> Commands1(EditorModel model, ChunkData chunk)
        {
            if (chunk.Mode == Mode.Face)
            {
                yield return new ExtractFaceVideoCommand
                {
                    VideoInput = model.Locations.FaceVideo,
                    StartTime = chunk.StartTime,
                    Duration = chunk.Length,
                    VideoOutput = model.Locations.Make(model.ChunkFolder, chunk.ChunkFilename)
                };
            }
            else
            {
                yield return new ExtractAudioCommand
                {
                    AudioInput = model.Locations.FaceVideo,
                    StartTime = chunk.StartTime,
                    Duration = chunk.Length,
                    AudioOutput = model.Locations.Make(model.ChunkFolder, chunk.AudioFilename)
                };
                yield return new ExtractScreenVideoCommand
                {
                    VideoInput = model.Locations.DesktopVideo,
                    StartTime = chunk.StartTime,
                    Duration = chunk.Length,
                    VideoOutput = model.Locations.Make(model.ChunkFolder, chunk.VideoFilename)
                };
                yield return new MixVideoAudioCommand
                {
                    VideoInput = model.Locations.Make(model.ChunkFolder, chunk.VideoFilename),
                    AudioInput = model.Locations.Make(model.ChunkFolder, chunk.AudioFilename),
                    VideoOutput = model.Locations.Make(model.ChunkFolder, chunk.ChunkFilename)
                };
            }

        }
        #endregion
    }
}
