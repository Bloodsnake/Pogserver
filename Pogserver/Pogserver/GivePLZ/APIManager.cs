using Pogserver.GivePLZ.Payloads.Requests;
using System.Collections.Generic;

namespace Pogserver.GivePLZ
{
    class APIManager
    {
        public static readonly string Path = "/GivePLZ/V1/";
        public static Dictionary<string, IRequest> GetAllAPIRequests()
        {
            var requests = new Dictionary<string, IRequest>();
            requests.Add(Path + "shutdown", new APIRequest(new ShutDownRequest()));
            requests.Add(Path + "generateNewData", new APIRequest(new GenerateNewDataRequest()));
            requests.Add(Path + "sendnewdata", new APIRequest(new GenerateNewDataRequest()));
            requests.Add(Path + "getdata", new APIRequest(new SendDataRequest()));
            requests.Add(Path + "removedata", new APIRequest(new RemoveDataRequest()));
            return requests;
        }
        public class APIContext
        {
            public string input { get; set; }
            public Server sender { get; set; }
        }
    }
}
