using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Montager.TestRun
{
    class Program
    {
        static void Main(string[] args)
        {
            var chunks=new List<Chunk>
            {
                new Chunk 
                { 
                    Id = 1, 
                    VideoSource = new ChunkSource { File = "..\\..\\..\\..\\TestFiles\\123-en.mp4", StartTime = 0, Duration = 1000 }
                },
                new Chunk 
                { 
                    Id = 2, 
                    VideoSource = new ChunkSource { File = "..\\..\\..\\..\\TestFiles\\123-en.mp4", StartTime = 1000, Duration = 1000 },
                    AudioSource = new ChunkSource { File = "..\\..\\..\\..\\TestFiles\\123-fr.mp4", StartTime = 1000, Duration = 1000 }
                },
                new Chunk 
                { 
                    Id = 3, 
                    VideoSource = new ChunkSource { File = "..\\..\\..\\..\\TestFiles\\123-en.mp4", StartTime = 2000, Duration = 1000 },
                    AudioSource = new ChunkSource { File = "..\\..\\..\\..\\TestFiles\\123-de.mp4", StartTime = 2000, Duration = 1000 }
                }
            };

            foreach (var e in Directory.GetFiles(".\\","*.mp*"))
                File.Delete(e);


            var context = new BatchCommandContext
            {
                FFMPEGPath = "C:\\ffmpeg\\bin\\ffmpeg.exe"
            };
            var ops=chunks.SelectMany(z=>z.CreateCommand()).ToList();
            foreach(var e in ops)
                e.Execute(context);

        }
    }
}
