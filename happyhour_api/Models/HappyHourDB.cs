using System;
using MySql.Data.MySqlClient;
using happyhour_api.Models;

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

        public void SaveUser(SlackCommandPayload payload) {
            try {
                using (MySqlConnection conn = GetConnection())
                {
                    MySqlCommand cmd = new MySqlCommand("AddUser", conn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("opt_in_time", payload.opt_in_time);
                    cmd.Parameters.AddWithValue("channel_id", payload.channel_id);
                    cmd.Parameters.AddWithValue("team_id", payload.team_id);
                    cmd.Parameters.AddWithValue("team_domain", payload.team_domain);
                    cmd.Parameters.AddWithValue("slack_user_id", payload.user_id);
                    cmd.Parameters.AddWithValue("user_name", payload.user_name);
                    cmd.Parameters.AddWithValue("webhook_url", payload.response_url);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            } catch(Exception ex){
                var that = ex.Data;
            }
        }

        public void UpdateUser(SlackCommandPayload payload)
        {
            try
            {
                using (MySqlConnection conn = GetConnection())
                {
                    MySqlCommand cmd = new MySqlCommand("UpdateUser", conn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("opt_in_time", payload.opt_in_time);;
                    cmd.Parameters.AddWithValue("slack_user_id", payload.user_id);
                    cmd.Parameters.AddWithValue("webhook_url", payload.response_url);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                var that = ex.Data;
            }
        }

        public bool DoesUserExist(SlackCommandPayload payload)
        {
            try
            {
                var returnVal = new object();
                using (MySqlConnection conn = GetConnection())
                {
                    MySqlCommand cmd = new MySqlCommand("DoesUserExist", conn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("slack_user_id", payload.user_id);
                    conn.Open();
                    returnVal = cmd.ExecuteScalar();
                    conn.Close();
                }

                if(returnVal != null) {
                    return true;
                } else {
                    return false;
                }
            }
            catch (Exception ex)
            {
                var that = ex.Data;
            }

            return false;
        }
    }
}
