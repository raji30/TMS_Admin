using Npgsql;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMS.BusinessObjects;
using TMS.Data.TableOperations;

namespace TMS.Data
{
    public class CompanyDL
    {
        string connString;//= "host=localhost;Username=postgres;Password=TMS@123;Database=App_model";      
        NpgsqlConnection connection;
        NpgsqlCommand cmd;

        public CompanyDL()
        {
            connString = ConfigurationManager.ConnectionStrings["App_model"].ConnectionString;
        }

        public Guid insertCompany(CompanyDetailBO company)
        {
            try
            {
                string sql = "dbo.fn_insert_company";
                connection = new NpgsqlConnection(connString);
                connection.Open();

                NpgsqlTransaction tran = connection.BeginTransaction();

                using (var cmd = new NpgsqlCommand(sql, connection))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("_compid", NpgsqlTypes.NpgsqlDbType.Varchar, company.compid);
                    cmd.Parameters.AddWithValue("_compname", NpgsqlTypes.NpgsqlDbType.Varchar, company.compname);
                    cmd.Parameters.AddWithValue("_addrkey", NpgsqlTypes.NpgsqlDbType.Uuid, company.addrkey);                    
                    var compKey = cmd.ExecuteScalar();
                    tran.Commit();
                    return Guid.Parse(compKey.ToString());
                }
            }
            catch (Exception msg)
            {
                throw msg;
            }
            finally
            {
                connection.Close();
            }
        }
        public bool updateCompany(CompanyDetailBO company)
        {            
           try
            {
                string sql = "dbo.fn_update_company";

                connection = new NpgsqlConnection(connString);
                connection.Open();
                NpgsqlTransaction tran = connection.BeginTransaction();

                using (var cmd = new NpgsqlCommand(sql, connection))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("_compkey", NpgsqlTypes.NpgsqlDbType.Uuid, company.compkey);
                    cmd.Parameters.AddWithValue("_compid", NpgsqlTypes.NpgsqlDbType.Varchar, company.compid);
                    cmd.Parameters.AddWithValue("_compname", NpgsqlTypes.NpgsqlDbType.Varchar, company.compname);
                    //cmd.Parameters.AddWithValue("_addrkey", NpgsqlTypes.NpgsqlDbType.Uuid, company.addrkey);

                    var compKey = cmd.ExecuteNonQuery();
                    tran.Commit();

                }
                return true;
            }
            catch (Exception msg)
            {
                throw msg;
            }
            finally
            {
                connection.Close();
            }
        }
        public List<CompanyDetailBO> GetCompanies()
        {
            try
            {
                string sql = "dbo.fn_getcompanies";
                List<CompanyDetailBO> companieslist = new List<CompanyDetailBO>();
                List<string> list = new List<string>();

                connection = new NpgsqlConnection(connString);
                connection.Open();

                using (var cmd = new NpgsqlCommand(sql, connection))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    var reader = cmd.ExecuteReader();
                    do
                    {
                        while (reader.Read())
                        {
                            var company = new CompanyDetailBO();

                            company.compkey = Utils.CustomParse<Guid>(reader["compkey"]);
                            company.compid = Utils.CustomParse<string>(reader["compid"]);
                            company.compname = Utils.CustomParse<string>(reader["compname"]);
                            company.addrkey = Utils.CustomParse<Guid>(reader["addrkey"]);

                            companieslist.Add(company);
                        }
                    }
                    while (reader.NextResult());

                    reader.Close();
                }
                return companieslist;
            }
            catch (Exception msg)
            {
                throw msg;
            }
            finally
            {
                connection.Close();
            }

        }
        public CompanyDetailBO GetCompanyDetailsbykey(Guid compKey)
        {          
          try
            {
                string sql = "dbo.fn_getcompanydetailbykey";
                CompanyDetailBO company = new CompanyDetailBO();

                connection = new NpgsqlConnection(connString);
                connection.Open();

                using (var cmd = new NpgsqlCommand(sql, connection))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("_compkey", NpgsqlTypes.NpgsqlDbType.Uuid, compKey);

                    var reader = cmd.ExecuteReader();
                    do
                    {
                        while (reader.Read())
                        {
                            company.compkey = Utils.CustomParse<Guid>(reader["compkey"]);
                            company.compid = Utils.CustomParse<string>(reader["compid"]);
                            company.compname = Utils.CustomParse<string>(reader["compname"]);
                            company.addrkey = Utils.CustomParse<Guid>(reader["addrkey"]);
                        }
                    }
                    while (reader.NextResult());
                    reader.Close();
                }
                return company;
            }
            catch (Exception msg)
            {
                throw msg;
            }
            finally
            {
                connection.Close();
            }
        }
        public AddressBO GetAddress(Guid? addrKey)
        {
            if (addrKey == null)
            {
                return null;
            }
            AddressBO addBO = new AddressBO();
            AddressRepository repo = new AddressRepository();
            var addr = repo.GetbyId(addrKey.Value);
            if (addr != null)
            {
                addBO.Address1 = addr.address1;
                addBO.Address2 = addr.address2;
                addBO.City = GetCityname(Guid.Parse(addr.city));
                addBO.State = addr.state;
                addBO.Zip = addr.zipcode;
                addBO.Email = addr.email;
                addBO.Fax = addr.fax;
                addBO.Phone = addr.phone;
            }
            return addBO;
        }

        public string GetCityname(Guid citykey)
        {            
            try
            {
                string city = string.Empty;

                string sql = "SELECT cityname from dbo.city " +
                    "where citykey = @cityKey FOR UPDATE";


                var list = new List<DocumentBO>();

                connection = new NpgsqlConnection(connString);
                connection.Open();

                using (var cmd = new NpgsqlCommand(sql, connection))
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
                connection.Close();
            }
        }
    }
}
