using Montager;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using VideoLib;
using System.Collections.Generic;
using System.Linq;

namespace Montager.Tests
{


    [TestClass()]
    public class BatchCommandGenerator
    {
        [TestMethod()]
        public void FaceTest()
        {
            var chunk = new Chunk { Id = 23, VideoSource = new ChunkSource { StartTime = 1000, File = "face", Duration = 1000 } };
            var cmds = chunk.CreateCommand().ToList();
            Assert.AreEqual(1, cmds.Count);
            Assert.IsInstanceOfType(cmds[0], typeof(SliceVideoCommand));
            var c=(SliceVideoCommand)cmds[0];
            Assert.AreEqual("face", c.VideoInput);
            Assert.AreEqual("chunk023.mp4", c.VideoOutput);
            Assert.AreEqual(1000, c.StartTime);
            Assert.AreEqual(1000, c.Duration);

        }

        [TestMethod()]
        public void ScreenTest()
        {
            var chunk = new Chunk
            {
                Id = 23,
                VideoSource = new ChunkSource { StartTime = 1000, File = "screen", Duration = 1000 },
                AudioSource = new ChunkSource { StartTime = 2000, File = "face", Duration = 1000 }
            };
            var cmds = chunk.CreateCommand().ToList();
            Assert.AreEqual(3, cmds.Count);
            Assert.IsInstanceOfType(cmds[0], typeof(ExtractAudioCommand));
            {
                var c = (ExtractAudioCommand)cmds[0];
                Assert.AreEqual("face", c.VideoInput);
                Assert.AreEqual("audio023.mp3", c.AudioOutput);
                Assert.AreEqual(2000, c.StartTime);
                Assert.AreEqual(1000, c.Duration);
            }
            Assert.IsInstanceOfType(cmds[1], typeof(SliceVideoCommand));
            {
                var c = (SliceVideoCommand)cmds[1];
                Assert.AreEqual("screen", c.VideoInput);
                Assert.AreEqual("video023.mp4", c.VideoOutput);
                Assert.AreEqual(1000, c.StartTime);
                Assert.AreEqual(1000, c.Duration);
            }
            Assert.IsInstanceOfType(cmds[1], typeof(MixVideoAudioCommand));
            {
                var c = (MixVideoAudioCommand)cmds[1];
                Assert.AreEqual("video023.mp4", c.VideoInput);
                Assert.AreEqual("audio023.mp3", c.AudioInput);
                Assert.AreEqual("chunk023.mp4", c.VideoOutput);
            }

        }
    }
}
