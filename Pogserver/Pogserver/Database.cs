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
            IsConfigured = true;
        }
        public static MySqlDataReader Read(string command)
        {
            if (!IsConfigured) throw new System.NotImplementedException();
            var cmd = Connection.CreateCommand();

            cmd.CommandText = command;
            cmd.ExecuteNonQuery();

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
            public string SomeString;
        }
        public class Sensor
        {

        }
        public class Unit
        {

        }
        public class Location
        {

        }
    }
}
