using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using VideoLib;

namespace Editor
{
    public class ModelIO
    {

        static EditorModel ParseV2(FileInfo file)
        {
            var container = new JavaScriptSerializer().Deserialize<FileContainer>(File.ReadAllText(file.FullName));
            return new EditorModel
            {
                Montage = container.Montage,
                WindowState = container.WindowState
            };
        }

        static EditorModel ParseV1(DirectoryInfo videoFolder, FileInfo file)
        {
            var montageModel=new JavaScriptSerializer().Deserialize<MontageModelV1>(File.ReadAllText(file.FullName));
            var model = new EditorModel()
            {
                Montage = new MontageModel
                {
                    Borders = montageModel.Borders,
                    Chunks = montageModel.Chunks,
                    Shift = montageModel.Shift,
                    TotalLength = montageModel.TotalLength,
                    Information = montageModel.Information
                },
                WindowState = new WindowState
                {
                    CurrentMode = montageModel.EditorMode,
                    CurrentPosition = montageModel.CurrentPosition,
                }
            };
            if (model.Montage.Information.Episodes.Count == 0)
            {
                var titles = videoFolder.GetFiles("titles.txt");
                if (titles.Length != 0)
                {
                    var lines = File.ReadAllLines(titles[0].FullName);
                    foreach (var e in lines)
                        model.Montage.Information.Episodes.Add(new EpisodInfo { Name = e });
                }
            }
            return model;
        }


        public static EditorModel Load(DirectoryInfo rootFolder, DirectoryInfo videoFolder)
        {
            if (!rootFolder.Exists)
                throw new Exception("Root directory " + rootFolder.FullName + " is not found");
            if (!videoFolder.Exists)
                throw new Exception("Video directory " + rootFolder.FullName + " is not found");


            EditorModel model = null;

            var fileV2 = videoFolder.GetFiles("montage.v2");
            var fileV1 = videoFolder.GetFiles("montage.editor");



            if (fileV2.Length == 1)
                model = ParseV2(fileV2[0]);
            else if (fileV1.Length == 1)
                model = ParseV1(videoFolder,fileV1[0]);
            else
            {
                model = new EditorModel
                {
                    Montage = new MontageModel
                    {
                        Shift = 0,
                        TotalLength = 90 * 60 * 1000 //TODO: как-то по-разумному определить это время
                    },
                    RootFolder = rootFolder,
                    VideoFolder = videoFolder
                };
                model.Montage.Chunks.Add(new ChunkData { StartTime = 0, Length = model.Montage.TotalLength, Mode = Mode.Undefined });
            }

            model.RootFolder = rootFolder;
            model.VideoFolder = videoFolder;
            return model;
        }


        public static void Save(EditorModel model)
        {
            SaveV2(model);
        }

        static void SaveV2(EditorModel model)
        {
            var container = new FileContainer()
            {
                FileFormat = "V2",
                Montage = model.Montage,
                WindowState = model.WindowState
            };
            using (var stream = new StreamWriter(model.VideoFolder.FullName + "\\montage.v2"))
            {
                stream.WriteLine(new JavaScriptSerializer().Serialize(container));
            }
            ExportV0(model.RootFolder, model.VideoFolder, model.Montage);
        }


        static void SaveV1(DirectoryInfo rootFolder, DirectoryInfo videoFolder, MontageModel model)
        {
            
            using (var stream = new StreamWriter(videoFolder.FullName+"\\montage.editor"))
            {
                stream.WriteLine(new JavaScriptSerializer().Serialize(model));
            }
            ExportV0(rootFolder, videoFolder, model);
        }

        static void ExportV0(DirectoryInfo rootFolder, DirectoryInfo videoFolder, MontageModel model)
        {
            File.WriteAllLines(videoFolder.FullName+"\\titles.txt", model.Information.Episodes.Select(z => z.Name).Where(z => z != null).ToArray(), Encoding.UTF8);


            var file = videoFolder.FullName+"\\log.txt";
            MontageCommandIO.Clear(file);
            MontageCommandIO.AppendCommand(new MontageCommand { Action = MontageAction.StartFace, Id = 1, Time = 0 }, file);
            MontageCommandIO.AppendCommand(new MontageCommand { Action = MontageAction.StartScreen, Id = 2, Time = model.Shift }, file);
            int id = 3;


            var list = model.Chunks.ToList();
            list.Add(new ChunkData
            {
                StartTime = list[list.Count - 1].StartTime + list[list.Count - 1].Length,
                Length = 0,
                Mode = Mode.Undefined
            });



            var oldMode = Mode.Drop;

            for (int i = 0; i < list.Count; i++)
            {
                var e = list[i];
                bool newEp = false;
                if (e.Mode != oldMode || e.StartsNewEpisode || i == list.Count - 1)
                {
                    var cmd = new MontageCommand();
                    cmd.Id = id++;
                    cmd.Time = e.StartTime;
                    switch (oldMode)
                    {
                        case Mode.Drop: cmd.Action = MontageAction.Delete; break;
                        case Mode.Face: cmd.Action = MontageAction.Commit; break;
                        case Mode.Screen: cmd.Action = MontageAction.Commit; break;
                        case Mode.Undefined: cmd.Action = MontageAction.Delete; break;
                    }
                    MontageCommandIO.AppendCommand(cmd, file);
                    oldMode = e.Mode;
                    newEp = true;
                }
                if (e.StartsNewEpisode)
                {
                    MontageCommandIO.AppendCommand(
                        new MontageCommand { Id = id++, Action = MontageAction.CommitAndSplit, Time = e.StartTime },
                        file
                        );
                    newEp = true;
                }
                if (newEp)
                {
                    if (e.Mode == Mode.Face || e.Mode == Mode.Screen)
                    {
                        var cmd = new MontageCommand();
                        cmd.Id = id++;
                        cmd.Time = e.StartTime;
                        switch (e.Mode)
                        {
                            case Mode.Face: cmd.Action = MontageAction.Face; break;
                            case Mode.Screen: cmd.Action = MontageAction.Screen; break;
                        }
                        MontageCommandIO.AppendCommand(cmd, file);
                    }

                }

            }
            
        }
    }
}
