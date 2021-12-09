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

            var common = GetAllCommonFiles();
            common.ToList().ForEach(x => requests.Add(x.Key, x.Value));

            return requests;
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
