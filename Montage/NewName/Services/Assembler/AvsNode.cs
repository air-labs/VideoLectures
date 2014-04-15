using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewName.Services.Assembler
{
    internal abstract class AvsNode
    {
        protected AvsNode()
        {
            id = idCounter;
            idCounter++;
        }
        public abstract void SerializeToContext(AvsContext context);

        public string Id
        {
            get { return string.Format(template, id); }
        }

        private int id;

        private static int idCounter;
        private const string template = "var_{0}";
    }
}
