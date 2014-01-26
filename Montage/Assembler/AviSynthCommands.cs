using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Montager;

namespace Assembler
{
    public abstract class AviSynthCommand
    {
        public const string libraryPath = "library.avs";
        internal void WriteAvsScript(BatchCommandContext avsContext, string avsScript)
        {
            avsContext.batFile.WriteLine(avsScript);
        }
        public abstract string Caption { get; }
        public abstract void WriteToAvs(BatchCommandContext avsContext, string input);
    }

    public class CrossFade : AviSynthCommand
    {
        public string VideoPrev;
        public int EffectDuration = 500;

        public override string Caption
        {
            get { return string.Format("Cross-fade от {0} ({1})", VideoPrev, EffectDuration); }
        }

        public override void WriteToAvs(BatchCommandContext context, string input)
        {
            var script = String.Format(@"
                            a = DirectShowSource('{0}')
                            b = DirectShowSource('{1}')
                           return CrossFadeTime(a, b, {2})
                          ", input, VideoPrev, EffectDuration);
            WriteAvsScript(context, script);
        }
    }

    public class FadeIn : AviSynthCommand
    {
        public int EffectDuration = 500;

        public override string Caption
        {
            get { return string.Format("FadeIn ({0})", EffectDuration); }
        }

        public override void WriteToAvs(BatchCommandContext context, string input)
        {
            var script = String.Format(@"
                            video = DirectShowSource('{0}')
                            return FadeInTime(video, {1})
                          ", input, EffectDuration);
            WriteAvsScript(context, script);
        }
    }

    public class FadeOut : AviSynthCommand
    {
        public int EffectDuration = 500;

        public override string Caption
        {
            get { return string.Format("FadeOut ({0})", EffectDuration); }
        }

        public override void WriteToAvs(BatchCommandContext context, string input)
        {
            var script = String.Format(@"
                            video = DirectShowSource('{0}')
                            return FadeOutTime(video, {1})
                          ", input, EffectDuration);
            WriteAvsScript(context, script);
        }
    }

    public class Intro : AviSynthCommand
    {
        public Dictionary<string, string> Settings = new Dictionary<string, string>
        {
            {"image", "image.png"},
            {"title", "Заголовок"},
            {"subtitle", "длинный подзаголовок\nв несколько строк"}
            // ...
            // TODO
        };
        
        
        public int EffectDuration = 10*1000;
        // TODO: params: text, position, fonts, etc.
        public override string Caption
        {
            get { return string.Format("Intro из {0} ({1})", Settings["image"], EffectDuration); }
        }

        public override void WriteToAvs(BatchCommandContext context, string input)
        {
            var paramString = String.Join(
                ", ",
                Settings.Select((k, v) => String.Format("{0}='{1}'", k, v))
                );
            var script = String.Format(@"
                            video = DirectShowSource('{0}')
                            return Intro(video, {1})
                          ", input, paramString);
            WriteAvsScript(context, script);
        }
    }

    public class Watermark : AviSynthCommand
    {
        public Dictionary<string, string> Settings = new Dictionary<string, string>
        {
            {"image", "image.png"},
            {"x", "0"},
            {"y", "0"}
            // ...
            // TODO
        };
        
        // TODO: params?
        public override string Caption
        {
            get { return string.Format("Watermark из {1}", Settings["image"]); }
        }

        public override void WriteToAvs(BatchCommandContext context, string input)
        {
            var paramString = String.Join(
                ", ",
                Settings.Select((k, v) => String.Format("{0}='{1}'", k, v))
                );
            var script = String.Format(@"
                            video = DirectShowSource('{0}')
                            return AddWatermarkPNG(video, {1})
                          ", input, paramString);
            WriteAvsScript(context, script);
        }
    }
}
