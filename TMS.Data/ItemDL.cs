using Npgsql;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMS.BusinessObjects;
using TMS.Data.TableOperations;

namespace TMS.Data
{
   public class ItemDL
    {
        string connString = "host=localhost;Username=postgres;Password=TMS@123;Database=App_model";
        NpgsqlConnection connection;


        public ItemDL()
        {
            connection = new NpgsqlConnection(connString);
        }

        public List<ItemBO> GetItems()
        {
            
            string sql = "dbo.fn_get_items";
            List<ItemBO> itemlist = new List<ItemBO>();
           
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
                            var BO = new ItemBO();
                            BO.itemkey = Guid.Parse(reader["itemkey"].ToString());
                            BO.itemid = Utils.CustomParse<string>(reader["itemid"]);
                            BO.description = Utils.CustomParse<string>(reader["description"]);
                            BO.unitcost = Utils.CustomParse<decimal>(reader["unitcost"]);
                            BO.unitprice = Utils.CustomParse<decimal>(reader["unitprice"]);
                            BO.itemtype = Utils.CustomParse<short>(reader["itemtype"]);
                            itemlist.Add(BO);
                        }
                    }
                    while (reader.NextResult());
                }
            }
            return itemlist;
        }

    }
}
