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
                            //BO.unitcost = Utils.CustomParse<string>(reader["unitcost"]);
                            //BO.unitprice = Utils.CustomParse<string>(reader["unitprice"]);
                            BO.itemtype = Utils.CustomParse<short>(reader["itemtype"]);
                            itemlist.Add(BO);
                        }
                    }
                    while (reader.NextResult());
                }
            }
            return itemlist;
        }

        public List<ItemTypeBO> GetItemTypes()
        {

            string sql = "dbo.fn_getItemTypes";
            List<ItemTypeBO> itemlist = new List<ItemTypeBO>();

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
                            var BO = new ItemTypeBO();
                            BO.itemtypekey = Guid.Parse(reader["itemtypekey"].ToString());
                            BO.itemtypeid = Utils.CustomParse<short>(reader["itemtypeid"]);
                            BO.description = Utils.CustomParse<string>(reader["description"]);
                            //BO.createdate = Utils.CustomParse<DateTime>(reader["createdate"]);
                           // BO.createuserkey = Utils.CustomParse<Guid>(reader["createuserkey"]);
                           
                            itemlist.Add(BO);

                        }
                    }
                    while (reader.NextResult());
                }
            }
            return itemlist;
        }

        public Guid InsertItem(ItemBO item)
        {
            string sql = "dbo.fn_insert_tms_item";
            using (connection)
            {
                connection.Open();
                using (var cmd = new NpgsqlCommand(sql, connection))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("_itemid", NpgsqlTypes.NpgsqlDbType.Varchar, item.itemid);
                    cmd.Parameters.AddWithValue("_description",NpgsqlTypes.NpgsqlDbType.Varchar, item.description);
                    cmd.Parameters.AddWithValue("_itemtype",  NpgsqlTypes.NpgsqlDbType.Smallint, item.itemtype);                   

                    var itemKey = cmd.ExecuteScalar();
                    return Guid.Parse(itemKey.ToString());

                }
            }

        }

        public bool UpdateItem(ItemBO item)
        {           
            string query = "dbo.fn_update_tms_item";
            using (connection)
            {
                connection.Open();
                using (var cmd = new NpgsqlCommand(query, connection))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("_itemkey", NpgsqlTypes.NpgsqlDbType.Uuid, item.itemkey);
                    cmd.Parameters.AddWithValue("_itemid", NpgsqlTypes.NpgsqlDbType.Varchar, item.itemid);
                    cmd.Parameters.AddWithValue("_description", NpgsqlTypes.NpgsqlDbType.Varchar, item.description);
                    cmd.Parameters.AddWithValue("_itemtype", NpgsqlTypes.NpgsqlDbType.Smallint, item.itemtype);

                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        var result = bool.Parse(reader[0].ToString());
                        return result;
                    }
                }
            }
            return false;
        }

        public ItemBO GetItemByKey(Guid itemKey)
        {
            string sql = "dbo.fn_get_ItembyKey";
            ItemBO item = new ItemBO();

            try
            {

                using (connection)
                {
                    connection.Open();
                    using (var cmd = new NpgsqlCommand(sql, connection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("_itemkey", NpgsqlTypes.NpgsqlDbType.Uuid, itemKey);
                        var reader = cmd.ExecuteReader();
                        do
                        {
                            while (reader.Read())
                            {
                                item.itemkey = Utils.CustomParse<Guid>(reader["itemkey"]);
                                item.itemid = Utils.CustomParse<string>(reader["itemid"]);
                                item.itemtype = Utils.CustomParse<short>(reader["itemtype"]);
                                item.description = Utils.CustomParse<string>(reader["description"]);                                                         
                            }
                        }
                        while (reader.NextResult());
                    }
                }
                return item;
            }
            catch
            {
                throw;
            }
        }
    }
}
