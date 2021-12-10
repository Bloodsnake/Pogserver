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
            try
            {
                var request = JsonSerializer.Deserialize<GenerateNewDataRequest>(ctx.input);
                var t = Type.GetType("Pogserver.Database+" + request.TypeName);
                var data = JsonSerializer.Deserialize(request.Data, t);
                foreach (var d in data.GetType().GetProperties()) Console.WriteLine(d.GetValue(data));
                //Database.ExecuteCommand("");
                /*
                //---Second Method---//
                switch (request.TypeName)
                {
                    case "Measurement":
                        Console.WriteLine(request.Data);
                        var data = JsonSerializer.Deserialize<Database.Measurement>(request.Data);
                        Console.WriteLine(data.SomeString);
                        break;
                }
                */
            }
            catch
            {
                Console.WriteLine("Could not parse: " + ctx.input);
            }
            return "";
        }
    }
}