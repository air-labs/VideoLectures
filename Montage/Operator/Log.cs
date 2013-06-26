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
        
        public static void Start()
        {
            recordStartTime = DateTime.Now;
            lastCommitTime = DateTime.Now;
            MontageCommandIO.Clear(FileName);
        }

        public static void Commit(MontageAction action)
        {
            lastCommitTime = DateTime.Now;
            var time=(int)(lastCommitTime - recordStartTime).TotalMilliseconds;
            var cmd=new MontageCommand
            {
                Action = action,
                Time = time 
            };
            MontageCommandIO.AppendCommand(cmd,FileName);
        }

        public static TimeSpan TimeFromLastCommit { get { return DateTime.Now - lastCommitTime; } }

    }
}
