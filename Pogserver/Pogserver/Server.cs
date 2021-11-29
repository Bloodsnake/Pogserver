using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Pogserver.API;
using Pogserver.Content;

namespace Pogserver
{
    class Server
    {
        public bool IsRunning { get; private set; }
        public string Url { get; private set; }
        public HttpListener Listener { get; private set; }
        public Server(string url = "http://localhost", uint port = 8000)
        {
            this.Url = url + ":" + port + "/";
            this.Listener = new HttpListener();

            this.Listener.Prefixes.Add(this.Url);
        }

        public async Task Run(string pageFolder, Dictionary<string, Dictionary<string, IRequest>> requests)
        {
            this.Listener.Start();
            Console.WriteLine("Server running at: " + this.Url);
            this.IsRunning = true;
            while (this.IsRunning)
            {
                var ctx = await this.Listener.GetContextAsync();

                var req = ctx.Request;
                var resp = ctx.Response;

                //Check Method
                if (!requests.ContainsKey(req.HttpMethod)) continue;
                var typeRequests = requests[req.HttpMethod];

                //Check Path
                if (!typeRequests.ContainsKey(req.Url.AbsolutePath)) continue;
                var request = typeRequests[req.Url.AbsolutePath];

                //Check Request type
                switch (request.Type)
                {
                    case IRequest.RequestType.API:
                        var apiRequest = (APIRequest)request;
                        var stream = req.InputStream;
                        var sr = new StreamReader(stream).ReadToEnd();

                        var APIresp = apiRequest.RequestObject.HandleRequest(sr);
                        var APIdata = Encoding.UTF8.GetBytes(APIresp);

                        resp.ContentEncoding = Encoding.UTF8;
                        resp.ContentLength64 = APIdata.LongLength;

                        await resp.OutputStream.WriteAsync(APIdata, 0, APIdata.Length);
                        resp.Close();

                        break;

                    case IRequest.RequestType.Content:
                        var pageRequst = (ContentRequest)request;

                        var data = Encoding.UTF8.GetBytes(File.ReadAllText(pageFolder + pageRequst.ContentPath));

                        resp.ContentType = "text/html";
                        resp.ContentEncoding = Encoding.UTF8;
                        resp.ContentLength64 = data.LongLength;

                        await resp.OutputStream.WriteAsync(data, 0, data.Length);
                        resp.Close();
                        break;

                    default:
                        Console.WriteLine("Invalid request type");
                        break;
                }
            }
        }
        public void Stop()
        {
            this.Listener.Stop();
            this.IsRunning = false;
        }
    }
}
