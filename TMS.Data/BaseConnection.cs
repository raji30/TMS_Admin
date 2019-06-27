using Npgsql;
using System.Configuration;

namespace TMS.Data
{
    public class BaseConnection
    {
       string connstring ;
        private NpgsqlConnection connection;
        public BaseConnection()
        {
            connstring = ConfigurationManager.AppSettings.Get("AppModelConnection");
        }

        public NpgsqlConnection OpenConnection()
        {
            if (connection != null && connection.State != System.Data.ConnectionState.Open)
            {
                connection.Open();
            }
            return connection;
        }
        public void CloseConnection()
        {
            if(connection !=null && connection.State == System.Data.ConnectionState.Open)
            {
                connection.Close();
               
            }
            
        }
    }
}