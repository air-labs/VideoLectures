using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Editor
{
    public class GlobalSettings
    {
        public int MaxDeviationWhenBorderingBySound { get; set; }

        public GlobalSettings()
        {
            MaxDeviationWhenBorderingBySound = 200;
        }
    }
}
