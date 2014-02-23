using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Editor
{
    public class GeneralMode : IEditorMode
    {
        EditorModel model;

        public GeneralMode(EditorModel model)
        {
            this.model = model;
        }

        public Response CheckTime(int ms)
        {
            return Response.None;
        }


        public Response MouseClick(int ms, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                var index = model.Chunks.FindChunkIndex(ms);
                if (index == -1) return Response.None;
                return Response.Jump.To(model.Chunks[index].StartTime);
            }
            else
            {
                return Response.Jump.To(ms);
            }
        }
    }
}
