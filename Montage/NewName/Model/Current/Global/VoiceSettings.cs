using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Editor
{
    public class VoiceSettings
    {
        public int MaxDeviationWhenBorderingBySound { get; set; }
        public int SilenceMargin { get; set; }

        public VoiceSettings()
        {
            MaxDeviationWhenBorderingBySound = 200;
            SilenceMargin = 50;
        }
    }
}
