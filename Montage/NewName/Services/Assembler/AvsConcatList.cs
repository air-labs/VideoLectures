using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewName.Services.Assembler
{
    class AvsConcatList : AvsNode
    {

        public List<AvsNode> Items { get; set; }

        public override void SerializeToContext(AvsContext context)
        {
            Items.ForEach(item => item.SerializeToContext(context));
            var allItems = string.Join(" + ", Items.Select(item => item.Id));
            context.AddData(string.Format(Format, Id, allItems));
        }

        private string Format { get { return "{0} = {1}"; } }
    }
}