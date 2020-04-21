using Npgsql;
using System;
using System.Collections.Generic;
using System.Configuration;
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
        string connString = "host=localhost;port=5432;Username=postgres;Password=TMS@123;Database=App_model";      
        NpgsqlConnection conn;
        NpgsqlCommand cmd;

        public ItemDL()
        {
           // connString = ConfigurationManager.ConnectionStrings["App_model"].ConnectionString;
        }

        public List<ItemBO> GetItems()
        {
            try
            {
                string sql = "dbo.fn_get_items";
                List<ItemBO> itemlist = new List<ItemBO>();

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
                    reader.Close();
                }

                return itemlist;
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

        public List<ItemTypeBO> GetItemTypes()
        {           
            try
            {
                string sql = "dbo.fn_getItemTypes";
                List<ItemTypeBO> itemlist = new List<ItemTypeBO>();

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
                    reader.Close();
                }

                return itemlist;
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

        public Guid InsertItem(ItemBO item)
        {           
           try
            {
                string sql = "dbo.fn_insert_tms_item";

                conn = new NpgsqlConnection(connString);
                conn.Open();

                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("_itemid", NpgsqlTypes.NpgsqlDbType.Varchar, item.itemid);
                    cmd.Parameters.AddWithValue("_description",NpgsqlTypes.NpgsqlDbType.Varchar, item.description);
                    cmd.Parameters.AddWithValue("_itemtype",  NpgsqlTypes.NpgsqlDbType.Smallint, item.itemtype);                   

                    var itemKey = cmd.ExecuteScalar();
                    return Guid.Parse(itemKey.ToString());

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

        public bool UpdateItem(ItemBO item)
        {  
            try
            {
                string query = "dbo.fn_update_tms_item";

                conn = new NpgsqlConnection(connString);
                conn.Open();

                using (var cmd = new NpgsqlCommand(query, conn))
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
                    reader.Close();
                }

                return false;
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

        public ItemBO GetItemByKey(Guid itemKey)
        {
            try
            {
                string sql = "dbo.fn_get_ItembyKey";
                ItemBO item = new ItemBO();

                conn = new NpgsqlConnection(connString);
                conn.Open();

                using (var cmd = new NpgsqlCommand(sql, conn))
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
                    reader.Close();
                }

                return item;
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
