﻿using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMS.BusinessObjects;

namespace TMS.Data
{
    public class RateSheetDL
    {
        string connString = "host=localhost;Username=postgres;Password=TMS@123;Database=App_model";
        NpgsqlConnection connection;

        public RateSheetDL()
        {
            connection = new NpgsqlConnection(connString);
        }

        public List<RateSheetBO> GetRates()
        {
            string sql = "dbo.fn_get_rates";
            List<RateSheetBO> ratelist = new List<RateSheetBO>();

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
                }
            }
            return ratelist;
        }

        public List<RateSheetBO> GetRateByCustomer(Guid customerKey)
        {
            string sql = "dbo.fn_get_ratebycustomer";            
            List<RateSheetBO> ratelist = new List<RateSheetBO>();

            try
            {
                using (connection)
                {
                    connection.Open();
                    using (var cmd = new NpgsqlCommand(sql, connection))
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
                    }
                }
                return ratelist;
            }
            catch
            {
                throw;
            }
        }

        public bool AddRate(RateSheetBO rate)
        {
            string sql = "dbo.fn_insert_rate";
            using (connection)
            {
                connection.Open();

                foreach(var rateBO in rate.item )
                {
                    using (var cmd = new NpgsqlCommand(sql, connection))
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
        }

        public bool UpdateRate(RateSheetBO rate)
        {
            string query = "dbo.fn_update_rate";
            using (connection)
            {
                connection.Open();
                using (var cmd = new NpgsqlCommand(query, connection))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    //cmd.Parameters.AddWithValue("_ratekey", NpgsqlTypes.NpgsqlDbType.Uuid, rate.ratekey);
                    //cmd.Parameters.AddWithValue("_customerkey", NpgsqlTypes.NpgsqlDbType.Uuid, rate.customerkey);
                    //cmd.Parameters.AddWithValue("_itemkey", NpgsqlTypes.NpgsqlDbType.Uuid, rate.item.itemkey);
                    //cmd.Parameters.AddWithValue("_unitprice", NpgsqlTypes.NpgsqlDbType.Varchar, rate.item.unitprice);
                    //cmd.Parameters.AddWithValue("_unitcost", NpgsqlTypes.NpgsqlDbType.Varchar, rate.item.unitcost);
                    //cmd.Parameters.AddWithValue("_userkey", NpgsqlTypes.NpgsqlDbType.Uuid, rate.userkey);

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


    }
}
