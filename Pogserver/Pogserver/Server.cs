using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Pogserver.API;

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

        public async Task Run(string pageFolder, Dictionary<string,IRequest> requests)
        {
            this.Listener.Start();
            Console.WriteLine("Server running at: " + this.Url);
            this.IsRunning = true;
            while (this.IsRunning)
            {
                var ctx = await this.Listener.GetContextAsync();

                var req = ctx.Request;
                var resp = ctx.Response;

                if (requests.ContainsKey(req.Url.AbsolutePath))
                {
                    var request = requests[req.Url.AbsolutePath];
                    if (request.Type == IRequest.RequestType.API)
                    {
                        var apiRequest = (APIRequest)request;
                        var stream = req.InputStream;
                        var sr = new StreamReader(stream).ReadToEnd();

                        apiRequest.RequestObject.HandleRequest(sr);
                    }
                    else if (request.Type == IRequest.RequestType.Content)
                    {
                        var pageRequst = (ContentRequest)request;
                        
                        var data = Encoding.UTF8.GetBytes(File.ReadAllText(pageFolder + pageRequst.ContentPath));
                        
                        resp.ContentType = "text/html";
                        resp.ContentEncoding = Encoding.UTF8;
                        resp.ContentLength64 = data.LongLength;

                        await resp.OutputStream.WriteAsync(data, 0, data.Length);
                        resp.Close();
                    }
                    else
                    {
                        Console.WriteLine("Unknown request type");
                    }
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
