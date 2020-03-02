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
    public  class ContainerSizeDL
    {
        string connString;//= "host=localhost;Username=postgres;Password=TMS@123;Database=App_model";      
        NpgsqlConnection conn;
        NpgsqlCommand cmd;

        public ContainerSizeDL()
        {
            connString = ConfigurationManager.ConnectionStrings["App_model"].ConnectionString;
        }

        public List<ContainerSizeBO> GetContainerSize()
        { 
            try
            {
                string sql = "dbo.fn_get_containersize";
                List<ContainerSizeBO> containerSizelist = new List<ContainerSizeBO>();
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
                            var BO = new ContainerSizeBO();
                            BO.containersize = Utils.CustomParse<Int16>(reader["containersize"]);
                            BO.description = Utils.CustomParse<string>(reader["description"]);

                            containerSizelist.Add(BO);
                        }
                    }
                    while (reader.NextResult());
                    reader.Close();
                }

                return containerSizelist;
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
