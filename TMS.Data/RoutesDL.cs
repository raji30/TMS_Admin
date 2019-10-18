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
   public class RoutesDL
    {
        string connString = "host=localhost;Username=postgres;Password=TMS@123;Database=App_model";
        NpgsqlConnection connection;

        public RoutesDL()
        {
            connection = new NpgsqlConnection(connString);
        }
        public IList<Guid> InsertRouteData(RoutesBO obj)
        {
            //routekey, orderdetailkey, orderkey, legno, legtype, sourceaddrkey, destinationaddrkey,
            //estimateddistanceinmiles, estimatedtraveltime, status, driverkey, 
            //scheduledarrival, scheduleddeparture, odometeratsource, actualarrival, 
            //actualdeparture, odometeratdestination

            var RouteDetailCollection = new List<Guid>();         
            string sql = "dbo.fn_insert_routes_details";
            using (connection)
            {
                connection.Open();
                //foreach (var obj in objList)
                //{
                    using (var cmd = new NpgsqlCommand(sql, connection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("_orderkey",
                            NpgsqlTypes.NpgsqlDbType.Uuid, obj.OrderKey);
                        cmd.Parameters.AddWithValue("_orderdetailkey",
                           NpgsqlTypes.NpgsqlDbType.Uuid, obj.OrderDetailKey);
                        cmd.Parameters.AddWithValue("_driverkey",
                            NpgsqlTypes.NpgsqlDbType.Uuid, obj.driverkey);
                    cmd.Parameters.AddWithValue("_drivernotes",
                           NpgsqlTypes.NpgsqlDbType.Varchar, obj.drivernotes);

                    var reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                var RouteDetailID = Guid.Parse(reader[i].ToString());
                                RouteDetailCollection.Add(RouteDetailID);
                            }
                        }
                    }
               // }
                connection.Close();
            }
            return RouteDetailCollection;
        }

        public bool UpdateRouteDataforDispatchAssignment(RoutesBO obj)
        {
            string sql = "update dbo.tms_routes set drivernotes=@drivernotes, driverkey = @driverkey" +
                " where orderkey = @orderkey and orderdetailkey = @orderdetailkey";

            //SELECT routekey, orderdetailkey, orderkey, legno, legtype, sourceaddrkey, destinationaddrkey, estimateddistanceinmiles, 
            //    estimatedtraveltime, status, driverkey, scheduledarrival, scheduleddeparture, odometeratsource, actualarrival, 
            //    actualdeparture, odometeratdestination, drivernotes FROM dbo.tms_routes;
            string query = "dbo.fn_update_route_for_dispatch_assignment";
            using (connection)
            {
                connection.Open();
                using (var cmd = new NpgsqlCommand(query, connection))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("_orderkey", NpgsqlTypes.NpgsqlDbType.Uuid, obj.OrderKey);
                    cmd.Parameters.AddWithValue("_orderdetailkey", NpgsqlTypes.NpgsqlDbType.Uuid, obj.OrderDetailKey);
                    cmd.Parameters.AddWithValue("_drivernotes", NpgsqlTypes.NpgsqlDbType.Varchar, obj.drivernotes);
                    cmd.Parameters.AddWithValue("_driverkey", NpgsqlTypes.NpgsqlDbType.Uuid, obj.driverkey);

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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool UpdateRouteDataforDispatchDelivery(RoutesBO obj)
        {
            //string sql = "update dbo.tms_routes set legno=@legno, legtype = @legtype, actualarrival=@actualarrival ,actualdeparture =" +
            //    "@actualdeparture  where orderkey = @orderkey and orderdetailkey = @orderdetailkey";

            string sql = "dbo.fn_update_route_for_dispatch_delivery";

            using (connection)
            {
                connection.Open();
                using (var cmd = new NpgsqlCommand(sql, connection))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("_orderkey", NpgsqlTypes.NpgsqlDbType.Uuid, obj.OrderKey);
                    cmd.Parameters.AddWithValue("_orderdetailkey", NpgsqlTypes.NpgsqlDbType.Uuid, obj.OrderDetailKey);
                    cmd.Parameters.AddWithValue("_legno", NpgsqlTypes.NpgsqlDbType.Smallint, obj.legno);
                    cmd.Parameters.AddWithValue("_legtype", NpgsqlTypes.NpgsqlDbType.Smallint, obj.legtype);
                    cmd.Parameters.AddWithValue("_actualarrival", NpgsqlTypes.NpgsqlDbType.Timestamp, obj.actualarrival);
                    cmd.Parameters.AddWithValue("_actualdeparture", NpgsqlTypes.NpgsqlDbType.Timestamp, obj.actualdeparture);
                    cmd.Parameters.AddWithValue("_chassis", NpgsqlTypes.NpgsqlDbType.Varchar, obj.Chassis);

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
