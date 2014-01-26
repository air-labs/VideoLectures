﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Montager;
using System.IO;

namespace Assembler
{
    public abstract class AviSynthCommand
    {
        public const string libraryPath = "..\\..\\library.avs";

        public abstract string Caption { get; }

        public abstract void WriteToAvs(BatchCommandContext avsContext);

        internal void WriteAvsScript(BatchCommandContext avsContext, string avsScript)
        {
            avsContext.batFile.WriteLine(avsScript);
        }

        internal string GetInput(BatchCommandContext context, string videoInput)
        {
            string input;
            if(String.IsNullOrEmpty(videoInput))
                input = "video = last";
            else
                input = String.Format(@"video = DirectShowSource(""{0}"")", 
                    Path.Combine(context.FFMPEGPath, videoInput));
            return input;
        }
    }

    public class CrossFade : AviSynthCommand
    {
        public string VideoInput = "";  // leave empty for chaining
        public string VideoPrev;
        public int EffectDuration = 500;


        public override string Caption
        {
            get { return string.Format("Cross-fade от {0} ({1})", VideoPrev, EffectDuration); }
        }

        public override void WriteToAvs(BatchCommandContext context)
        {
            var input = GetInput(context, VideoInput);
            var prev = Path.Combine(context.FFMPEGPath, VideoPrev);
            var script = String.Format(@"
                            {0}
                            prev = DirectShowSource(""{1}"")
                            return CrossFadeTime(video, prev, {2})
                          ", input, prev, EffectDuration);
            WriteAvsScript(context, script);
        }
    }

    public class FadeIn : AviSynthCommand
    {
        public string VideoInput = "";  // leave empty for chaining
        public int EffectDuration = 500;

        public override string Caption
        {
            get { return string.Format("FadeIn ({0})", EffectDuration); }
        }

        public override void WriteToAvs(BatchCommandContext context)
        {
            var input = GetInput(context, VideoInput);
            var script = String.Format(@"
                            {0}
                            return FadeInTime(video, {1})
                          ", input, EffectDuration);
            WriteAvsScript(context, script);
        }
    }

    public class FadeOut : AviSynthCommand
    {
        public string VideoInput = "";  // leave empty for chaining
        public int EffectDuration = 500;

        public override string Caption
        {
            get { return string.Format("FadeOut ({0})", EffectDuration); }
        }

        public override void WriteToAvs(BatchCommandContext context)
        {
            var input = GetInput(context, VideoInput);
            var script = String.Format(@"
                            {0}
                            return FadeOutTime(video, {1})
                          ", input, EffectDuration);
            WriteAvsScript(context, script);
        }
    }

    public class Intro : AviSynthCommand
    {
        // public string VideoInput = "";  // no input! should be the first element in a chain
        public string VideoReference;
        public int EffectDuration = 1;
        public Dictionary<string, string> Settings = new Dictionary<string, string>
        {
            {"image", "image.png"},
            {"title", "Заголовок"},
            {"description", @"длинный подзаголовок\nв несколько строк"}
            // ...
            // TODO
        };
        
        // TODO: params: text, position, fonts, etc.
        public override string Caption
        {
            get { return string.Format("Intro из {0} ({1})", Settings["image"], EffectDuration); }
        }

        public override void WriteToAvs(BatchCommandContext context)
        {
            var pathToReference = Path.Combine(context.FFMPEGPath, VideoReference);
            Settings["image"] = Path.Combine(context.FFMPEGPath, Settings["image"]);
            var paramString = String.Join(
                ", ",
                Settings.Select(pair => String.Format(@"{0}=""{1}""", pair.Key, pair.Value))
                );
            var script = String.Format(@"
                            video = DirectShowSource(""{0}"")
                            Intro(video, {1}, {2})
                          ", pathToReference, EffectDuration, paramString);
            WriteAvsScript(context, script);
        }
    }

    public class Watermark : AviSynthCommand
    {
        public string VideoInput = "";  // leave empty for chaining
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

        public override void WriteToAvs(BatchCommandContext context)
        {
            var input = GetInput(context, VideoInput);
            Settings["image"] = Path.Combine(context.FFMPEGPath, Settings["image"]);
            var paramString = String.Join(
                ", ",
                Settings.Select(pair => String.Format(@"{0}=""{1}""", pair.Key, pair.Value))
                );
            var script = String.Format(@"
                            {0}
                            return AddWatermarkPNG(video, {1})
                          ", input, paramString);
            WriteAvsScript(context, script);
        }
    }
}