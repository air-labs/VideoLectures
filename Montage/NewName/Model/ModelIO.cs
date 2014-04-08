﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using VideoLib;

namespace Editor
{
    public class ModelIO
    {



        static bool ParseV3(EditorModel model)
        {
            var file = model.VideoFolder.GetFiles(Locations.LocalFileName).FirstOrDefault();
            if (file == null) return false;
            var container = new JavaScriptSerializer().Deserialize<FileContainer>(File.ReadAllText(file.FullName));
            
            if (container.Montage!=null)
                model.Montage = container.Montage;
            if (container.WindowState!=null)
                model.WindowState=container.WindowState;
            return true;
        }

        static bool ParseV2(EditorModel model)
        {
            var file = model.VideoFolder.GetFiles(Locations.LocalFileNameV2).FirstOrDefault();
            if (file == null) return false;
            var container = new JavaScriptSerializer().Deserialize<FileContainerV2>(File.ReadAllText(file.FullName));

            if (container.Montage != null)
            {
                model.Montage.Chunks = container.Montage.Chunks;
                model.Montage.Borders = container.Montage.Borders;
                model.Montage.Information = container.Montage.Information;
                model.Montage.Shift = container.Montage.Shift;
                model.Montage.TotalLength = container.Montage.TotalLength;
                model.Montage.Intervals = new List<Interval>();
                if (container.Montage.Intervals!=null)
                foreach (var e in container.Montage.Intervals)
                    model.Montage.Intervals.Add(new Interval(
                        (int)Math.Round(e.StartTime * 1000),
                        (int)Math.Round(e.EndTime * 1000),
                        e.HasVoice));
            }
            if (container.WindowState != null)
                model.WindowState = container.WindowState;
            return true;
        }




        static bool ParseV1(EditorModel model)
        {
            var file = model.VideoFolder.GetFiles(Locations.LocalFileNameV1).FirstOrDefault();
            if (file == null) return false;
            var montageModel=new JavaScriptSerializer().Deserialize<MontageModelV1>(File.ReadAllText(file.FullName));
            
            model.Montage= new MontageModel
                {
                    Borders = montageModel.Borders,
                    Chunks = montageModel.Chunks,
                    Shift = montageModel.Shift,
                    TotalLength = montageModel.TotalLength,
                    Information = montageModel.Information
                };
            model.WindowState= new WindowState
                {
                    CurrentMode = montageModel.EditorMode,
                    CurrentPosition = montageModel.CurrentPosition,
                };
            
            if (model.Montage.Information.Episodes.Count == 0)
            {
                var titles = model.VideoFolder.GetFiles("titles.txt");
                if (titles.Length != 0)
                {
                    var lines = File.ReadAllLines(titles[0].FullName);
                    foreach (var e in lines)
                        model.Montage.Information.Episodes.Add(new EpisodInfo { Name = e });
                }
            }
            return true;
        }


        public static string DebugSubdir(string subdirectory)
        {
            if (subdirectory.StartsWith("debug\\"))
            {
                subdirectory = subdirectory.Replace("debug\\", "..\\..\\..\\..\\Model\\");
            }
            else if (subdirectory.StartsWith("work\\"))
            {
                subdirectory = subdirectory.Replace("work\\", "..\\..\\..\\..\\..\\AIML-VIDEO\\");
            }
            return subdirectory;
        }

        public static EditorModel Load(string subdirectory)
        {
            var localDirectory = new DirectoryInfo(subdirectory);
            if (!localDirectory.Exists) throw new Exception("Local directory '"+subdirectory+"' is not found");
            var rootDirectory = localDirectory;
            while (true)
            {
                try
                {
                    rootDirectory = rootDirectory.Parent;
                }
                catch
                {
                    throw new Exception("Root directory is not found. Root directory must be a parent of '"+localDirectory.FullName+"' and contain global data file '"+Locations.GlobalFileName+"'");
                }
                if (rootDirectory.GetFiles(Locations.GlobalFileName).Length!=0)
                    break;
            }

            var programFolder = new FileInfo(Assembly.GetExecutingAssembly().FullName).Directory;

            var editorModel = new EditorModel { 
                ProgramFolder = programFolder, 
                VideoFolder = localDirectory,
                ChunkFolder = localDirectory.CreateSubdirectory("chunks"),
                RootFolder = rootDirectory
            };

          
            if (!ParseV3(editorModel) && !ParseV2(editorModel) && !ParseV1(editorModel))
            {
                editorModel.Montage = new MontageModel
                    {
                        Shift = 0,
                        TotalLength = 90 * 60 * 1000 //TODO: как-то по-разумному определить это время
                    };
                editorModel.Montage.Chunks.Add(new ChunkData { StartTime = 0, Length = editorModel.Montage.TotalLength, Mode = Mode.Undefined });
            }
            return editorModel;
        }


        public static void Save(EditorModel model)
        {
            SaveV2(model);
        }

        static void SaveV2(EditorModel model)
        {
            var container = new FileContainer()
            {
                FileFormat = "V3",
                Montage = model.Montage,
                WindowState = model.WindowState
            };
            using (var stream = new StreamWriter(Path.Combine(model.VideoFolder.FullName,Locations.LocalFileName)))
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
