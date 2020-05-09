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
    public class BaseRateDL
    {
        string connString;//= "host=localhost;port=5432;Username=postgres;Password=TMS@123;Database=App_model";      
        NpgsqlConnection conn;
        NpgsqlCommand cmd;

        public BaseRateDL()
        {
            connString = ConfigurationManager.ConnectionStrings["App_model"].ConnectionString;
        }

        public List<BaseRateBO> GetRates()
        {
            try
            {
                string sql = "dbo.fn_get_baserates";
                List<BaseRateBO> ratelist = new List<BaseRateBO>();

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
                            BaseRateBO BO = new BaseRateBO();
                            //BO.item = new List<ItemBO>();
                            //var item = new ItemBO();

                            BO.baseratekey = Guid.Parse(reader["baseratekey"].ToString());
                            BO.customerkey = Guid.Parse(reader["customerkey"].ToString());
                            BO.customername = reader["customername"].ToString();
                            BO.citykey = Guid.Parse(reader["citykey"].ToString());
                            BO.cityname = reader["cityname"].ToString();
                            BO.unitprice = Utils.CustomParse<decimal>(reader["unitprice"]);
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

        public List<BaseRateBO> GetRateByCustomer(Guid customerKey)
        {
            string sql = "dbo.fn_get_baseratebycustomer";
            List<BaseRateBO> ratelist = new List<BaseRateBO>();

            try
            {
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
                            BaseRateBO BO = new BaseRateBO();
                            //BO.item = new List<ItemBO>();
                            //var item = new ItemBO();

                            BO.baseratekey = Guid.Parse(reader["baseratekey"].ToString());
                            BO.customerkey = Guid.Parse(reader["customerkey"].ToString());
                            BO.customername = reader["customername"].ToString();
                            BO.citykey = Guid.Parse(reader["citykey"].ToString());
                            BO.unitprice = Utils.CustomParse<decimal>(reader["unitprice"]);
                            BO.cityname = reader["cityname"].ToString();
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

        public bool AddRate(BaseRateBO[] rate)
        {
            try
            {
                string sql = "dbo.fn_insert_baserate";
                conn = new NpgsqlConnection(connString);
                conn.Open();

                NpgsqlTransaction tran = conn.BeginTransaction();

                foreach (var rateBO in rate)
                {
                    using (var cmd = new NpgsqlCommand(sql, conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("_customerkey", NpgsqlTypes.NpgsqlDbType.Uuid, rateBO.customerkey);
                        cmd.Parameters.AddWithValue("_citykey", NpgsqlTypes.NpgsqlDbType.Uuid, rateBO.citykey);
                        cmd.Parameters.AddWithValue("_unitprice", NpgsqlTypes.NpgsqlDbType.Numeric, rateBO.unitprice);
                        cmd.Parameters.AddWithValue("_userkey", NpgsqlTypes.NpgsqlDbType.Uuid, rateBO.userkey);

                        var carrierKey = cmd.ExecuteScalar();
                    }
                }

                tran.Commit();
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
        public bool AddRate(BaseRateBO rateBO)
        {
            try
            {
                string sql = "dbo.fn_insert_baserate";
                conn = new NpgsqlConnection(connString);
                conn.Open();

                NpgsqlTransaction tran = conn.BeginTransaction();

                   using (var cmd = new NpgsqlCommand(sql, conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("_customerkey", NpgsqlTypes.NpgsqlDbType.Uuid, rateBO.customerkey);
                        cmd.Parameters.AddWithValue("_citykey", NpgsqlTypes.NpgsqlDbType.Uuid, rateBO.citykey);
                        cmd.Parameters.AddWithValue("_unitprice", NpgsqlTypes.NpgsqlDbType.Numeric, rateBO.unitprice);
                        cmd.Parameters.AddWithValue("_userkey", NpgsqlTypes.NpgsqlDbType.Uuid, rateBO.userkey);

                        var carrierKey = cmd.ExecuteScalar();
                    }                

                tran.Commit();
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

        public bool UpdateRate(BaseRateBO[] rate)
        {           
            try
            {
                string query = "dbo.fn_update_baserate";
                conn = new NpgsqlConnection(connString);
                conn.Open();
                NpgsqlTransaction tran = conn.BeginTransaction();

                foreach (var item in rate)
                    {
                        using (var cmd = new NpgsqlCommand(query, conn))
                        {
                            cmd.CommandType = System.Data.CommandType.StoredProcedure;

                            cmd.Parameters.AddWithValue("_baseratekey", NpgsqlTypes.NpgsqlDbType.Uuid, item.baseratekey);
                            cmd.Parameters.AddWithValue("_customerkey", NpgsqlTypes.NpgsqlDbType.Uuid, item.customerkey);
                            cmd.Parameters.AddWithValue("_citykey", NpgsqlTypes.NpgsqlDbType.Uuid, item.citykey);
                            cmd.Parameters.AddWithValue("_unitprice", NpgsqlTypes.NpgsqlDbType.Numeric, item.unitprice);
                            cmd.Parameters.AddWithValue("_userkey", NpgsqlTypes.NpgsqlDbType.Uuid, item.userkey);

                            var reader = cmd.ExecuteNonQuery();

                        }
                    }
                tran.Commit();
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

        public bool UpdateRate(BaseRateBO item)
        {
            try
            {
                string query = "dbo.fn_update_baserate";
                conn = new NpgsqlConnection(connString);
                conn.Open();
                NpgsqlTransaction tran = conn.BeginTransaction();

                  using (var cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("_baseratekey", NpgsqlTypes.NpgsqlDbType.Uuid, item.baseratekey);
                        cmd.Parameters.AddWithValue("_customerkey", NpgsqlTypes.NpgsqlDbType.Uuid, item.customerkey);
                        cmd.Parameters.AddWithValue("_citykey", NpgsqlTypes.NpgsqlDbType.Uuid, item.citykey);
                        cmd.Parameters.AddWithValue("_unitprice", NpgsqlTypes.NpgsqlDbType.Numeric, item.unitprice);
                        cmd.Parameters.AddWithValue("_userkey", NpgsqlTypes.NpgsqlDbType.Uuid, item.userkey);

                        var reader = cmd.ExecuteNonQuery();

                    }
                
                tran.Commit();
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
