using Npgsql;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMS.BusinessObjects;

namespace TMS.Data
{
   public class CustomerDL
    {
        string connString = "host=localhost;port=5432;Username=postgres;Password=TMS@123;Database=App_model";      
        NpgsqlConnection conn;
        NpgsqlCommand cmd;

        public CustomerDL()
        {
           // connString = ConfigurationManager.ConnectionStrings["App_model"].ConnectionString;
        }
        public Guid insertCustomer(CustomerBO customer)
        {
          
           try
            {
                string sql = "dbo.fn_insert_customer";
              
                conn = new NpgsqlConnection(connString);
                conn.Open();

                NpgsqlTransaction tran = conn.BeginTransaction();

                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;                    

                    cmd.Parameters.AddWithValue("_custid", NpgsqlTypes.NpgsqlDbType.Varchar, customer.CustId);
                    cmd.Parameters.AddWithValue("_custname", NpgsqlTypes.NpgsqlDbType.Varchar, customer.CustName);                 
                    cmd.Parameters.AddWithValue("_creditlimit", NpgsqlTypes.NpgsqlDbType.Numeric, customer.CreditLimit);
                    cmd.Parameters.AddWithValue("_addrkey", NpgsqlTypes.NpgsqlDbType.Uuid, customer.addrkey);
                    //cmd.Parameters.AddWithValue("_customergroup", NpgsqlTypes.NpgsqlDbType.Smallint, 0);
                    //cmd.Parameters.AddWithValue("_creditcheck", NpgsqlTypes.NpgsqlDbType.Bit, '0');
                    cmd.Parameters.AddWithValue("_paymentterms", NpgsqlTypes.NpgsqlDbType.Smallint, customer.paymentterms);
                    if (customer.achrequired== null)
                    {
                        cmd.Parameters.AddWithValue("_ach_required", NpgsqlTypes.NpgsqlDbType.Bit, 0 );
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("_ach_required", NpgsqlTypes.NpgsqlDbType.Bit, customer.achrequired);
                    }

                    //cmd.Parameters.AddWithValue("_ach_required", NpgsqlTypes.NpgsqlDbType.Bit, customer.achrequired.ToString() == 'false' ? '0' : '1');                   
                    

                    var customerKey = cmd.ExecuteScalar();
                    tran.Commit();
                    return Guid.Parse(customerKey.ToString());
                }
            }
            catch (Exception msg)
            {
                throw msg;
            }
            finally
            {
                conn.Close();
            }
        }

        public bool updateCustomer(CustomerBO customer)
        {         
          try
            {
                string sql = "dbo.fn_update_customer";
                conn = new NpgsqlConnection(connString);
                conn.Open();

                using (var cmd = new NpgsqlCommand(sql, conn))
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
            catch (Exception msg)
            {
                throw msg;
            }
            finally
            {
                conn.Close();
            }
        }

        public List<CustomerBO> GetCustomers()
        {
            try
            {
                string sql = "dbo.fn_getcustomers";
                List<CustomerBO> customerlist = new List<CustomerBO>();
                List<string> list = new List<string>();

                conn = new NpgsqlConnection(connString);
                conn.Open();

                using (var cmd = new NpgsqlCommand(sql, conn))
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
                    reader.Close();
                }
                return customerlist;
            }

            catch (Exception msg)
            {
                throw msg;
            }
            finally
            {
                conn.Close();
            }
        }

        public CustomerBO GetCustomerbykey(Guid custKey)
        {                    
          try
            {
                string sql = "dbo.fn_getcustomerbykey";
                CustomerBO customer = new CustomerBO();

                conn = new NpgsqlConnection(connString);
                conn.Open();

                using (var cmd = new NpgsqlCommand(sql, conn))
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
                    reader.Close();
                }
                return customer;
            }
          
            catch (Exception msg)
            {
                throw msg;
            }
            finally
            {
                conn.Close();
            }
        }

        public bool GetCustomerCredit(Guid custKey, int amount)
        {
            try
            {
                string sql = "dbo.fn_get_cust_credit";

                conn = new NpgsqlConnection(connString);
                conn.Open();

                using (var cmd = new NpgsqlCommand(sql, conn))
                {

                    cmd.Parameters.AddWithValue("_custkey", custKey);
                    cmd.Parameters.AddWithValue("_amount", amount);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    var reader = cmd.ExecuteReader();
                    int result = 0;
                    while (reader.Read())
                    {
                        result = Convert.ToInt32(reader[0].ToString());
                    }
                    reader.Close();
                    return result > 0;
                }
            }
            catch (Exception msg)
            {
                throw msg;
            }
            finally
            {
                conn.Close();
            }
        }

        public Int64 GetCustomerMaxcount(string custname)
        {
           
          try
            {
                string sql = "SELECT cnt FROM (SELECT custkey, COUNT(*) AS cnt FROM dbo.tms_orderheader  GROUP BY custkey ) AS customer" +
                           " WHERE custkey = (select custkey from dbo.customer where custname=@custname)";

                conn = new NpgsqlConnection(connString);
                conn.Open();

                using (var cmd = new NpgsqlCommand(sql, conn))
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
            catch (Exception msg)
            {
                throw msg;
            }
            finally
            {
                conn.Close();
            }
        }
    }
}
