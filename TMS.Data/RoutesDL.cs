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
   public class RoutesDL
    {
        string connString;//= "host=localhost;Username=postgres;Password=TMS@123;Database=App_model";      
        NpgsqlConnection conn;
        NpgsqlCommand cmd;

        public RoutesDL()
        {
            connString = ConfigurationManager.ConnectionStrings["App_model"].ConnectionString;
        }


        public RoutesBO getRoutesData(string orderDetailKey)
        {
            try
            {
                string sql = "dbo.fn_getRoutesData";
                RoutesBO routeData = new RoutesBO();

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
                            routeData.Routekey = Utils.CustomParse<Guid>(reader["routekey"]);
                            routeData.scheduledarrival = Utils.CustomParse<DateTime>(reader["scheduledarrival"]);
                            routeData.scheduledarrival = Utils.CustomParse<DateTime>(reader["scheduleddeparture"]);
                            routeData.legtype = Utils.CustomParse<int>(reader["legtype"]);
                            routeData.legno = Utils.CustomParse<int>(reader["legno"]);
                            routeData.drivernotes = Utils.CustomParse<string>(reader["drivernotes"]);
                        }
                    }
                    while (reader.NextResult());
                    reader.Close();
                }

                return routeData;
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

        public IList<Guid> AddRoutes(RoutesBO obj)
        {
            
          try
            {
                //routekey, orderdetailkey, orderkey, legno, legtype, sourceaddrkey, destinationaddrkey,
                //estimateddistanceinmiles, estimatedtraveltime, status, driverkey, 
                //scheduledarrival, scheduleddeparture, odometeratsource, actualarrival, 
                //actualdeparture, odometeratdestination

                var RouteDetailCollection = new List<Guid>();
                string sql = "dbo.fn_insert_tms_route_details";

                conn = new NpgsqlConnection(connString);
                conn.Open();

                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("_orderkey", NpgsqlTypes.NpgsqlDbType.Uuid, obj.OrderKey);
                    cmd.Parameters.AddWithValue("_orderdetailkey", NpgsqlTypes.NpgsqlDbType.Uuid, obj.OrderDetailKey);
                    cmd.Parameters.AddWithValue("_legno", NpgsqlTypes.NpgsqlDbType.Smallint, obj.legno);
                    cmd.Parameters.AddWithValue("_legtype", NpgsqlTypes.NpgsqlDbType.Smallint, obj.legtype);

                    if (String.IsNullOrEmpty(obj.drivernotes) || String.IsNullOrWhiteSpace(obj.drivernotes))
                    {
                        cmd.Parameters.AddWithValue("_drivernotes", NpgsqlTypes.NpgsqlDbType.Varchar, string.Empty);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("_drivernotes", NpgsqlTypes.NpgsqlDbType.Varchar, obj.drivernotes);
                    }
                    cmd.Parameters.AddWithValue("_scheduledarrival", NpgsqlTypes.NpgsqlDbType.Timestamp, obj.scheduledarrival);
                    cmd.Parameters.AddWithValue("_scheduleddeparture", NpgsqlTypes.NpgsqlDbType.Timestamp, obj.scheduleddeparture);
                    //cmd.Parameters.AddWithValue("_scheduledarrival", NpgsqlTypes.NpgsqlDbType.Timestamp, DateTime.Parse(obj.scheduleddeparture, System.Globalization.CultureInfo.InvariantCulture));
                    //cmd.Parameters.AddWithValue("_scheduleddeparture", NpgsqlTypes.NpgsqlDbType.Timestamp, DateTime.Parse(obj.scheduleddeparture, System.Globalization.CultureInfo.InvariantCulture));


                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            var RouteDetailID = Guid.Parse(reader[i].ToString());
                            RouteDetailCollection.Add(RouteDetailID);
                        }
                    }
                    reader.Close();
                }


                return RouteDetailCollection;
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

        public IList<Guid> InsertRouteData(RoutesBO obj)
        {

            try
            {
                //routekey, orderdetailkey, orderkey, legno, legtype, sourceaddrkey, destinationaddrkey,
                //estimateddistanceinmiles, estimatedtraveltime, status, driverkey, 
                //scheduledarrival, scheduleddeparture, odometeratsource, actualarrival, 
                //actualdeparture, odometeratdestination

                var RouteDetailCollection = new List<Guid>();
                string sql = "dbo.fn_insert_routes_details";

                conn = new NpgsqlConnection(connString);
                conn.Open();

                using (var cmd = new NpgsqlCommand(sql, conn))
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
                    reader.Close();
                }


                return RouteDetailCollection;
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

        public bool UpdateRouteDataforDispatchAssignment(RoutesBO obj)
        {
            try
            {
                string sql = "update dbo.tms_routes set drivernotes=@drivernotes, driverkey = @driverkey" +
                 " where orderkey = @orderkey and orderdetailkey = @orderdetailkey";

                //SELECT routekey, orderdetailkey, orderkey, legno, legtype, sourceaddrkey, destinationaddrkey, estimateddistanceinmiles, 
                //    estimatedtraveltime, status, driverkey, scheduledarrival, scheduleddeparture, odometeratsource, actualarrival, 
                //    actualdeparture, odometeratdestination, drivernotes FROM dbo.tms_routes;
                string query = "dbo.fn_update_route_for_dispatch_assignment";

                conn = new NpgsqlConnection(connString);
                conn.Open();

                using (var cmd = new NpgsqlCommand(query, conn))
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
                    reader.Close();
                }

                return false;
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool UpdateRouteDataforDispatchDelivery(DispatchBO[] dispatchBO)
        {
            try
            {
                string sql = "dbo.fn_update_route_for_dispatch_delivery";
                conn = new NpgsqlConnection(connString);
                conn.Open();

                foreach (var obj in dispatchBO)
                    {
                        using (var cmd = new NpgsqlCommand(sql, conn))
                        {
                            cmd.CommandType = System.Data.CommandType.StoredProcedure;                            
                            cmd.Parameters.AddWithValue("_routekey", NpgsqlTypes.NpgsqlDbType.Uuid, (object)obj.Routekey ?? DBNull.Value);
                            cmd.Parameters.AddWithValue("_orderdetailkey", NpgsqlTypes.NpgsqlDbType.Uuid, (object)obj.OrderDetailKey ?? DBNull.Value);
                            cmd.Parameters.AddWithValue("_appointmentno", NpgsqlTypes.NpgsqlDbType.Varchar, obj.appointmentno == null ? "" : obj.appointmentno);
                            cmd.Parameters.AddWithValue("_driverkey", NpgsqlTypes.NpgsqlDbType.Uuid, obj.driverkey);
                            cmd.Parameters.AddWithValue("_drivernotes", NpgsqlTypes.NpgsqlDbType.Varchar, obj.drivernotes == null ? "" : obj.drivernotes);
                            cmd.Parameters.AddWithValue("_legno", NpgsqlTypes.NpgsqlDbType.Varchar, obj.legno == null ? "" : obj.legno);
                            cmd.Parameters.AddWithValue("_legtype", NpgsqlTypes.NpgsqlDbType.Smallint, obj.legtype == null ? 0 : obj.legtype);
                            cmd.Parameters.AddWithValue("_portwaitingtimefrom", NpgsqlTypes.NpgsqlDbType.Timestamp,(object)obj.portwaitingtimefrom??DBNull.Value);                            
                            cmd.Parameters.AddWithValue("_portwaitingtimeto", NpgsqlTypes.NpgsqlDbType.Timestamp, (object)obj.portwaitingtimeto ?? DBNull.Value);
                            cmd.Parameters.AddWithValue("_customerwaitingtimefrom", NpgsqlTypes.NpgsqlDbType.Timestamp, (object)obj.customerwaitingtimefrom ?? DBNull.Value);
                            cmd.Parameters.AddWithValue("_customerwaitingtimeto", NpgsqlTypes.NpgsqlDbType.Timestamp, (object)obj.customerwaitingtimeto ?? DBNull.Value );
                            cmd.Parameters.AddWithValue("_actualarrival", NpgsqlTypes.NpgsqlDbType.Timestamp, (object)obj.actualarrival ?? DBNull.Value);
                            cmd.Parameters.AddWithValue("_actualdeparture", NpgsqlTypes.NpgsqlDbType.Timestamp, (object)obj.actualdeparture ?? DBNull.Value);
                            cmd.Parameters.AddWithValue("_chassis", NpgsqlTypes.NpgsqlDbType.Varchar, obj.chassis==null?"": obj.chassis);

                            var reader = cmd.ExecuteNonQuery();
                            //while (reader.Read())
                            //{
                            //    var result = bool.Parse(reader[0].ToString());
                            //    return result;
                            //}
                        }
                    }
                
                return true;
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

        public bool UpdateStatus(DispatchBO detail)
        {
            try
            {
                string sql = "dbo.fn_update_status_dispatch";

                conn = new NpgsqlConnection(connString);
                conn.Open();

                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("_orderdetailkey", NpgsqlTypes.NpgsqlDbType.Uuid, detail.OrderDetailKey);
                    cmd.Parameters.AddWithValue("_status", NpgsqlTypes.NpgsqlDbType.Smallint, detail.status);

                    cmd.ExecuteNonQuery();
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

        public bool UpdateDriver(RoutesBO route)
        {
           
            try
            {

                string sql = "update dbo.tms_routes set driverkey =" +
           "@driverkey  where orderkey = @orderkey and orderdetailkey = @orderdetailkey";

                conn = new NpgsqlConnection(connString);
                conn.Open();

                using (var cmd = new NpgsqlCommand(sql, conn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;                       
                        cmd.Parameters.AddWithValue("@orderdetailkey", NpgsqlTypes.NpgsqlDbType.Uuid, route.OrderDetailKey);
                        cmd.Parameters.AddWithValue("@driverkey", NpgsqlTypes.NpgsqlDbType.Smallint, route.driverkey);
                       
                        int returnvalue = cmd.ExecuteNonQuery();
                        if (returnvalue < 0)
                        {
                            return false;
                        }
                        else return true;
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

        public bool UpdateDrivernotes(RoutesBO route)
        {
            try
            {
                string sql = "update dbo.tms_routes set drivernotes =" +
            "@drivernotes  where orderdetailkey = @orderdetailkey";

                conn = new NpgsqlConnection(connString);
                conn.Open();

                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Parameters.AddWithValue("@orderdetailkey", NpgsqlTypes.NpgsqlDbType.Uuid, route.OrderDetailKey);
                    cmd.Parameters.AddWithValue("@drivernotes", NpgsqlTypes.NpgsqlDbType.Smallint, route.driverkey);

                    int returnvalue = cmd.ExecuteNonQuery();
                    if (returnvalue < 0)
                    {
                        return false;
                    }
                    else return true;
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

        public bool UpdateChassis(RoutesBO route)
        {            
            try
            {
             
                    string sql = "update dbo.tms_routes set drivernotes =" +
                 "@drivernotes  where orderdetailkey = @orderdetailkey";

                conn = new NpgsqlConnection(connString);
                conn.Open();

                using (var cmd = new NpgsqlCommand(sql, conn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.Parameters.AddWithValue("@orderdetailkey", NpgsqlTypes.NpgsqlDbType.Uuid, route.OrderDetailKey);
                        cmd.Parameters.AddWithValue("@drivernotes", NpgsqlTypes.NpgsqlDbType.Smallint, route.driverkey);

                        int returnvalue = cmd.ExecuteNonQuery();
                        if (returnvalue < 0)
                        {
                            return false;
                        }
                        else return true;
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

        public bool UpdateLegno(RoutesBO route)
        {            
            try
            {
                string sql = "update dbo.tms_routes set drivernotes =" +
                "@drivernotes  where orderdetailkey = @orderdetailkey";

                conn = new NpgsqlConnection(connString);
                conn.Open();

                using (var cmd = new NpgsqlCommand(sql, conn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.Parameters.AddWithValue("@orderdetailkey", NpgsqlTypes.NpgsqlDbType.Uuid, route.OrderDetailKey);
                        cmd.Parameters.AddWithValue("@drivernotes", NpgsqlTypes.NpgsqlDbType.Smallint, route.driverkey);

                        int returnvalue = cmd.ExecuteNonQuery();
                        if (returnvalue < 0)
                        {
                            return false;
                        }
                        else return true;
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

        public bool UpdateLegtype(RoutesBO route)
        {
           
            try
            {
                string sql = "update dbo.tms_routes set drivernotes =" +
               "@drivernotes  where orderdetailkey = @orderdetailkey";

                conn = new NpgsqlConnection(connString);
                conn.Open();

                using (var cmd = new NpgsqlCommand(sql, conn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.Parameters.AddWithValue("@orderdetailkey", NpgsqlTypes.NpgsqlDbType.Uuid, route.OrderDetailKey);
                        cmd.Parameters.AddWithValue("@drivernotes", NpgsqlTypes.NpgsqlDbType.Smallint, route.driverkey);

                        int returnvalue = cmd.ExecuteNonQuery();
                        if (returnvalue < 0)
                        {
                            return false;
                        }
                        else return true;
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

        public bool UpdatePortwaitingtime(RoutesBO route)
        {
           
            try
            {
                string sql = "update dbo.tms_routes set drivernotes =" +
                 "@drivernotes  where orderdetailkey = @orderdetailkey";


                conn = new NpgsqlConnection(connString);
                conn.Open();

                using (var cmd = new NpgsqlCommand(sql, conn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.Parameters.AddWithValue("@orderdetailkey", NpgsqlTypes.NpgsqlDbType.Uuid, route.OrderDetailKey);
                        cmd.Parameters.AddWithValue("@drivernotes", NpgsqlTypes.NpgsqlDbType.Smallint, route.driverkey);

                        int returnvalue = cmd.ExecuteNonQuery();
                        if (returnvalue < 0)
                        {
                            return false;
                        }
                        else return true;
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

        public bool UpdateCustomerwaitingtime(RoutesBO route)
        {
           
            try
            {
                string sql = "update dbo.tms_routes set drivernotes =" +
               "@drivernotes  where orderdetailkey = @orderdetailkey";

                conn = new NpgsqlConnection(connString);
                conn.Open();

                using (var cmd = new NpgsqlCommand(sql, conn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.Parameters.AddWithValue("@orderdetailkey", NpgsqlTypes.NpgsqlDbType.Uuid, route.OrderDetailKey);
                        cmd.Parameters.AddWithValue("@drivernotes", NpgsqlTypes.NpgsqlDbType.Smallint, route.driverkey);

                        int returnvalue = cmd.ExecuteNonQuery();
                        if (returnvalue < 0)
                        {
                            return false;
                        }
                        else return true;
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

        public bool UpdatePickupandDrop(RoutesBO route)
        {           
            try
            {
                string sql = "update dbo.tms_routes set drivernotes =" +
                "@drivernotes  where orderdetailkey = @orderdetailkey";

                conn = new NpgsqlConnection(connString);
                conn.Open();

                using (var cmd = new NpgsqlCommand(sql, conn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.Parameters.AddWithValue("@orderdetailkey", NpgsqlTypes.NpgsqlDbType.Uuid, route.OrderDetailKey);
                        cmd.Parameters.AddWithValue("@drivernotes", NpgsqlTypes.NpgsqlDbType.Smallint, route.driverkey);

                        int returnvalue = cmd.ExecuteNonQuery();
                        if (returnvalue < 0)
                        {
                            return false;
                        }
                        else return true;
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
    }
}
