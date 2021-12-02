using Pogserver.GivePLZ.Payloads;
using System;
using System.Text.Json;
using Microsoft.Data.Sqlite;

namespace Pogserver.GivePLZ.Requests
{
    class GenerateNewDataRequest : APIRequestPayloadBase
    {
        public override string HandleRequest(string input)
        {
            try
            {
                var request = JsonSerializer.Deserialize<GenerateNewDataRequest>(input);
                var connection = new SqliteConnection("Data Source=/Data/messwerte.sql");
                connection.Open();
                using var command = new SqliteCommand("SELECT SQLITE_VERSION()", connection);

                command.CommandText = "INSERT INTO sensors(Zahl, Zeitpunk, SensorID, MessungsID) VALUE (21, NULL, 12, 81)";
                command.ExecuteNonQuery();
                Console.WriteLine("Hoffentlich gehts");
            }
            catch
            {
                Console.WriteLine("Could not parse: " + input);
            }
            return "";
        }
    }
}