using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

namespace Pogserver.GivePLZ.Payloads.Requests
{
    [APIPayload("senddata", 1)]
    class SendDataRequest : APIRequestPayloadBase
    {
        public string TypeName { get; set; }
        public override async Task<Response> HandleRequest(APIManager.APIContext ctx)
        {
            if (!Database.IsConfigured)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Cannot access Database");
                Console.ForegroundColor = ConsoleColor.White;
                return new Response(Response.ResponseStatus.Failed, "");
            }
            var request = JsonSerializer.Deserialize<SendDataRequest>(ctx.Input);
            var type = Type.GetType("Pogserver.Database+" + request.TypeName);
            var reader = Database.Read("SELECT * FROM `" + request.TypeName + "`;");

            try
            {
                List<object> response = new List<object>();

                while (reader.Read())
                {
                    var instance = Activator.CreateInstance(type);
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        foreach (var prop in instance.GetType().GetProperties())
                        {
                            if (prop.Name == reader.GetName(i))
                            {
                                instance.GetType().GetProperty(prop.Name).SetValue(instance, reader.GetValue(i).ToString());
                            }
                        }
                    }
                    response.Add(instance);
                }
                reader.Close();
                return new Response(Response.ResponseStatus.Sucess, JsonSerializer.Serialize(response));
            }
            catch (Exception e)
            {
                Console.WriteLine("Could not handle: " + ctx.Input);
                Console.WriteLine(e.Message);
                reader.Close();
            }
            return new Response(Response.ResponseStatus.Failed, "");
        }
    }
}