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
    public class CityDL
    {
        string connString;//= "host=localhost;Username=postgres;Password=TMS@123;Database=App_model";      
        NpgsqlConnection conn;
        NpgsqlCommand cmd;

        public CityDL()
        {
            connString = ConfigurationManager.ConnectionStrings["App_model"].ConnectionString;
        }

        public Guid InsertCity(CityBO city)
        {            
           try
            {
                string sql = "dbo.fn_insert_city";               

                conn = new NpgsqlConnection(connString);
                conn.Open();
                NpgsqlTransaction tran = conn.BeginTransaction();

                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("_cityid", NpgsqlTypes.NpgsqlDbType.Varchar, city.cityid);
                    cmd.Parameters.AddWithValue("_cityname", NpgsqlTypes.NpgsqlDbType.Varchar, city.cityname);

                   var carrierKey = cmd.ExecuteScalar();
                    tran.Commit();
                    return Guid.Parse(carrierKey.ToString());
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

        public List<CityBO> GetCity()
        {
           
           try
            {
                string sql = "dbo.fn_get_city";
                List<CityBO> citylist = new List<CityBO>();
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
                            var BO = new CityBO();
                            BO.citykey = Guid.Parse(reader["citykey"].ToString());
                            BO.cityid = Utils.CustomParse<string>(reader["cityid"]);
                            BO.cityname = Utils.CustomParse<string>(reader["cityname"]);

                            citylist.Add(BO);
                        }
                    }
                    while (reader.NextResult());
                    reader.Close();
                }
                return citylist;
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

        public CityBO GetCitybyKey(Guid cityKey)
        {
           

            try
            {
                string sql = "dbo.fn_getcitybykey";
                CityBO city = new CityBO();

                conn = new NpgsqlConnection(connString);
                conn.Open();

                using (var cmd = new NpgsqlCommand(sql, conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("_citykey", NpgsqlTypes.NpgsqlDbType.Uuid, cityKey);
                        var reader = cmd.ExecuteReader();
                        do
                        {
                            while (reader.Read())
                            {
                                city.citykey = Guid.Parse(reader["citykey"].ToString());
                                city.cityid = Utils.CustomParse<string>(reader["cityid"]);
                                city.cityname = Utils.CustomParse<string>(reader["cityname"]);
                            }
                        }
                        while (reader.NextResult());
                    reader.Close();
                }
                
                return city;
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

        public bool UpdateCity(CityBO city)
        {
           
            try
            {
                string query = "dbo.fn_update_city";
                conn = new NpgsqlConnection(connString);
                conn.Open();
                NpgsqlTransaction tran = conn.BeginTransaction();

                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("_citykey", NpgsqlTypes.NpgsqlDbType.Uuid, city.citykey);
                    cmd.Parameters.AddWithValue("_cityid", NpgsqlTypes.NpgsqlDbType.Varchar, city.cityid);
                    cmd.Parameters.AddWithValue("_cityname", NpgsqlTypes.NpgsqlDbType.Varchar, city.cityname);                   

                    var reader = cmd.ExecuteReader();
                    tran.Commit();
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

    }
}


