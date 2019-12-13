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
    public class CityDL
    {
        string connString = "host=localhost;Username=postgres;Password=TMS@123;Database=App_model";
        NpgsqlConnection connection;

        public CityDL()
        {
            connection = new NpgsqlConnection(connString);
        }
        public Guid InsertCity(CityBO city)
        {
            string sql = "dbo.fn_insert_city";
            using (connection)
            {
                connection.Open();
                using (var cmd = new NpgsqlCommand(sql, connection))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("_cityid", NpgsqlTypes.NpgsqlDbType.Varchar, city.cityid);
                    cmd.Parameters.AddWithValue("_cityname", NpgsqlTypes.NpgsqlDbType.Varchar, city.cityname);
                  
                    var carrierKey = cmd.ExecuteScalar();
                    return Guid.Parse(carrierKey.ToString());
                }
            }
        }

        public List<CityBO> GetCity()
        {
            //carrierkey, carrierid, carriername, issteamline, addrkey, scaccode, 
            //licenseplate, licenseplateexpirydate, createdate, status, statusdate

            string sql = "dbo.fn_get_city";
            List<CityBO> citylist = new List<CityBO>();
            List<string> list = new List<string>();

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
                            var BO = new CityBO();
                            BO.citykey = Guid.Parse(reader["citykey"].ToString());
                            BO.cityid = Utils.CustomParse<string>(reader["cityid"]);
                            BO.cityname = Utils.CustomParse<string>(reader["cityname"]);

                            citylist.Add(BO);
                        }
                    }
                    while (reader.NextResult());
                }
            }
            return citylist;
        }

        public CityBO GetCitybyKey(Guid cityKey)
        {
            string sql = "dbo.fn_getcitybykey";
            CityBO city = new CityBO();

            try
            {

                using (connection)
                {
                    connection.Open();
                    using (var cmd = new NpgsqlCommand(sql, connection))
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
                    }
                }
                return city;
            }
            catch
            {
                throw;
            }
        }

        public bool UpdateCity(CityBO city)
        {
            string query = "dbo.fn_update_city";
            using (connection)
            {
                connection.Open();
                using (var cmd = new NpgsqlCommand(query, connection))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("_citykey", NpgsqlTypes.NpgsqlDbType.Uuid, city.citykey);
                    cmd.Parameters.AddWithValue("_cityid", NpgsqlTypes.NpgsqlDbType.Varchar, city.cityid);
                    cmd.Parameters.AddWithValue("_cityname", NpgsqlTypes.NpgsqlDbType.Varchar, city.cityname);                   

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


