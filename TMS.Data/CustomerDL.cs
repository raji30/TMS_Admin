using Npgsql;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMS.BusinessObjects;

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
        public Guid insertCustomer(CustomerBO customer)
        {
            string sql = "dbo.fn_insert_customer";
            using (connection)
            {
                connection.Open();
                using (var cmd = new NpgsqlCommand(sql, connection))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;                    

                    cmd.Parameters.AddWithValue("_custid", NpgsqlTypes.NpgsqlDbType.Varchar, customer.CustId);
                    cmd.Parameters.AddWithValue("_custname", NpgsqlTypes.NpgsqlDbType.Varchar, customer.CustName);                 
                    cmd.Parameters.AddWithValue("_creditlimit", NpgsqlTypes.NpgsqlDbType.Numeric, customer.CreditLimit);
                    cmd.Parameters.AddWithValue("_addrkey", NpgsqlTypes.NpgsqlDbType.Uuid, customer.addrkey);
                    //cmd.Parameters.AddWithValue("_customergroup", NpgsqlTypes.NpgsqlDbType.Smallint, 0);
                    //cmd.Parameters.AddWithValue("_creditcheck", NpgsqlTypes.NpgsqlDbType.Bit, '0');
                    cmd.Parameters.AddWithValue("_paymentterms", NpgsqlTypes.NpgsqlDbType.Smallint, customer.paymentterms);
                    cmd.Parameters.AddWithValue("_ach_required", NpgsqlTypes.NpgsqlDbType.Bit, customer.achrequired);                   
                    

                    var customerKey = cmd.ExecuteScalar();
                    return Guid.Parse(customerKey.ToString());
                }
            }
        }

        public bool updateCustomer(CustomerBO customer)
        {
            string sql = "dbo.fn_update_customer";
            using (connection)
            {
                connection.Open();
                using (var cmd = new NpgsqlCommand(sql, connection))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("_custkey", NpgsqlTypes.NpgsqlDbType.Uuid, customer.CustomerKey);
                    cmd.Parameters.AddWithValue("_custid", NpgsqlTypes.NpgsqlDbType.Varchar, customer.CustId);
                    cmd.Parameters.AddWithValue("_custname", NpgsqlTypes.NpgsqlDbType.Varchar, customer.CustName);
                    cmd.Parameters.AddWithValue("_status", NpgsqlTypes.NpgsqlDbType.Smallint, customer.Status);
                    cmd.Parameters.AddWithValue("_creditlimit", NpgsqlTypes.NpgsqlDbType.Numeric, customer.CreditLimit); 
                   // cmd.Parameters.AddWithValue("_creditcheck", NpgsqlTypes.NpgsqlDbType.Varchar, customer.CreditCheck);
                    cmd.Parameters.AddWithValue("_paymentterms", NpgsqlTypes.NpgsqlDbType.Smallint, customer.paymentterms);
                    cmd.Parameters.AddWithValue("_ach_required", NpgsqlTypes.NpgsqlDbType.Bit, customer.achrequired);

                    var customerKey = cmd.ExecuteNonQuery();
                    return true;
                }
            }
        }

        public List<CustomerBO> GetCustomers()
        {
            string sql = "dbo.fn_getcustomers";
            List<CustomerBO> customerlist = new List<CustomerBO>();
            List<string> list = new List<string>();
            using (connection)
            {
                connection.Open();
                using (var cmd = new NpgsqlCommand(sql, connection))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    var reader = cmd.ExecuteReader();
                    do
                    {
                        while (reader.Read())
                        {  
                            var customer = new CustomerBO();

                            customer.CustomerKey = Utils.CustomParse<Guid>(reader["custkey"]);
                            customer.CustId = Utils.CustomParse<string>(reader["custid"]);
                            customer.CustName = Utils.CustomParse<string>(reader["custname"]);
                            customer.addrkey = Utils.CustomParse<Guid>(reader["addrkey"]);
                            // var creditchk = Utils.CustomParse<short>(["creditcheck"]);

                            customer.CreditCheck = reader.GetBoolean(reader.GetOrdinal("creditcheck"));
                            customer.achrequired = reader.GetBoolean(reader.GetOrdinal("ach_required"));                            

                            customer.CreditLimit = Utils.CustomParse<decimal>(reader["creditlimit"]);                        
                            customer.paymentterms = Utils.CustomParse<short>(reader["paymentterms"]);
                            customer.Status = Utils.CustomParse<short>(reader["status"]);

                            customerlist.Add(customer);
                        }
                    }
                    while (reader.NextResult());
                }
            }
            return customerlist;
        }

        public CustomerBO GetCustomerbykey(Guid custKey)
        {
            string sql = "dbo.fn_getcustomerbykey";
            CustomerBO customer = new CustomerBO();
           
            using (connection)
            {
                connection.Open();
                using (var cmd = new NpgsqlCommand(sql, connection))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("_custkey", NpgsqlTypes.NpgsqlDbType.Uuid, custKey);

                    var reader = cmd.ExecuteReader();
                    do
                    {
                        while (reader.Read())
                        {  
                            customer.CustomerKey = Utils.CustomParse<Guid>(reader["custkey"]);
                            customer.CustId = Utils.CustomParse<string>(reader["custid"]);
                            customer.CustName = Utils.CustomParse<string>(reader["custname"]);
                            customer.addrkey = Utils.CustomParse<Guid>(reader["addrkey"]);
                            customer.CreditCheck = reader.GetBoolean(reader.GetOrdinal("creditcheck"));
                            customer.achrequired = reader.GetBoolean(reader.GetOrdinal("ach_required"));                            
                            customer.CreditLimit = Utils.CustomParse<decimal>(reader["creditlimit"]);                          
                            customer.paymentterms = Utils.CustomParse<short>(reader["paymentterms"]);
                            customer.Status = Utils.CustomParse<short>(reader["status"]);                           
                        }
                    }
                    while (reader.NextResult());
                }
            }
            return customer;
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
