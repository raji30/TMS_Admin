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
    public class CarrierDL
    {
        string connString;// = "host=localhost;port=5432;Username=postgres;Password=TMS@123;Database=App_model";      
        NpgsqlConnection conn;
        NpgsqlCommand cmd;

        public CarrierDL()
        {
            connString = ConfigurationManager.ConnectionStrings["App_model"].ConnectionString;
        }
        public Guid InsertCarrier(CarrierBO carrier)
        {
         
           try
            {
                string sql = "dbo.fn_insert_carrier";
                conn = new NpgsqlConnection(connString);
                conn.Open();
                NpgsqlTransaction tran = conn.BeginTransaction();

                using (var cmd = new NpgsqlCommand(sql, conn))
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

        public List<CarrierBO> GetCarriers()
        {
            try
            {
                string sql = "dbo.fn_get_carriers";
                List<CarrierBO> carrierlist = new List<CarrierBO>();
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
                            var BO = new CarrierBO();
                            BO.CarrierKey = Guid.Parse(reader["carrierkey"].ToString());
                            BO.CarrierId = Utils.CustomParse<string>(reader["carrierid"]);
                            BO.CarrierName = Utils.CustomParse<string>(reader["carriername"]);
                            BO.AddrKey = Guid.Parse(reader["addrkey"].ToString());
                            BO.ScacCode = Utils.CustomParse<string>(reader["scaccode"]);
                            carrierlist.Add(BO);
                        }
                    }
                    while (reader.NextResult());
                    reader.Close();
                }

                return carrierlist;
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

        public CarrierBO GetCarrierbyKey(Guid carrierKey)
        {
            try
            {
                string sql = "dbo.fn_get_carrierbyKey";
                CarrierBO carrierlist = new CarrierBO();

                conn = new NpgsqlConnection(connString);
                conn.Open();

                using (var cmd = new NpgsqlCommand(sql, conn))
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
                    reader.Close();
                }
                
                return carrierlist;
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

