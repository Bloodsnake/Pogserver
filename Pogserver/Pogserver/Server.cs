using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Pogserver.GivePLZ;
using Pogserver.Content;
using System.Linq;

namespace Pogserver
{
    class Server
    {
        #region Properties
        public bool IsRunning { get; private set; }
        public string Url { get; private set; }
        public HttpListener Listener { get; private set; }
        public Dictionary<string, Dictionary<string, IRequest>> RequestLibrary { get; set; }
        public string ContentPath { get; private set; }
        public bool PermitAllContentRequests { get; private set; }
        public string ErrorPath { get; set; }
        #endregion
        #region Config
        public Server(string url = "http://localhost", uint port = 8000)
        {
            this.Url = url + ":" + port + "/";
            this.Listener = new HttpListener();

            this.Listener.Prefixes.Add(this.Url);
        }
        public void AddErrorPath(string path)
        {
            this.ErrorPath = path;
        }
        public void AddRequestLists(Dictionary<string, Dictionary<string, IRequest>> requests)
        {
            this.RequestLibrary = requests;
        }
        public void AddContentLocation(string path)
        {
            this.ContentPath = path;
        }
        private bool ConfigIsValid()
        {
            if (string.IsNullOrEmpty(this.ContentPath)
                || this.RequestLibrary.Count() < 1
                || string.IsNullOrEmpty(this.ErrorPath))
            {
                return false;
            }
            else return true;
                
        }
        #endregion
        #region Run
        public async Task Run()
        {
            if (!this.ConfigIsValid())
            {
                throw new ConfigNotSetExeption();
            }
            this.Listener.Start();
            Console.WriteLine("Server running at: " + this.Url);
            this.IsRunning = true;
            while (this.IsRunning)
            {
                var ctx = await this.Listener.GetContextAsync();

                var req = ctx.Request;
                var resp = ctx.Response;

                //Check Method
                if (!this.RequestLibrary.ContainsKey(req.HttpMethod)) continue;
                var typeLibrary = this.RequestLibrary[req.HttpMethod];

                //Check Path
                Console.WriteLine(req.Url.AbsolutePath);
                if (!typeLibrary.ContainsKey(req.Url.AbsolutePath))
                {
                    HandleContentRequest(new ContentRequest("404.html"), resp);
                    continue;
                }
                var request = typeLibrary[req.Url.AbsolutePath];

                //Check Request type
                switch (request.Type)
                {
                    case IRequest.RequestType.API:
                        HandleApiRequest((APIRequest)request, req, resp);
                        break;

                    case IRequest.RequestType.Content:
                        HandleContentRequest((ContentRequest)request, resp);
                        break;
                    default:
                        Console.WriteLine("Invalid request type");
                        break;
                }
            }
        }
        private async Task HandleContentRequest(ContentRequest request, HttpListenerResponse response)
        {
            var data = File.ReadAllBytes(this.ContentPath + request.ContentPath);

            if (request.ContentPath.Contains(".html")) response.ContentType = "text/html";
            else if (request.ContentPath.Contains(".js")) response.ContentType = "text/javascript";
            else if (request.ContentPath.Contains(".css")) response.ContentType = "text/css";
            else if (request.ContentPath.Contains(".png")) response.ContentType = "image/png";
            else if (request.ContentPath.Contains(".gif")) response.ContentType = "image/gif";

            response.ContentEncoding = Encoding.UTF8;
            response.ContentLength64 = data.LongLength;

            await response.OutputStream.WriteAsync(data, 0, data.Length);
            response.Close();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Successful handled: " + request.ContentPath);
            Console.ForegroundColor = ConsoleColor.White;
        }
        private async Task HandleApiRequest(APIRequest request, HttpListenerRequest HTTPRequest, HttpListenerResponse response)
        {
            var stream = HTTPRequest.InputStream;
            var reader = new StreamReader(stream).ReadToEnd();

            var context = new APIManager.APIContext();
            context.Input = reader;
            context.Sender = this;

            var APIresp = await request.RequestObject.HandleRequest(context);
            var APIdata = Encoding.UTF8.GetBytes(APIresp.Result);

            Console.WriteLine(APIresp);

            response.ContentType = "text/json";
            response.ContentEncoding = Encoding.UTF8;
            response.ContentLength64 = APIdata.LongLength;

            await response.OutputStream.WriteAsync(APIdata, 0, APIdata.Length);
            response.Close();
        }
        #endregion
        public void Stop()
        {
            this.Listener.Stop();
            this.IsRunning = false;
        }
        public class ConfigNotSetExeption : Exception
        {
            public ConfigNotSetExeption()
        : base("Config was wrong")
            {
            }
        }
    }
}
