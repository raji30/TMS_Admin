﻿using Npgsql;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMS.BusinessObjects;

namespace TMS.Data
{
   public class StatusDL
    {
        string connString;//= "host=localhost;Username=postgres;Password=TMS@123;Database=App_model";      
        NpgsqlConnection conn;
        NpgsqlCommand cmd;

        public StatusDL()
        {
            connString = ConfigurationManager.ConnectionStrings["App_model"].ConnectionString;
        }
        public List<StatusBO> getOrderStatus()
        {
            try
            {
                string sql = "dbo.fn_get_tms_orderstatus";
                List<StatusBO> statuslist = new List<StatusBO>();

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
                            var status = new StatusBO();
                            status.status = Utils.CustomParse<short>(reader["status"]);
                            status.description = Utils.CustomParse<string>(reader["description"]);

                            statuslist.Add(status);
                        }
                    }
                    while (reader.NextResult());
                    reader.Close();
                }

                return statuslist;
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
