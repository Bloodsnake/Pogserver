using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace Pogserver.GivePLZ.Payloads.Requests
{
    [APIPayload("getdata", 1)]
    class AddNewDataRequest : APIRequestPayloadBase
    {
        public string TypeName { get; set; }
        public string Data { get; set; }
        public override async Task<Response> HandleRequest(APIManager.APIContext ctx)
        {
            if (!Database.IsConfigured)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Cannot access Database");
                Console.ForegroundColor = ConsoleColor.White;
                return new Response(Response.ResponseStatus.Failed, "");
            }

            var request = JsonSerializer.Deserialize<AddNewDataRequest>(ctx.Input);

            var type = Type.GetType("Pogserver.Database+" + request.TypeName);
            string vars = "";
            string vals = "";

            var data = JsonSerializer.Deserialize(request.Data, type);
            int i = 1;
            foreach (var prop in type.GetProperties()) {
                if (i == type.GetProperties().Length) vars += prop.Name;
                else vars += (prop.Name + ", ");

                Console.WriteLine(prop.GetValue(data));

                if (prop.Name.Contains("ID") || prop.GetValue(data).ToString().Contains("TIME")) vals += prop.GetValue(data);
                else vals += ("'" + prop.GetValue(data)+ "'");
                if (i != type.GetProperties().Length) vals += ",";
                i++;
            }
            Console.WriteLine($"INSERT INTO {request.TypeName} (" + vars + ") VALUES (" + vals + ");");
            Database.ExecuteCommand($"INSERT INTO {request.TypeName} (" + vars + ") VALUES (" + vals + ");");

            return new Response(Response.ResponseStatus.Sucess, "");
        }
    }
}
