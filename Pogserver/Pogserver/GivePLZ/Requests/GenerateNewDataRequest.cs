using Pogserver.GivePLZ.Payloads;
using System;
using System.Text.Json;

namespace Pogserver.GivePLZ.Requests
{
    class GenerateNewDataRequest : APIRequestPayloadBase
    {
        public override string HandleRequest(string input)
        {
            try
            {
                var request = JsonSerializer.Deserialize<GenerateNewDataRequest>(input);

                throw new System.NotImplementedException();
                Database.ExecuteCommand("");
            }
            catch
            {
                Console.WriteLine("Could not parse: " + input);
            }
            return "";
        }
    }
}