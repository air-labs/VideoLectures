using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace NewName
{
    class Test
    {
        public Guid id { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var t = new Test();
            t.id=Guid.NewGuid();
            var result=new JavaScriptSerializer().Serialize(t);
        }
    }
}
