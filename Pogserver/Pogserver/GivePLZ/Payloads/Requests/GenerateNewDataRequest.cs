using System;
using System.Text.Json;

namespace Pogserver.GivePLZ.Payloads.Requests
{
    class GenerateNewDataRequest : APIRequestPayloadBase
    {
        public string TypeName { get; set; }
        public string Data { get; set; }
        public override string HandleRequest(APIManager.APIContext ctx)
        {
            var request = JsonSerializer.Deserialize<GenerateNewDataRequest>(ctx.input);
            Console.WriteLine(request.Token);
            Console.WriteLine(request.TypeName);
            Console.WriteLine(request.Data);
            var uff = new GenerateNewDataRequest();
            var t = Type.GetType("Database." + request.TypeName);
            var data = JsonSerializer.Deserialize(request.Data, t);

            try
            {

                //Database.ExecuteCommand("");
            }
            catch
            {
                Console.WriteLine("Could not parse: " + ctx.input);
            }
            return "";
        }
    }
}