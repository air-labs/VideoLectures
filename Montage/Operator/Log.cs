using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VideoLib;

namespace Operator
{
    public static class Log
    {
        public static string FileName = "log";
        static DateTime recordStartTime;
        static DateTime lastCommitTime;
        
        public static void Start()
        {
            lastCommitTime=recordStartTime = DateTime.Now;
            MontageCommandIO.Clear(FileName);
        }

        public static void Commit(MontageAction action)
        {
            lastCommitTime = DateTime.Now;
            MontageCommandIO.AppendCommand(new MontageCommand
            {
                Action = action,
                Time = (int)(lastCommitTime - recordStartTime).TotalMilliseconds
            },FileName);
        }

        public static TimeSpan TimeFromLastCommit { get { return DateTime.Now - lastCommitTime; } }

    }
}
