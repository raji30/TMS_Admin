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
    public class CarrierDL
    {
        string connString = "host=localhost;Username=postgres;Password=TMS@123;Database=App_model";
        NpgsqlConnection connection;

        public CarrierDL()
        {
            connection = new NpgsqlConnection(connString);
        }
        public Guid InsertCarrier(CarrierBO carrier)
        {
            string sql = "dbo.fn_insert_carrier";
            using (connection)
            {
                connection.Open();
                using (var cmd = new NpgsqlCommand(sql, connection))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("_carrierkey", NpgsqlTypes.NpgsqlDbType.Uuid, carrier.CarrierKey);                   
                    cmd.Parameters.AddWithValue("_carrierid", NpgsqlTypes.NpgsqlDbType.Varchar, carrier.CarrierId);
                    cmd.Parameters.AddWithValue("_carriername", NpgsqlTypes.NpgsqlDbType.Varchar, carrier.CarrierName);
                    cmd.Parameters.AddWithValue("_issteamline", NpgsqlTypes.NpgsqlDbType.Bit, carrier.isstreamline);
                    cmd.Parameters.AddWithValue("_addrkey", NpgsqlTypes.NpgsqlDbType.Uuid, carrier.AddrKey);
                    cmd.Parameters.AddWithValue("_scaccode", NpgsqlTypes.NpgsqlDbType.Varchar, carrier.ScacCode);
                    cmd.Parameters.AddWithValue("_licenseplate", NpgsqlTypes.NpgsqlDbType.Varchar, carrier.LicensePlate);
                    cmd.Parameters.AddWithValue("_licenseplateexpirydate", NpgsqlTypes.NpgsqlDbType.Date, carrier.LicensePlateExpiryDate);
                    cmd.Parameters.AddWithValue("_status", NpgsqlTypes.NpgsqlDbType.Smallint, carrier.Status);
                    cmd.Parameters.AddWithValue("_createdate", NpgsqlTypes.NpgsqlDbType.Timestamp, carrier.CreatedDate);
                    cmd.Parameters.AddWithValue("_statusdate", NpgsqlTypes.NpgsqlDbType.Timestamp, carrier.StatusDate);


                    var carrierKey = cmd.ExecuteScalar();
                    return Guid.Parse(carrierKey.ToString());
                }
            }           
        }

        public List<CarrierBO> GetCarriers()
        {
            //carrierkey, carrierid, carriername, issteamline, addrkey, scaccode, 
            //licenseplate, licenseplateexpirydate, createdate, status, statusdate

            string sql = "dbo.fn_get_carriers";
            List<CarrierBO> carrierlist = new List<CarrierBO>();
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
                            var BO = new CarrierBO();
                            BO.CarrierKey = Guid.Parse(reader["carrierkey"].ToString());
                            BO.CarrierId = Utils.CustomParse<string>(reader["carrierid"]);
                            BO.CarrierName = Utils.CustomParse<string>(reader["carriername"]);
                            BO.AddrKey = Guid.Parse(reader["addrkey"].ToString());
                          
                            carrierlist.Add(BO);
                        }
                    }
                    while (reader.NextResult());
                }
            }
            return carrierlist;
        }
        public CarrierBO GetCarrierbyKey(Guid carrierKey)
        {
            string sql = "dbo.fn_get_carrierbyKey";
            CarrierBO carrierlist = new CarrierBO();

            try
            {

                using (connection)
                {
                    connection.Open();
                    using (var cmd = new NpgsqlCommand(sql, connection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("_carrierkey", NpgsqlTypes.NpgsqlDbType.Uuid, carrierKey);
                        var reader = cmd.ExecuteReader();
                        do
                        {
                            while (reader.Read())
                            {
                                carrierlist.CarrierKey = Guid.Parse(reader["carrierkey"].ToString());
                                carrierlist.CarrierId = Utils.CustomParse<string>(reader["carrierid"]);
                                carrierlist.CarrierName = Utils.CustomParse<string>(reader["carriername"]);
                                carrierlist.ScacCode = Utils.CustomParse<string>(reader["scaccode"]);
                                //carrierlist.isstreamline = Convert.ToBoolean(reader["issteamline"]);
                                carrierlist.LicensePlate = Utils.CustomParse<string>(reader["licenseplate"]);
                                carrierlist.LicensePlateExpiryDate = Utils.CustomParse<DateTime>(reader["licenseplateexpirydate"]);
                                carrierlist.AddrKey = Guid.Parse(reader["addrkey"].ToString());
                            }
                        }
                        while (reader.NextResult());
                    }
                }
                return carrierlist;
            }
            catch
            {
                throw;
            }
        }
    }
}

