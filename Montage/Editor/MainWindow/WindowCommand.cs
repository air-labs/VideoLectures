using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Editor
{
    public enum ResponseAction
    {
        None,
        Stop,
        Jump,
        Processed
    }

    public class WindowCommand
    {
        public ResponseAction Action;
        public int JumpWhere;
        public bool Invalidate;

        public WindowCommand AndInvalidate()
        {
            Invalidate = true;
            return this;
        }

        public WindowCommand To(int ms)
        {
            JumpWhere = ms;
            return this;
        }

        public static WindowCommand None { get { return new WindowCommand { Action = ResponseAction.None }; } }
        public static WindowCommand Jump { get { return new WindowCommand { Action = ResponseAction.Jump }; } }
        public static WindowCommand Stop { get { return new WindowCommand { Action = ResponseAction.Stop }; } }
        public static WindowCommand Processed { get { return new WindowCommand { Action = ResponseAction.Processed }; } }


    }
}
