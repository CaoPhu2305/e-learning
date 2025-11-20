using MediaToolkit;
using MediaToolkit.Model;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Web;

namespace e_learning.Util
{
    public class VideoUtil
    {

        public static TimeSpan GetVideoDuration(string videoPath)
        {

            string physicalPath = HttpContext.Current.Server.MapPath(videoPath);

            var tmp = physicalPath;

            var inputFile = new MediaFile { Filename = physicalPath };

            using (var engine = new Engine())

            {
                engine.GetMetadata(inputFile);
            }

            return inputFile.Metadata.Duration;
        }

    }
}