using MySql.Data.MySqlClient;
using System;

namespace Pogserver
{
    class Database
    {
        private static MySqlConnection Connection { get; set; }
        public static bool IsConfigured { get; set; }
        public static void Configure(string parameters = "host=localhost;user=root;password='';database=messwerte;")
        {
            if (IsConfigured) return;

            Connection = new MySqlConnection(parameters);
            try
            {
                Connection.Open();
                IsConfigured = true;
            }
            catch
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Connot connect to Database");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
        public static MySqlDataReader Read(string command)
        {
            if (!IsConfigured) throw new System.NotImplementedException();
            var cmd = Connection.CreateCommand();

            cmd.CommandText = command;

            return cmd.ExecuteReader();
        }
        public static void ExecuteCommand(string command)
        {
            var cmd = Connection.CreateCommand();

            cmd.CommandText = command;
            cmd.ExecuteNonQuery();
        }
        public class Measurements
        {
            public string MessungsID { get; set; }
            public string PhysID { get; set; }
            public string SensorID { get; set; }
            public string Wert { get; set; }
            public string Zeitpunkt { get; set; }
        }
        public class Sensors
        {
            public string Bezeichnung { get; set; }
            public string Hersteller { get; set; }
            public string Herstellernummer { get; set; }
            public string SensorID { get; set; }
            public string PhysID { get; set; }
            public string Seriennummer { get; set; }
            public string StandortID { get; set; }
        }
        public class Units
        {
            public string Einheit { get; set; }
            public string Zeichen { get; set; }
            public string Name { get; set; }
            public string PhysID { get; set; }
        }
        public class Locations
        {
            public string Bezeichnung { get; set; }
            public string KoordinateX { get; set; }
            public string KoordinateY { get; set; }
            public string StandortID { get; set; }
        }
    }
}
