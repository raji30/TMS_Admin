using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMS.BusinessObjects;

namespace TMS.Data
{
   public class SchedulingDL
    {
        string connString = "host=localhost;Username=postgres;Password=TMS@123;Database=App_model";
        NpgsqlConnection connection;
        public SchedulingDL()
        {
            connection = new NpgsqlConnection(connString);
            connection.Open();
        }
        public Guid InsertSchedule(ScheduleOrderBO schedule)
        {
            string sql = "dbo.fn_insert_tms_route";
            using (connection)
            {
                connection.Open();
                using (var cmd = new NpgsqlCommand(sql, connection))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("_orderdetailkey",
                        NpgsqlTypes.NpgsqlDbType.Uuid, schedule.OrderDetailKey);
                    cmd.Parameters.AddWithValue("_orderkey",
                        NpgsqlTypes.NpgsqlDbType.Uuid, schedule.OrderKey);
                    cmd.Parameters.AddWithValue("_legno",
                        NpgsqlTypes.NpgsqlDbType.Smallint, schedule.LegNo);
                    cmd.Parameters.AddWithValue("_legtype",
                       NpgsqlTypes.NpgsqlDbType.Smallint, schedule.LegType);
                    cmd.Parameters.AddWithValue("_sourceaddrkey",
                       NpgsqlTypes.NpgsqlDbType.Uuid, schedule.SourceAddressKey);
                    cmd.Parameters.AddWithValue("_destinationaddrkey",
                       NpgsqlTypes.NpgsqlDbType.Uuid, schedule.DestinationAddressKey);
                    cmd.Parameters.AddWithValue("_estimateddistanceinmiles",
                       NpgsqlTypes.NpgsqlDbType.Double, schedule.DistanceInMiles);
                    cmd.Parameters.AddWithValue("_estimatedtraveltime",
                       NpgsqlTypes.NpgsqlDbType.Double, schedule.TravelTime);
                    cmd.Parameters.AddWithValue("_status",
                       NpgsqlTypes.NpgsqlDbType.Smallint, schedule.Status);
                    cmd.Parameters.AddWithValue("_driverkey",
                       NpgsqlTypes.NpgsqlDbType.Smallint, schedule.DriverKey);
                    cmd.Parameters.AddWithValue("_scheduledarrival",
                       NpgsqlTypes.NpgsqlDbType.Timestamp, schedule.ScheduleArrival);
                    cmd.Parameters.AddWithValue("_scheduleddeparture",
                       NpgsqlTypes.NpgsqlDbType.Timestamp, schedule.ScheduleDeparture);
                    cmd.Parameters.AddWithValue("_odometeratsource",
                       NpgsqlTypes.NpgsqlDbType.Smallint, schedule.Odometer);
                    cmd.Parameters.AddWithValue("_actualarrival",
                       NpgsqlTypes.NpgsqlDbType.Timestamp, schedule.ActualArrival);
                    cmd.Parameters.AddWithValue("_actualdeparture",
                       NpgsqlTypes.NpgsqlDbType.Timestamp, schedule.ActualDeparture);
                    cmd.Parameters.AddWithValue("_odometeratdestination",
                       NpgsqlTypes.NpgsqlDbType.Smallint, schedule.OdometerAtDestination);
                    
                    var RouteKey = cmd.ExecuteScalar();
                    return Guid.Parse(RouteKey.ToString());

                }
            }
           
        }
        public List<ScheduleOrderBO> GetSchedulingDetails(Guid OrderKey)
        {
            string sql = "dbo.fn_get_all_route_forDO";
            List<ScheduleOrderBO> DOlist = new List<ScheduleOrderBO>();
            List<string> list = new List<string>();
            using (connection)
            {
                connection.Open();
                using (var cmd = new NpgsqlCommand(sql, connection))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("_orderkey", NpgsqlTypes.NpgsqlDbType.Uuid, OrderKey);
                    var reader = cmd.ExecuteReader();
                    do
                    {
                        while (reader.Read())
                        {
                            var BO = new ScheduleOrderBO();
                            BO.OrderDetailKey = Utils.CustomParse<Guid>(reader["orderdetailkey"]);
                            BO.LegNo = Utils.CustomParse<short>(reader["legno"]);
                            BO.LegType = Utils.CustomParse<short>(reader["legtype"]);
                            BO.SourceAddressKey = Utils.CustomParse<Guid>(reader["sourceaddrkey"]);
                            BO.DestinationAddressKey = Utils.CustomParse<Guid>(reader["destinationaddrkey"]);
                            BO.DistanceInMiles = Utils.CustomParse<double>(reader["estimateddistanceinmiles"]);
                            BO.TravelTime = Utils.CustomParse<double>(reader["estimatedtraveltime"]);
                            BO.Status = Utils.CustomParse<short>(reader["status"]);
                            BO.DriverKey = Utils.CustomParse<Guid>(reader["driverkey"]);
                            BO.ScheduleArrival = TimeSpan.Parse(reader["scheduledarrival"].ToString());
                            BO.ScheduleDeparture = TimeSpan.Parse(reader["scheduleddeparture"].ToString());
                            BO.Odometer = Utils.CustomParse<short>(reader["odometeratsource"]);
                            BO.OdometerAtDestination = Utils.CustomParse<short>(reader["odometeratdestination"]);
                            BO.ActualArrival = TimeSpan.Parse(reader["actualarrival"].ToString());
                            BO.ActualDeparture=TimeSpan.Parse(reader["actualdeparture"].ToString());
                            DOlist.Add(BO);

                        }
                    }
                    while (reader.NextResult());
                }
            }
            return DOlist;
        }

        public List<DeliveryOrderDetailBO> GetOrderstoSchedule()
        {
            string sql = "dbo.fn_get_orders_to_schedule";
            List<DeliveryOrderDetailBO> orderDetails = new List<DeliveryOrderDetailBO>();            
            using (connection)
            {                
                using (var cmd = new NpgsqlCommand(sql, connection))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                  
                    var reader = cmd.ExecuteReader();
                    do
                    {
                        while (reader.Read())
                        {                          
                            var orderDetail = new DeliveryOrderDetailBO();
                            orderDetail.OrderKey = Utils.CustomParse<Guid>(reader["orderkey"]);
                            orderDetail.OrderDetailKey = Utils.CustomParse<Guid>(reader["orderdetailkey"]);
                            orderDetail.containerid = Utils.CustomParse<string>(reader["containerid"]);
                            orderDetail.ContainerNo = Utils.CustomParse<string>(reader["containerno"]);
                            orderDetail.ContainerSize = Utils.CustomParse<short>(reader["containersize"]);
                            orderDetail.ContainerSizeDesc = Utils.CustomParse<string>(reader["containersizeDesc"]);
                            orderDetail.Chassis = Utils.CustomParse<string>(reader["chassis"]);
                            orderDetail.SealNo = Utils.CustomParse<string>(reader["sealno"]);
                            orderDetail.Weight = Utils.CustomParse<string>(reader["weight"]);
                                                     
                            orderDetails.Add(orderDetail);
                        }
                    }
                    while (reader.NextResult());
                }
            }
            return orderDetails;
        }

        public bool UpdateScheduler(DeliveryOrderDetailBO detail)
        {
            //string sql = @"update dbo.tms_orderdetail set apptdatefrom=@apptdatefrom, apptdateto=@apptdateto, " +
            //    "status=@status,statusdate = @statusdate,holdreason=@holdreason, holddate=@holddate where orderdetailkey=@orderdetailkey and orderkey=@orderkey";

            //string sql = @"update dbo.tms_orderdetail set apptdatefrom=@apptdatefrom, apptdateto=@apptdateto,Pickupdate = @Pickupdate, Pickuptime = @Pickuptime,DropOffdate = @DropOffdate, DropOfftime = @DropOfftime " +
            //   ",status=@status where orderdetailkey=@orderdetailkey and orderkey=@orderkey";



            string sql = "dbo.fn_update_order_details";

            try
            {

                using (connection)
                {
                    connection.Open();
                    using (var cmd = new NpgsqlCommand(sql, connection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("_orderkey", NpgsqlTypes.NpgsqlDbType.Uuid, detail.OrderKey);
                        cmd.Parameters.AddWithValue("_orderdetailkey", NpgsqlTypes.NpgsqlDbType.Uuid, detail.OrderDetailKey);
                        cmd.Parameters.AddWithValue("_apptdatefrom", NpgsqlTypes.NpgsqlDbType.Timestamp, DateTime.Parse(detail.AppDateFrom, System.Globalization.CultureInfo.InvariantCulture));
                        cmd.Parameters.AddWithValue("_apptdateto", NpgsqlTypes.NpgsqlDbType.Timestamp, DateTime.Parse(detail.AppDateTo, System.Globalization.CultureInfo.InvariantCulture));
                        cmd.Parameters.AddWithValue("_status", NpgsqlTypes.NpgsqlDbType.Smallint, detail.Status);
                        cmd.Parameters.AddWithValue("_statusdate", NpgsqlTypes.NpgsqlDbType.Date, DateTime.Now);
                        cmd.Parameters.AddWithValue("_pickupdatetime", NpgsqlTypes.NpgsqlDbType.Timestamp, DateTime.Parse(detail.PickupDateTime, System.Globalization.CultureInfo.InvariantCulture));
                        cmd.Parameters.AddWithValue("_dropoffdatetime", NpgsqlTypes.NpgsqlDbType.Timestamp, DateTime.Parse(detail.DropOffDateTime, System.Globalization.CultureInfo.InvariantCulture));

                        if (detail.SchedulerNotes == null)
                        {
                            cmd.Parameters.AddWithValue("_schedulernotes", NpgsqlTypes.NpgsqlDbType.Varchar, string.Empty);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("_schedulernotes", NpgsqlTypes.NpgsqlDbType.Varchar, detail.SchedulerNotes);
                        }

                        if (detail.LastFreeDay == null)
                        {
                            cmd.Parameters.AddWithValue("_lastfreeday", NpgsqlTypes.NpgsqlDbType.Timestamp, null);

                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("_lastfreeday", NpgsqlTypes.NpgsqlDbType.Timestamp, DateTime.Parse(detail.LastFreeDay, System.Globalization.CultureInfo.InvariantCulture));

                        }


                        //int returnvalue = cmd.ExecuteNonQuery();
                        //if (returnvalue < 0)
                        //{
                        //    return false;
                        //}
                        //else return true;

                        var reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            var RouteDetailID = Guid.Parse(reader[0].ToString());

                        }
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }


        }
    }
}
