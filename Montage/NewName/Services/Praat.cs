using Editor;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewName.Services
{
    public class Praat
    {
        const string SilentLabel = "--";
        const string SoundLabel = "++";

        const double MinPitch = 100;
        const double TimeStep = 0;

        const double SilenceThreshold = -27;
        const double MinSilentInterval = 0.5;
        const double MinSoundInterval = 0.1;

        public void DoWork(string folder)
        {
            var model = ModelIO.Load(folder);

            if (model.Locations.PraatVoice.Exists) model.Locations.PraatVoice.Delete();

            Shell.FFMPEG("-i \"{0}\" -vn -acodec copy \"{1}\"", model.Locations.FaceVideo, model.Locations.PraatVoice);

            Shell.Exec(model.Locations.PraatExecutable, 
                String.Format(
                    CultureInfo.InvariantCulture,
                    "\"{0}\" \"{1}\" \"{2}\" {3} {4} {5} {6} {7} {8} {9}", 
                    model.Locations.PraatScriptSource,
                    model.Locations.PraatVoice,
                    model.Locations.PraatOutput,
                    SilentLabel, 
                    SoundLabel, 
                    MinPitch, 
                    TimeStep, 
                    SilenceThreshold, 
                    MinSilentInterval, 
                    MinSoundInterval));

            model.Montage.Intervals = new List<Interval>();
            using (var reader = new StreamReader(model.Locations.PraatOutput.FullName))
            {

                for (var i = 0; i < 11; i++)
                    reader.ReadLine();

                var intervalCount = int.Parse(reader.ReadLine());
                for (int i = 0; i < intervalCount; i++)
                {
                    var startTime = double.Parse(reader.ReadLine(), CultureInfo.InvariantCulture);
                    var endTime = double.Parse(reader.ReadLine(), CultureInfo.InvariantCulture);
                    var hasVoice = reader.ReadLine() == '"' + SoundLabel + '"';
                    model.Montage.Intervals.Add(new Interval(startTime, endTime, hasVoice));
                }
            }

            ModelIO.Save(model);
            
        }
    }
}
