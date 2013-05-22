using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MontageLib
{
    public class Montager
    {
        List<string> FaceChunks;
        List<string> DisplayChunks;
        List<string> AllChunks;
        List<BatchCommand> CutCommands;

        public static void Montage(string faceFile, string displayFile, List<MontageCommand> cmds)
        {

        }
        
        
        static IEnumerable<BatchCommand> CutMontage(string faceFile, string displayFile, List<MontageCommand> cmds)
        {
            int startTime = 0;
            MontageAction currentAction= MontageAction.Face;
            int chunkNum=0;
            foreach (var e in cmds)
            {
                var chunkName=string.Format("Chunk_{0:D3}",chunkNum);


                var action = e.Action;
                if (action == MontageAction.Commit) action = currentAction;

                switch (action)
                {
                    case MontageAction.Delete: break;
                    case MontageAction.Face:
                        yield return new SubVideoCommand(faceFile, chunkName, startTime, e.Time);
                        yield return new SRTCommand(chunkName, startTime, e.Time);
                        chunkNum++;
                        break;
                    case MontageAction.Display:
                        yield return new SubVideoCommand(faceFile, chunkName + "_TMP1", startTime, e.Time);
                        yield return new ExtractAudioCommand(chunkName + "_TMP", chunkName);
                        yield return new SubVideoCommand(displayFile, chunkName + "TMP2", startTime, e.Time);
                        yield return new AddAudioCommand(chunkName, chunkName + "_TMP2", chunkName);
                        yield return new FileDeleteCommand(chunkName + "_TMP1.mp4");
                        yield return new FileDeleteCommand(chunkName + "_TMP2.mp4");
                        yield return new FileDeleteCommand(chunkName + ".mp3");
                        yield return new SRTCommand(chunkName, startTime, e.Time);
                        chunkNum++;
                        break;
                }

                startTime = e.Time;
                if (action != MontageAction.Delete) currentAction = e.Action;
            }
        }
    }
}