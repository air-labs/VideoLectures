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
        Jump
    }

    public class Response
    {
        public ResponseAction Action;
        public int JumpWhere;
        public bool Invalidate;

        public Response AndInvalidate()
        {
            Invalidate = true;
            return this;
        }

        public Response To(int ms)
        {
            JumpWhere = ms;
            return this;
        }

        public static Response None { get { return new Response { Action = ResponseAction.None }; } }
        public static Response Jump { get { return new Response { Action = ResponseAction.Jump }; } }
        public static Response Stop { get { return new Response { Action = ResponseAction.Stop }; } }

    }
}
