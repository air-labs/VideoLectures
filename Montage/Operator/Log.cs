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
        static int Id;
        
        public static void Start()
        {
            recordStartTime = DateTime.Now;
            lastCommitTime = DateTime.Now;
            Id = 0;
            MontageCommandIO.Clear(FileName);
        }

        public static void Commit(MontageAction action)
        {
            lastCommitTime = DateTime.Now;
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

    }
}
