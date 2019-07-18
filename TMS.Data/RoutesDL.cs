﻿using Npgsql;
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

        public bool UpdateRouteData(RoutesBO obj)
        {
            string sql = "update dbo.tms_routes set legno=@legno, legtype = @legtype, actualarrival=@actualarrival ,actualdeparture =" +
                "@actualdeparture  where orderkey = @orderkey and orderdetailkey = @orderdetailkey";

            //SELECT routekey, orderdetailkey, orderkey, legno, legtype, sourceaddrkey, destinationaddrkey, estimateddistanceinmiles, 
            //    estimatedtraveltime, status, driverkey, scheduledarrival, scheduleddeparture, odometeratsource, actualarrival, 
            //    actualdeparture, odometeratdestination, drivernotes FROM dbo.tms_routes;

            using (connection)
            {
                connection.Open();
                using (var cmd = new NpgsqlCommand(sql, connection))
                {
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Parameters.AddWithValue("orderkey", NpgsqlTypes.NpgsqlDbType.Uuid, obj.OrderKey);
                    cmd.Parameters.AddWithValue("orderdetailkey", NpgsqlTypes.NpgsqlDbType.Uuid, obj.OrderDetailKey);
                    cmd.Parameters.AddWithValue("legno", NpgsqlTypes.NpgsqlDbType.Numeric, obj.legno);
                    cmd.Parameters.AddWithValue("legtype", NpgsqlTypes.NpgsqlDbType.Numeric, obj.legtype);
                    cmd.Parameters.AddWithValue("actualarrival", NpgsqlTypes.NpgsqlDbType.Timestamp, obj.actualarrival);
                    cmd.Parameters.AddWithValue("actualdeparture", NpgsqlTypes.NpgsqlDbType.Timestamp, obj.actualdeparture);
                    
                    int returnvalue = cmd.ExecuteNonQuery();
                    if (returnvalue < 0)
                    {
                        return false;
                    }
                    else return true;
                }
            }
        }
    }
}
