using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VideoLib;

namespace Operator
{
    public static class Log
    {
        public static string FileName = "log.txt";
        static DateTime recordStartTime;
        static DateTime lastCommitTime;
        static DateTime goodSplitTime;
        static DateTime goodStartTime;
        static int Id;
        
        public static void Start()
        {
            recordStartTime = DateTime.Now;
            lastCommitTime = DateTime.Now;
            goodSplitTime = DateTime.Now;
            goodStartTime = DateTime.Now;
            Id = 0;
            MontageCommandIO.Clear(FileName);
        }

        public static void Commit(MontageAction action)
        {
            if (action == MontageAction.Delete)
            {
                // deal with current recordStartTime
                var currentLength = (DateTime.Now - lastCommitTime).TotalMilliseconds;
                goodSplitTime = goodSplitTime.AddMilliseconds(currentLength);
                goodStartTime = goodStartTime.AddMilliseconds(currentLength);
            }

            lastCommitTime = DateTime.Now;

            if (action == MontageAction.StartScreen)
            {
                // to show 00:00 at start
                goodStartTime = DateTime.Now;
                goodSplitTime = DateTime.Now;
            }

            if(action == MontageAction.CommitAndSplit)
                goodSplitTime = DateTime.Now;
            
            var time=(int)(lastCommitTime - recordStartTime).TotalMilliseconds;
            var cmd=new MontageCommand
            {
                Action = action,
                Time = time ,
                Id = Log.Id,
            };
            MontageCommandIO.AppendCommand(cmd,FileName);
            Id++;
        }

        public static TimeSpan TimeFromLastCommit { get { return DateTime.Now - lastCommitTime; } }

        public static TimeSpan TimeGoodFromLastSplit { get { return DateTime.Now - goodSplitTime; } }

        public static TimeSpan TimeGoodFromStart { get { return DateTime.Now - goodStartTime; } }

    }
}
