using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS.Data
{
   public class CustomerDL
    {
        string connString = "host=localhost;Username=postgres;Password=Abc1234!;Database=App_model";
        private NpgsqlConnection connection;
        public CustomerDL()
        {
            connection = new NpgsqlConnection();
            connection.ConnectionString = connString;
            
        }
        public bool GetCustomerCredit(Guid custKey, int amount)
        {
            connection.Open();
            string sql = "dbo.fn_get_cust_credit";
            using (var cmd = new NpgsqlCommand(sql, connection))
            {

                cmd.Parameters.AddWithValue("_custkey", custKey);
                cmd.Parameters.AddWithValue("_amount", amount);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                var reader = cmd.ExecuteReader();
                int result= 0;
                while (reader.Read())
                {
                    result= Convert.ToInt32(reader[0].ToString());
                }
                    connection.Close();
                return result>0;
            }
        }
    }
}
