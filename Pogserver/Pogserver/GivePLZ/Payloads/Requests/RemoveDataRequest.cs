using System;
using System.Text.Json;

namespace Pogserver.GivePLZ.Payloads.Requests
{
    class RemoveDataRequest : APIRequestPayloadBase
    {
        public string ID { get; set; }
        public string Table { get; set; }
        public string VariableName { get; set; }
        public override string HandleRequest(APIManager.APIContext ctx)
        {
            Console.WriteLine("uwu");
            var request = JsonSerializer.Deserialize<RemoveDataRequest>(ctx.input);
            Console.WriteLine(request.ID);
            Console.WriteLine(request.Table);
            Console.WriteLine(request.VariableName);
            //Database.ExecuteCommand("DELETE FROM " + request.Table + " WHERE" + request.VariableName + "=" + request.ID);
            return "";
        }
    }
}
