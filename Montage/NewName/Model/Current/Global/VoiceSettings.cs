using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Editor
{
    public class VoiceSettings
    {
        public int MediumSilence { get; set; }
        public int SilenceMargin { get; set; }

        public VoiceSettings()
        {
            MediumSilence = 500;
            SilenceMargin = 300;
        }
    }
}
