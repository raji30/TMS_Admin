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
    public class DriverDL
    {
        string connString = "host=localhost;port=5432;Username=postgres;Password=TMS@123;Database=App_model";      
        NpgsqlConnection conn;
        NpgsqlCommand cmd;

        public DriverDL()
        {
           // connString = ConfigurationManager.ConnectionStrings["App_model"].ConnectionString;
        }

        public IList<Guid> InsertDriverDetails(DriverBO[] obj)
        {
            //routekey, orderdetailkey, orderkey, legno, legtype, sourceaddrkey, destinationaddrkey,
            //estimateddistanceinmiles, estimatedtraveltime, status, driverkey, 
            //scheduledarrival, scheduleddeparture, odometeratsource, actualarrival, 
            //actualdeparture, odometeratdestination

            var RouteDetailCollection = new List<Guid>();
            //string sql = "dbo.fn_insert_routes_details";
            //using (connection)
            //{
            //    connection.Open();
            //    //foreach (var obj in objList)
            //    //{
            //    using (var cmd = new NpgsqlCommand(sql, connection))
            //    {
            //        cmd.CommandType = System.Data.CommandType.StoredProcedure;

            //        cmd.Parameters.AddWithValue("_orderkey",
            //            NpgsqlTypes.NpgsqlDbType.Uuid, obj.OrderKey);
            //        cmd.Parameters.AddWithValue("_orderdetailkey",
            //           NpgsqlTypes.NpgsqlDbType.Uuid, obj.OrderDetailKey);
            //        cmd.Parameters.AddWithValue("_driverkey",
            //            NpgsqlTypes.NpgsqlDbType.Uuid, obj.driverkey);

            //        var reader = cmd.ExecuteReader();
            //        while (reader.Read())
            //        {
            //            for (int i = 0; i < reader.FieldCount; i++)
            //            {
            //                var RouteDetailID = Guid.Parse(reader[i].ToString());
            //                RouteDetailCollection.Add(RouteDetailID);
            //            }
            //        }
            //    }
            //    // }
            //    connection.Close();
            //}
            return RouteDetailCollection;
        }

        public List<DriverBO> GetDrivers()
        {
            try
            {
                string sql = "dbo.fn_get_drivers";
                List<DriverBO> driverlist = new List<DriverBO>();
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
                            var BO = new DriverBO();
                            BO.DriverKey = Guid.Parse(reader["driverkey"].ToString());
                            BO.DriverId = Utils.CustomParse<string>(reader["driverid"]);
                            driverlist.Add(BO);
                        }
                    }
                    while (reader.NextResult());
                    reader.Close();
                }
                return driverlist;
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
