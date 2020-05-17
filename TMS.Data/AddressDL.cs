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
    public class AddressDL
    {
        string connString;//= "host=localhost;port=5432;Username=postgres;Password=TMS@123;Database=App_model";      
        NpgsqlConnection conn;
        NpgsqlCommand cmd;


        public AddressDL()
        {
            // connection = new NpgsqlConnection(connString);
            // connection.Open();
            connString = ConfigurationManager.ConnectionStrings["App_model"].ConnectionString;
        }
        public Guid InsertAddress(AddressBO addr)
        {
           
            try
            {
                string sql = "dbo.fn_insert_address";
                conn.Open();
                NpgsqlTransaction tran = conn.BeginTransaction();
                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("_addrname", NpgsqlTypes.NpgsqlDbType.Varchar, addr.Name == null ? " " : addr.Name);
                    cmd.Parameters.AddWithValue("_address1", NpgsqlTypes.NpgsqlDbType.Varchar, addr.Address1 == null ? " " : addr.Address1);
                    cmd.Parameters.AddWithValue("_address2", NpgsqlTypes.NpgsqlDbType.Varchar, addr.Address2 == null ? " " : addr.Address2);
                    cmd.Parameters.AddWithValue("_city", NpgsqlTypes.NpgsqlDbType.Varchar, addr.City == null ? " " : addr.City);
                    cmd.Parameters.AddWithValue("_state", NpgsqlTypes.NpgsqlDbType.Varchar, addr.State == null ? " " : addr.State);
                    cmd.Parameters.AddWithValue("_zipcode", NpgsqlTypes.NpgsqlDbType.Varchar, addr.Zip == null ? " " : addr.Zip);
                    cmd.Parameters.AddWithValue("_country", NpgsqlTypes.NpgsqlDbType.Varchar, addr.Country == null ? " " : addr.Country);
                    cmd.Parameters.AddWithValue("_website", NpgsqlTypes.NpgsqlDbType.Varchar, addr.Website == null ? " " : addr.Website);
                    cmd.Parameters.AddWithValue("_phone", NpgsqlTypes.NpgsqlDbType.Varchar, addr.Phone == null ? " " : addr.Phone);
                    cmd.Parameters.AddWithValue("_phone2", NpgsqlTypes.NpgsqlDbType.Varchar, addr.Phone2 == null ? " " : addr.Phone2);
                    cmd.Parameters.AddWithValue("_email", NpgsqlTypes.NpgsqlDbType.Varchar, addr.Email == null ? " " : addr.Email);
                    cmd.Parameters.AddWithValue("_email2", NpgsqlTypes.NpgsqlDbType.Varchar, addr.Email2 == null ? " " : addr.Email2);
                    cmd.Parameters.AddWithValue("_fax", NpgsqlTypes.NpgsqlDbType.Varchar, addr.Fax == null ? " " : addr.Fax);

                    var itemKey = cmd.ExecuteScalar();
                    tran.Commit();
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

        public bool UpdateAddress(AddressBO addr)
        {        
           try
            {
                string query = "dbo.fn_update_address";
                conn = new NpgsqlConnection(connString);
                conn.Open();

                NpgsqlTransaction tran = conn.BeginTransaction();
                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("_addrkey", NpgsqlTypes.NpgsqlDbType.Uuid, addr.AddrKey);
                    cmd.Parameters.AddWithValue("_address1", NpgsqlTypes.NpgsqlDbType.Varchar, addr.Address1 == null ? " " : addr.Address1);
                    cmd.Parameters.AddWithValue("_address2", NpgsqlTypes.NpgsqlDbType.Varchar, addr.Address2 == null ? " " : addr.Address2);
                    cmd.Parameters.AddWithValue("_city", NpgsqlTypes.NpgsqlDbType.Varchar, addr.City == null ? " " : addr.City);
                    cmd.Parameters.AddWithValue("_state", NpgsqlTypes.NpgsqlDbType.Varchar, addr.State == null ? " " : addr.State);
                    cmd.Parameters.AddWithValue("_zipcode", NpgsqlTypes.NpgsqlDbType.Varchar, addr.Zip == null ? " " : addr.Zip);
                    cmd.Parameters.AddWithValue("_country", NpgsqlTypes.NpgsqlDbType.Varchar, addr.Country== null? " ": addr.Country);
                    cmd.Parameters.AddWithValue("_website", NpgsqlTypes.NpgsqlDbType.Varchar, addr.Website == null ? " " : addr.Website);
                    cmd.Parameters.AddWithValue("_phone", NpgsqlTypes.NpgsqlDbType.Varchar, addr.Phone == null ? " " : addr.Phone);
                    cmd.Parameters.AddWithValue("_phone2", NpgsqlTypes.NpgsqlDbType.Varchar, addr.Phone2 == null ? " " : addr.Phone2);
                    cmd.Parameters.AddWithValue("_email", NpgsqlTypes.NpgsqlDbType.Varchar, addr.Email == null ? " " : addr.Email);
                    cmd.Parameters.AddWithValue("_email2", NpgsqlTypes.NpgsqlDbType.Varchar, addr.Email2 == null ? " " : addr.Email2);
                    cmd.Parameters.AddWithValue("_fax", NpgsqlTypes.NpgsqlDbType.Varchar, addr.Fax == null ? " " : addr.Fax);
                  
                    var reader = cmd.ExecuteReader();
                    reader.Close();

                    tran.Commit();
                    return true;
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

        public AddressBO GetAddressByKey(Guid addrKey)
        {
            try
            {
                string sql = "dbo.fn_get_address";
                AddressBO addr = new AddressBO();
                conn = new NpgsqlConnection(connString);
                conn.Open();

                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("_addrkey", NpgsqlTypes.NpgsqlDbType.Uuid, addrKey);
                    var reader = cmd.ExecuteReader();
                    do
                    {
                        while (reader.Read())
                        {
                            addr.AddrKey = Utils.CustomParse<Guid>(reader["addrkey"]);
                            addr.Name = Utils.CustomParse<string>(reader["addrname"]);
                            addr.Address1 = Utils.CustomParse<string>(reader["address1"]);
                            addr.Address2 = Utils.CustomParse<string>(reader["address2"]);
                            addr.City = Utils.CustomParse<string>(reader["city"]);
                            addr.State = Utils.CustomParse<string>(reader["state"]);
                            addr.Zip = Utils.CustomParse<string>(reader["zipcode"]);
                            addr.Country = Utils.CustomParse<string>(reader["country"]);
                            addr.Website = Utils.CustomParse<string>(reader["website"]);
                            addr.Phone = Utils.CustomParse<string>(reader["phone"]);
                            addr.Phone2 = Utils.CustomParse<string>(reader["phone2"]);
                            addr.Email = Utils.CustomParse<string>(reader["email"]);
                            addr.Email2 = Utils.CustomParse<string>(reader["email2"]);
                            addr.Fax = Utils.CustomParse<string>(reader["fax"]);
                        }
                    }
                    while (reader.NextResult());
                    reader.Close();                   
                }
                return addr;
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

        public string GetCityname(Guid citykey)
        {
            try
            {
                string city = string.Empty;

                string sql = "SELECT cityname from dbo.city " +
                    "where citykey = @cityKey FOR UPDATE";


                var list = new List<DocumentBO>();

                conn = new NpgsqlConnection(connString);
                conn.Open();

                using (var cmd = new NpgsqlCommand(sql, conn))
                {

                    cmd.Parameters.AddWithValue("@cityKey", citykey);
                    cmd.CommandType = System.Data.CommandType.Text;
                    var reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        city = Convert.ToString(reader["cityname"]);
                    }
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
    }
}
