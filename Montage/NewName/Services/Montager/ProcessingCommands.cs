﻿using System;
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

        public static IEnumerable<FFMPEGCommand> Processing(EditorModel model, List<ChunkData> chunks)
        {
            return chunks.Where(c => c.IsActive).SelectMany(z => Commands(model, z));
        }

        public static IEnumerable<FFMPEGCommand> Commands(EditorModel model, ChunkData chunk)
        {
            switch (chunk.Mode)
            {
                case Mode.Face:
                    yield return new ExtractFaceVideoCommand
                    {
                        VideoInput = model.Locations.FaceVideo,
                        StartTime = chunk.StartTime,
                        Duration = chunk.Length,
                        VideoOutput = model.Locations.Make(model.ChunkFolder, chunk.ChunkFilename)
                    };
                    break;
                case Mode.Screen:
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
                    break;
            }
        }
    }
}
