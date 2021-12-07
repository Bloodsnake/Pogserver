using Pogserver.GivePLZ.Payloads;
using System;
using System.Text.Json;

namespace Pogserver.GivePLZ.Payloads.Requests
{
    class GenerateNewDataRequest : APIRequestPayloadBase
    {
        public override string HandleRequest(APIManager.APIContext ctx)
        {
            try
            {
                var request = JsonSerializer.Deserialize<GenerateNewDataRequest>(ctx.input);

                throw new System.NotImplementedException();
                Database.ExecuteCommand("");
            }
            catch
            {
                Console.WriteLine("Could not parse: " + ctx.input);
            }
            return "";
        }
    }
}