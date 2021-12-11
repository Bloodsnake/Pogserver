using MySql.Data.MySqlClient;

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
            Connection.Open();
            IsConfigured = true;
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
        public class Measurement
        {
            public string PhysID { get; set; }
            public string SensorID { get; set; }
            public string Value { get; set; }
            public string Time { get; set; }
        }
        public class Sensor
        {
            public string Bezeichnung { get; set; }
            public string Hersteller { get; set; }
            public string Herstellernummer { get; set; }
            public string SensorID { get; set; }
            public string PhysID { get; set; }
            public string Seriennummer { get; set; }
            public string StandortID { get; set; }
        }
        public class Unit
        {
            public string Einheit { get; set; }
            public string Character { get; set; }
            public string Name { get; set; }
            public string PhysID { get; set; }
        }
        public class Location
        {
            public string Bezeichnung { get; set; }
            public string Standort { get; set; }
            public string StandortID { get; set; }
        }
    }
}
