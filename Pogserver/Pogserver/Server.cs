using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

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
            this.Listener.Start();
        }

        public async Task StartServer(string pageFolder)
        {
            this.IsRunning = true;
            while (this.IsRunning)
            {
                HttpListenerContext ctx = await this.Listener.GetContextAsync();

                HttpListenerRequest req = ctx.Request;
                HttpListenerResponse resp = ctx.Response;

                if (req.HttpMethod == "GET" && req.Url.AbsolutePath == "/")
                {
                    byte[] data = Encoding.UTF8.GetBytes(String.Format(File.ReadAllText(pageFolder + "/index.html")));
                    resp.ContentType = "text/html";
                    resp.ContentEncoding = Encoding.UTF8;
                    resp.ContentLength64 = data.LongLength;

                    await resp.OutputStream.WriteAsync(data, 0, data.Length);
                    resp.Close();
                }
                else if (req.HttpMethod == "GET" && req.Url.AbsolutePath == "/shutdown")
                {
                    this.IsRunning = false;
                }
            }
        }
    }
}
