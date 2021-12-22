using System;
using System.Collections.Generic;
using System.Text.Json;

namespace Pogserver.GivePLZ.Payloads.Requests
{
    class SendDataRequest : APIRequestPayloadBase
    {
        public string TypeName { get; set; }
        public override string HandleRequest(APIManager.APIContext ctx)
        {
            if (!Database.IsConfigured)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Cannot access Database");
                Console.ForegroundColor = ConsoleColor.White;
                return "";
            }
            try
            {
                var request = JsonSerializer.Deserialize<SendDataRequest>(ctx.input);
                var type = Type.GetType("Pogserver.Database+" + request.TypeName);
                var reader = Database.Read("SELECT * FROM `" + request.TypeName + "`;");

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
                return JsonSerializer.Serialize(response);
            }
            catch (Exception e)
            {
                Console.WriteLine("Could not handle: " + ctx.input);
                Console.WriteLine(e.Message);
            }
            return "";
        }
    }
}