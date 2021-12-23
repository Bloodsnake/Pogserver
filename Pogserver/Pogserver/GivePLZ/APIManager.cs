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
            requests.Add(Path + "newData", new APIRequest(new AddNewDataRequest()));
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
