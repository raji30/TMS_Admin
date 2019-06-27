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
    public class DriverDL
    {
        string connString = "host=localhost;Username=postgres;Password=TMS@123;Database=App_model";
        NpgsqlConnection connection;

        public DriverDL()
        {
            connection = new NpgsqlConnection(connString);
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
            string sql = "dbo.fn_get_drivers";
            List<DriverBO> driverlist = new List<DriverBO>();
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
                            var BO = new DriverBO();
                            BO.driverkey = Guid.Parse(reader["driverkey"].ToString());
                            BO.driverid = Utils.CustomParse<string>(reader["driverid"]);
                            driverlist.Add(BO);
                        }
                    }
                    while (reader.NextResult());
                }
            }
            return driverlist;
        }
    }
}
