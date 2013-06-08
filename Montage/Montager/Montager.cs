using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VideoLib;

namespace Montager
{
    public class Montager
    {




        public static List<Chunk> CreateChunks(List<MontageCommand> commands, string faceFile, string screenFile)
        {
            var result = new List<Chunk>();
            if (commands[0].Action != MontageAction.Start)
                throw new Exception("Expected Start as the first command");
            int screenLag = commands[0].Time;
            
            int currentTime = screenLag;
            bool isFace=true;
            int currentId=0;


            for (int i = 1; i < commands.Count; i++)
            {
                if (commands[i].Action == MontageAction.Delete)
                {
                    currentTime= commands[i].Time;
                    continue;
                }
             
                if (isFace)
                    result.Add(new Chunk
                    {
                        Id = currentId++,
                        VideoSource = new ChunkSource
                           {
                               StartTime = currentTime,
                               Duration = commands[i].Time - currentTime,
                               File = faceFile
                           }
                    });
                else
                    result.Add(new Chunk
                    {
                        Id = currentId++,
                        VideoSource = new ChunkSource
                        {
                            StartTime = currentTime - screenLag,
                            Duration = commands[i].Time - currentTime,
                            File = screenFile
                        },
                        AudioSource = new ChunkSource
                        {
                            StartTime = currentTime,
                            Duration = commands[i].Time - currentTime,
                            File = faceFile
                        }
                    });

                currentTime = commands[i].Time;
                if (commands[i].Action == MontageAction.Face)
                    isFace = true;
                if (commands[i].Action == MontageAction.Screen)
                    isFace = false;

            }
            return result;

        }
    }
}
