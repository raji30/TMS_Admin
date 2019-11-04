using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMS.BusinessObjects;

namespace TMS.Data
{
   public class AccountingDL
    {
        string connString = "host=localhost;Username=postgres;Password=TMS@123;Database=App_model";
        NpgsqlConnection connection;

        public AccountingDL()
        {
            connection = new NpgsqlConnection(connString);           
        }

        public List<AccountingBO> GetItemsbyType(int itemtype)
        {
            string sql = "dbo.fn_get_itemsbytype";
            List<AccountingBO> Details = new List<AccountingBO>();
            using (connection)
            {
                connection.Open();
                using (var cmd = new NpgsqlCommand(sql, connection))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("_itemtype",NpgsqlTypes.NpgsqlDbType.Smallint, itemtype);

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
                }
            }
            return Details;
        }

        public bool InsertAccountingOptions(List<AccountingBO> options)
        {
            string sql = "dbo.fn_insert_AccountingOptions";
            using (connection)
            {
                connection.Open();
                using (var cmd = new NpgsqlCommand(sql, connection))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;  
                    
                    foreach(var item in options)
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("_itemkey", NpgsqlTypes.NpgsqlDbType.Uuid, item.itemkey);
                        cmd.Parameters.AddWithValue("_orderdetailkey", NpgsqlTypes.NpgsqlDbType.Uuid, item.orderdetailkey);
                        cmd.Parameters.AddWithValue("_customerkey", NpgsqlTypes.NpgsqlDbType.Uuid, item.customerkey);                       
                        cmd.Parameters.AddWithValue("_createuserkey", NpgsqlTypes.NpgsqlDbType.Uuid, item.createuserkey);

                        var carrierKey = cmd.ExecuteScalar();
                    }
                    return true;
                }
            }
            return false;
        }
    }
}
