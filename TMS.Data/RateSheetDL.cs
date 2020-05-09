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
    public class RateSheetDL
    {
        string connString;// = "host=localhost;port=5432;Username=postgres;Password=TMS@123;Database=App_model";      
        NpgsqlConnection conn;
        NpgsqlCommand cmd;

        public RateSheetDL()
        {
           connString = ConfigurationManager.ConnectionStrings["App_model"].ConnectionString;
        }

        public List<RateSheetBO> GetRates()
        {
           try
            {
                string sql = "dbo.fn_get_rates";
                List<RateSheetBO> ratelist = new List<RateSheetBO>();

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
                            RateSheetBO BO = new RateSheetBO();
                            //BO.item = new List<ItemBO>();
                            //var item = new ItemBO();

                            BO.ratekey = Guid.Parse(reader["ratekey"].ToString());
                            BO.customerkey = Guid.Parse(reader["customerkey"].ToString());
                            BO.customername = reader["customername"].ToString();
                            BO.itemkey = Guid.Parse(reader["itemkey"].ToString());
                            BO.unitprice = Utils.CustomParse<decimal>(reader["unitprice"]);
                            BO.description = reader["description"].ToString();
                            //    item.itemkey = Guid.Parse(reader["itemkey"].ToString());
                            //    item.unitcost = Utils.CustomParse<decimal>(reader["unitcost"]);
                            //     item.unitprice = Utils.CustomParse<decimal>(reader["unitprice"]);
                            //item.description = reader["description"].ToString();
                            // BO.item.Add(item);
                            ratelist.Add(BO);
                        }
                    }
                    while (reader.NextResult());
                    reader.Close();
                }

                return ratelist;
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

        public List<RateSheetBO> GetRateByCustomer(Guid customerKey)
        {
            try
            {
                string sql = "dbo.fn_get_ratebycustomer";
                List<RateSheetBO> ratelist = new List<RateSheetBO>();

                conn = new NpgsqlConnection(connString);
                conn.Open();

                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("_customerkey", NpgsqlTypes.NpgsqlDbType.Uuid, customerKey);
                    var reader = cmd.ExecuteReader();
                    do
                    {
                        while (reader.Read())
                        {
                            RateSheetBO BO = new RateSheetBO();
                            //BO.item = new List<ItemBO>();
                            //var item = new ItemBO();

                            BO.ratekey = Guid.Parse(reader["ratekey"].ToString());
                            BO.customerkey = Guid.Parse(reader["customerkey"].ToString());
                            BO.itemkey = Guid.Parse(reader["itemkey"].ToString());
                            BO.unitprice = Utils.CustomParse<decimal>(reader["unitprice"]);
                            BO.description = reader["description"].ToString();
                            //    item.itemkey = Guid.Parse(reader["itemkey"].ToString());
                            //    item.unitcost = Utils.CustomParse<decimal>(reader["unitcost"]);
                            //     item.unitprice = Utils.CustomParse<decimal>(reader["unitprice"]);
                            //item.description = reader["description"].ToString();
                            // BO.item.Add(item);
                            ratelist.Add(BO);
                        }
                    }
                    while (reader.NextResult());
                    reader.Close();
                }

                return ratelist;
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

        public bool AddRate(RateSheetBO rate)
        {
            try
            {
                string sql = "dbo.fn_insert_rate";
                conn = new NpgsqlConnection(connString);
                conn.Open();

                foreach (var rateBO in rate.item)
                {
                    using (var cmd = new NpgsqlCommand(sql, conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("_customerkey", NpgsqlTypes.NpgsqlDbType.Uuid, rate.customerkey);
                        cmd.Parameters.AddWithValue("_itemkey", NpgsqlTypes.NpgsqlDbType.Uuid, rateBO.itemkey);
                        cmd.Parameters.AddWithValue("_unitprice", NpgsqlTypes.NpgsqlDbType.Numeric, rateBO.unitprice);
                        cmd.Parameters.AddWithValue("_userkey", NpgsqlTypes.NpgsqlDbType.Uuid, rate.userkey);

                        var carrierKey = cmd.ExecuteScalar();
                        Guid.Parse(carrierKey.ToString());
                    }
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

        public bool UpdateRate(RateSheetBO[] rate)
        {
            try
            {
                string query = "dbo.fn_update_rate";

                conn = new NpgsqlConnection(connString);
                conn.Open();

                foreach (var item in rate)
                {
                    using (var cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("_ratekey", NpgsqlTypes.NpgsqlDbType.Uuid, item.ratekey);
                        cmd.Parameters.AddWithValue("_customerkey", NpgsqlTypes.NpgsqlDbType.Uuid, item.customerkey);
                        cmd.Parameters.AddWithValue("_itemkey", NpgsqlTypes.NpgsqlDbType.Uuid, item.itemkey);
                        cmd.Parameters.AddWithValue("_unitprice", NpgsqlTypes.NpgsqlDbType.Numeric, item.unitprice);
                        cmd.Parameters.AddWithValue("_userkey", NpgsqlTypes.NpgsqlDbType.Uuid, item.userkey);

                        var reader = cmd.ExecuteNonQuery();

                    }
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
