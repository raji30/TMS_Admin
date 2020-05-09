using Npgsql;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMS.BusinessObjects;

namespace TMS.Data
{
   public class AccountingDL
    {
        string connString;//= "host=localhost;port=5432;Username=postgres;Password=TMS@123;Database=App_model";      
        NpgsqlConnection conn;
        NpgsqlCommand cmd;


        public AccountingDL()
        {
            // connection = new NpgsqlConnection(connString); 
            connString = ConfigurationManager.ConnectionStrings["App_model"].ConnectionString;
        }

        public List<AccountingBO> GetItemsbyType(int itemtype)
        {
            try
            {
                string sql = "dbo.fn_get_itemsbytype";
                List<AccountingBO> Details = new List<AccountingBO>();

                conn = new NpgsqlConnection(connString);
                conn.Open();

                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("_itemtype", NpgsqlTypes.NpgsqlDbType.Smallint, itemtype);

                    var reader = cmd.ExecuteReader();
                    do
                    {
                        while (reader.Read())
                        {
                            var accdetail = new AccountingBO();
                            accdetail.itemkey = Utils.CustomParse<Guid>(reader["itemkey"]);
                            accdetail.itemid = Utils.CustomParse<string>(reader["itemid"]);
                            accdetail.description = Utils.CustomParse<string>(reader["description"]);
                            accdetail.itemtype = Utils.CustomParse<short>(reader["itemtype"]);
                            Details.Add(accdetail);
                        }
                    }
                    while (reader.NextResult());
                    reader.Close();
                    return Details;
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

        public bool InsertAccountingOptions(List<AccountingBO> options)
        {
            try
            {
                string sql = "dbo.fn_insert_AccountingOptions";

                conn = new NpgsqlConnection(connString);
                conn.Open();
                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    foreach (var item in options)
                    {
                        cmd.Parameters.Clear();
                       // cmd.Parameters.AddWithValue("_itemskey", NpgsqlTypes.NpgsqlDbType.Uuid, item.itemskey);
                        cmd.Parameters.AddWithValue("_itemkey", NpgsqlTypes.NpgsqlDbType.Uuid, item.itemkey);
                        cmd.Parameters.AddWithValue("_orderdetailkey", NpgsqlTypes.NpgsqlDbType.Uuid, item.orderdetailkey);
                        //cmd.Parameters.AddWithValue("_customerkey", NpgsqlTypes.NpgsqlDbType.Uuid, item.customerkey);
                        cmd.Parameters.AddWithValue("_createuserkey", NpgsqlTypes.NpgsqlDbType.Uuid, item.createuserkey);
                        var carrierKey = cmd.ExecuteScalar();
                    }
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

        public List<AccountingBO> GetAccountingOptionsbyKey(string key)
        {
            try
            {
                string sql = "dbo.fn_get_accountingoptionsbykey";
                List<AccountingBO> Details = new List<AccountingBO>();

                conn = new NpgsqlConnection(connString);
                conn.Open();

                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                   cmd.Parameters.AddWithValue("_orderdetailkey", NpgsqlTypes.NpgsqlDbType.Uuid, Guid.Parse(key));

                    var reader = cmd.ExecuteReader();
                    do
                    {
                        while (reader.Read())
                        {
                            var accdetail = new AccountingBO();
                            accdetail.itemkey = Utils.CustomParse<Guid>(reader["itemkey"]);
                            accdetail.itemskey = Utils.CustomParse<Guid>(reader["itemskey"]);
                            accdetail.orderdetailkey = Guid.Parse(key);                           
                            Details.Add(accdetail);
                        }
                    }
                    while (reader.NextResult());
                    reader.Close();
                }
                return Details;
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderdetailkey"></param>
        /// <returns></returns>
        public bool UpdateAccountingOptions(string orderdetailkey)
        {
            try
            {
                string sql = " delete from dbo.itemsforaccounting where orderdetailkey=" + " '" + orderdetailkey + "' "; 

                conn = new NpgsqlConnection(connString);
                conn.Open();
                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.CommandType = System.Data.CommandType.Text;
                   // cmd.Parameters.AddWithValue("@orderdetailkey", NpgsqlTypes.NpgsqlDbType.Uuid, Guid.Parse(orderdetailkey));
                    int returnvalue = cmd.ExecuteNonQuery();
                }
                return true;
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
