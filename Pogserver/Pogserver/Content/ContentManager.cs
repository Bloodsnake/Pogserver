using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Pogserver.Content
{
    class ContentManager
    {
        public static readonly string Path = "/Content";
        public static Dictionary<string, IRequest> GetAllContentRequests()
        {
            var requests = new Dictionary<string, IRequest>();

            requests.Add("/", new ContentRequest("index.html"));
            requests.Add("/shutdown", new ContentRequest("shutdown.html"));
            requests.Add("/newdata", new ContentRequest("newdata.html"));
            requests.Add("/DB/units", new ContentRequest("DB/units.html"));
            requests.Add("/DB/locations", new ContentRequest("DB/locations.html"));
            requests.Add("/DB/measurements", new ContentRequest("DB/measurements.html"));
            requests.Add("/DB/sensors", new ContentRequest("DB/sensors.html"));
            requests.Add("/favicon.png", new ContentRequest("favicon.png"));
            requests.Add("/navigation", new ContentRequest("navigation.html"));
            requests.Add("/pepohappy.png", new ContentRequest("pepohappy.png"));
            requests.Add("/peposad.gif", new ContentRequest("peposad.gif"));

            var common = GetAllCommonFiles();
            common.ToList().ForEach(x => requests.Add(x.Key, x.Value));

            return requests;
        }
        public static Dictionary<string, IRequest> Verify(Dictionary<string, IRequest> input, string contentPath)
        {
            foreach (var req in input)
            {
                var request = (ContentRequest)req.Value;
                if (!File.Exists("Content/" + request.ContentPath))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Couldn't load: " + request.ContentPath);
                    Console.ForegroundColor = ConsoleColor.White;
                    input.Remove(req.Key);
                }
            }
            return input;
        }
        public static Dictionary<string, IRequest> GetAllCommonFiles()
        {
            var requests = new Dictionary<string, IRequest>();

            foreach (var file in Directory.GetFiles(Environment.CurrentDirectory + Path + "/Common/"))
            {
                var c = @"\".ToCharArray()[0];
                var path = file.Replace(Environment.CurrentDirectory + Path, "").Replace(c, '/');
                requests.Add(path, new ContentRequest(path));
            }

            return requests;
        }
    }
}
