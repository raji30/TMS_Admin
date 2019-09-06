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
        string connString = "host=localhost;Username=postgres;Password=TMS@123;Database=App_model";
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

        public Int64 GetCustomerMaxcount(string custname)
        {
            string sql = "SELECT cnt FROM (SELECT custkey, COUNT(*) AS cnt FROM dbo.tms_orderheader  GROUP BY custkey ) AS customer" +
                            " WHERE custkey = (select custkey from dbo.customer where custname=@custname)";  
            using (connection)
            {
                connection.Open();
                using (var cmd = new NpgsqlCommand(sql, connection))
                {
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Parameters.AddWithValue("custname",
                       NpgsqlTypes.NpgsqlDbType.Varchar, custname);
                    object Obj = cmd.ExecuteScalar();
                    if (Obj != null)
                    {
                        return (Int64)cmd.ExecuteScalar();
                    }
                    else return 0;
                }
            }
        }
    }
}
