using System.Text.Json;

namespace Pogserver.GivePLZ.Payloads.Requests
{
    class RemoveDataRequest : APIRequestPayloadBase
    {
        public string ID { get; set; }
        public string Table { get; set; }
        public override string HandleRequest(APIManager.APIContext ctx)
        {
            var request = JsonSerializer.Deserialize<RemoveDataRequest>(ctx.input);
            Database.ExecuteCommand("DELETE FROM " + request.Table + " WHERE PhysID=" + request.ID);
            return "";
        }
    }
}
