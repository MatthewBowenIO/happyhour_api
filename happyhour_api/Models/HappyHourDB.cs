using System;
using MySql.Data.MySqlClient;

namespace happyhour_api.Models
{
    public class HappyHourDB
    {
        public string ConnectionString { get; set; }

        public HappyHourDB(string connectionString) {
            this.ConnectionString = connectionString;
        }

        private MySqlConnection GetConnection() {
            return new MySqlConnection(ConnectionString);
        }
    }
}
