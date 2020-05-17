using Npgsql;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMS.BusinessObjects;

namespace TMS.Data
{
   public class SchedulingDL
    {
        string connString;//= "host=localhost;port=5432;Username=postgres;Password=TMS@123;Database=App_model";      
        NpgsqlConnection conn;       
        NpgsqlCommand cmd;

        public SchedulingDL()
        {
            // connection = new NpgsqlConnection(connString);
            // connection.Open();
           connString = ConfigurationManager.ConnectionStrings["App_model"].ConnectionString;
        }
        public Guid InsertSchedule(ScheduleOrderBO schedule)
        {
            try
            {
                string sql = "dbo.fn_insert_tms_route";
                conn = new NpgsqlConnection(connString);
                conn.Open();

                NpgsqlTransaction tran = conn.BeginTransaction();
                cmd = new NpgsqlCommand(sql, conn);

                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("_orderdetailkey", NpgsqlTypes.NpgsqlDbType.Uuid, schedule.OrderDetailKey);
                cmd.Parameters.AddWithValue("_orderkey", NpgsqlTypes.NpgsqlDbType.Uuid, schedule.OrderKey);
                cmd.Parameters.AddWithValue("_legno", NpgsqlTypes.NpgsqlDbType.Smallint, schedule.LegNo);
                cmd.Parameters.AddWithValue("_legtype", NpgsqlTypes.NpgsqlDbType.Smallint, schedule.LegType);
                cmd.Parameters.AddWithValue("_sourceaddrkey", NpgsqlTypes.NpgsqlDbType.Uuid, schedule.SourceAddressKey);
                cmd.Parameters.AddWithValue("_destinationaddrkey",  NpgsqlTypes.NpgsqlDbType.Uuid, schedule.DestinationAddressKey);
                cmd.Parameters.AddWithValue("_estimateddistanceinmiles", NpgsqlTypes.NpgsqlDbType.Double, schedule.DistanceInMiles);
                cmd.Parameters.AddWithValue("_estimatedtraveltime",  NpgsqlTypes.NpgsqlDbType.Double, schedule.TravelTime);
                cmd.Parameters.AddWithValue("_status",  NpgsqlTypes.NpgsqlDbType.Smallint, schedule.Status);
                cmd.Parameters.AddWithValue("_driverkey",NpgsqlTypes.NpgsqlDbType.Smallint, schedule.DriverKey);
                cmd.Parameters.AddWithValue("_scheduledarrival",  NpgsqlTypes.NpgsqlDbType.Timestamp, schedule.ScheduleArrival);
                cmd.Parameters.AddWithValue("_scheduleddeparture", NpgsqlTypes.NpgsqlDbType.Timestamp, schedule.ScheduleDeparture);
                cmd.Parameters.AddWithValue("_odometeratsource", NpgsqlTypes.NpgsqlDbType.Smallint, schedule.Odometer);
                cmd.Parameters.AddWithValue("_actualarrival", NpgsqlTypes.NpgsqlDbType.Timestamp, schedule.ActualArrival);
                cmd.Parameters.AddWithValue("_actualdeparture",NpgsqlTypes.NpgsqlDbType.Timestamp, schedule.ActualDeparture);
                cmd.Parameters.AddWithValue("_odometeratdestination", NpgsqlTypes.NpgsqlDbType.Smallint, schedule.OdometerAtDestination);

                var RouteKey = cmd.ExecuteScalar();
                tran.Commit();
                return Guid.Parse(RouteKey.ToString());

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
        public List<ScheduleOrderBO> GetSchedulingDetails(Guid OrderKey)
        {
            try
            {
                List<ScheduleOrderBO> DOlist = new List<ScheduleOrderBO>();
                List<string> list = new List<string>();

                string sql = "dbo.fn_get_all_route_forDO";
                conn = new NpgsqlConnection(connString);
                conn.Open();
              
                cmd = new NpgsqlCommand(sql, conn);

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
                        BO.ActualDeparture = TimeSpan.Parse(reader["actualdeparture"].ToString());
                        DOlist.Add(BO);

                    }
                }
                while (reader.NextResult());
                reader.Close();
                return DOlist;
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
        public DeliveryOrderDetailBO GetOrderDetailsbykey(string orderdetailkey)
        {

            try
            {
                string sql = "dbo.fn_get_orderdetailbykey";
                var orderDetail = new DeliveryOrderDetailBO();

                conn = new NpgsqlConnection(connString);
                conn.Open();

                cmd = new NpgsqlCommand(sql, conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("_orderdetailkey", NpgsqlTypes.NpgsqlDbType.Uuid, Guid.Parse(orderdetailkey));
                var reader = cmd.ExecuteReader();
                do
                {
                    while (reader.Read())
                    {                       
                        orderDetail.OrderKey = Utils.CustomParse<Guid>(reader["orderkey"]);
                        orderDetail.OrderDetailKey = Utils.CustomParse<Guid>(reader["orderdetailkey"]);
                        orderDetail.containerid = Utils.CustomParse<string>(reader["containerid"]);
                        orderDetail.ContainerNo = Utils.CustomParse<string>(reader["containerno"]);
                        orderDetail.ContainerSize = Utils.CustomParse<short>(reader["containersize"]);
                        orderDetail.ContainerSizeDesc = Utils.CustomParse<string>(reader["containersizeDesc"]);
                        orderDetail.Chassis = Utils.CustomParse<string>(reader["chassis"]);
                        orderDetail.SealNo = Utils.CustomParse<string>(reader["sealno"]);
                        orderDetail.Weight = Utils.CustomParse<string>(reader["weight"]);                       
                    }
                }
                while (reader.NextResult());

                reader.Close();
                return orderDetail;
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
        public List<DeliveryOrderDetailBO> GetOrderstoSchedule()
        {

            try
            {
                string sql = "dbo.fn_get_orders_to_schedule";
                List<DeliveryOrderDetailBO> orderDetails = new List<DeliveryOrderDetailBO>();

                conn = new NpgsqlConnection(connString);
                conn.Open();
               
                cmd = new NpgsqlCommand(sql, conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                var reader = cmd.ExecuteReader();
                do
                {
                    while (reader.Read())
                    {
                        var orderDetail = new DeliveryOrderDetailBO();
                        orderDetail.DOHeader = new DeliveryOrderBO();
                        orderDetail.DOHeader.OrderDate = Utils.CustomParse<DateTime>(reader["orderdate"]);
                        orderDetail.DOHeader.BrokerRefNo = Utils.CustomParse<string>(reader["brokerrefno"]);
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

                reader.Close();
                return orderDetails;
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
    public bool UpdateScheduler(DeliveryOrderDetailBO detail)
        {

            try
            {
                string sql = "dbo.fn_update_order_details";
                conn = new NpgsqlConnection(connString);
                conn.Open();

                NpgsqlTransaction tran = conn.BeginTransaction();
                cmd = new NpgsqlCommand(sql, conn);

                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                //  cmd.Parameters.AddWithValue("_orderkey", NpgsqlTypes.NpgsqlDbType.Uuid, detail.OrderKey);
                cmd.Parameters.AddWithValue("_orderdetailkey", NpgsqlTypes.NpgsqlDbType.Uuid, detail.OrderDetailKey);
                if (detail.AppDateFrom == null)
                {
                    cmd.Parameters.AddWithValue("_apptdatefrom", NpgsqlTypes.NpgsqlDbType.Timestamp, null);

                }
                else
                {
                    cmd.Parameters.AddWithValue("_apptdatefrom", NpgsqlTypes.NpgsqlDbType.Timestamp, detail.AppDateFrom);// DateTime.Parse(detail.AppDateFrom, System.Globalization.CultureInfo.InvariantCulture));

                    //if (detail.AppDateFrom.ToString().Length == 24)
                    //{
                    //    cmd.Parameters.AddWithValue("_apptdatefrom", NpgsqlTypes.NpgsqlDbType.Timestamp, detail.AppDateFrom);// DateTime.Parse(detail.AppDateFrom, System.Globalization.CultureInfo.InvariantCulture));

                    //}
                    //else
                    //{
                    //    var AppDateFrom = DateTime.ParseExact(detail.AppDateFrom.ToString().Substring(0, 24), "ddd MMM dd yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                    //    cmd.Parameters.AddWithValue("_apptdatefrom", NpgsqlTypes.NpgsqlDbType.Timestamp, AppDateFrom);
                    //}

                }
                if (detail.AppDateTo == null)
                {
                    cmd.Parameters.AddWithValue("_apptdateto", NpgsqlTypes.NpgsqlDbType.Timestamp, null);

                }
                else
                {

                    cmd.Parameters.AddWithValue("_apptdateto", NpgsqlTypes.NpgsqlDbType.Timestamp, detail.AppDateTo);

                    //if (detail.AppDateTo.ToString().Length == 24)
                    //{
                    //    //cmd.Parameters.AddWithValue("_apptdateto", NpgsqlTypes.NpgsqlDbType.Timestamp, DateTime.Parse(detail.AppDateTo, System.Globalization.CultureInfo.InvariantCulture));
                    //    cmd.Parameters.AddWithValue("_apptdateto", NpgsqlTypes.NpgsqlDbType.Timestamp, detail.AppDateTo);

                    //}
                    //else
                    //{
                    //    var AppDateTo = DateTime.ParseExact(detail.AppDateTo.ToString().Substring(0, 24), "ddd MMM dd yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                    //    cmd.Parameters.AddWithValue("_apptdateto", NpgsqlTypes.NpgsqlDbType.Timestamp, AppDateTo);
                    //}
                }
                //cmd.Parameters.AddWithValue("_apptdatefrom", NpgsqlTypes.NpgsqlDbType.Timestamp, DateTime.Parse(detail.AppDateFrom, System.Globalization.CultureInfo.InvariantCulture));
                //cmd.Parameters.AddWithValue("_apptdateto", NpgsqlTypes.NpgsqlDbType.Timestamp, DateTime.Parse(detail.AppDateTo, System.Globalization.CultureInfo.InvariantCulture));
                //cmd.Parameters.AddWithValue("_status", NpgsqlTypes.NpgsqlDbType.Smallint, detail.Status);
                //cmd.Parameters.AddWithValue("_statusdate", NpgsqlTypes.NpgsqlDbType.Date, DateTime.Now);
              // cmd.Parameters.AddWithValue("_pickupdatetime", NpgsqlTypes.NpgsqlDbType.Timestamp, detail.PickupDateTime);
               //cmd.Parameters.AddWithValue("_dropoffdatetime", NpgsqlTypes.NpgsqlDbType.Timestamp, detail.DropOffDateTime);

                if (detail.SchedulerNotes == null || String.IsNullOrEmpty(detail.SchedulerNotes) || String.IsNullOrWhiteSpace(detail.SchedulerNotes))
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
                    cmd.Parameters.AddWithValue("_lastfreeday", NpgsqlTypes.NpgsqlDbType.Timestamp, detail.LastFreeDay);

                    //if (detail.LastFreeDay.ToString().Length == 24)
                    //{
                    //    cmd.Parameters.AddWithValue("_lastfreeday", NpgsqlTypes.NpgsqlDbType.Timestamp, detail.LastFreeDay);// DateTime.Parse(detail.LastFreeDay, System.Globalization.CultureInfo.InvariantCulture));

                    //}
                    //else
                    //{
                    //    var LastFreeDay = DateTime.ParseExact(detail.LastFreeDay.ToString().Substring(0, 24), "ddd MMM dd yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                    //    cmd.Parameters.AddWithValue("_lastfreeday", NpgsqlTypes.NpgsqlDbType.Timestamp, LastFreeDay);

                    //    //  cmd.Parameters.AddWithValue("_lastfreeday", NpgsqlTypes.NpgsqlDbType.Timestamp, DateTime.Parse(detail.LastFreeDay, System.Globalization.CultureInfo.InvariantCulture));
                    //}
                }
                cmd.ExecuteNonQuery();
                tran.Commit();
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



        public List<DeliveryOrderDetailBO> GetScheduledContainers()
        {
            try
            {
                var orderDetails = new List<DeliveryOrderDetailBO>();
                string sql = "dbo.fn_get_scheduledlist";
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
                            var orderDetail = new DeliveryOrderDetailBO();
                            orderDetail.OrderKey = Utils.CustomParse<Guid>(reader["orderkey"]);
                            orderDetail.OrderDetailKey = Utils.CustomParse<Guid>(reader["orderdetailkey"]);
                            orderDetail.containerid = Utils.CustomParse<string>(reader["containerid"]);
                            orderDetail.ContainerNo = Utils.CustomParse<string>(reader["containerno"]);
                            orderDetail.ContainerSize = Utils.CustomParse<short>(reader["containersize"]);
                            orderDetail.ContainerSizeDesc = Utils.CustomParse<string>(reader["containerDesc"]);
                            orderDetail.Comments = Utils.CustomParse<string>(reader["comment_notes"]);
                            orderDetail.SchedulerNotes = Utils.CustomParse<string>(reader["schedulernotes"]);
                            // orderDetail = Utils.CustomParse<string>(reader["routekey"]);
                            orderDetail.LastFreeDay = Utils.CustomParse<DateTime>(reader["lastfreeday"]);
                            orderDetail.PickupDateTime = Utils.CustomParse<DateTime>(reader["scheduledarrival"]);
                            orderDetail.DropOffDateTime = Utils.CustomParse<DateTime>(reader["scheduleddeparture"]);
                            orderDetail.StatusDesc = Utils.CustomParse<string>(reader["status"]);
                            orderDetail.nextaction = Utils.CustomParse<string>(reader["nextaction"]);
                            // orderDetail.dr = Utils.CustomParse<string>(reader["drivernotes"]);                          

                            orderDetails.Add(orderDetail);
                        }
                    } while (reader.NextResult());
                    reader.Close();
                }
                return orderDetails;
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

        public SchedulerBO GetScheduledContainer(string orderdetailkey)
        {
            try
            {
                var BO = new SchedulerBO();
                string sql = "dbo.fn_get_scheduledcontainer";
                conn = new NpgsqlConnection(connString);
                conn.Open();

                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("_orderdetailkey", NpgsqlTypes.NpgsqlDbType.Uuid, Guid.Parse(orderdetailkey));
                    var reader = cmd.ExecuteReader();
                    do
                    {
                        while (reader.Read())
                        {
                            BO.OrderDetailKey = Utils.CustomParse<Guid>(reader["orderdetailkey"]);
                            BO.RouteKey = Utils.CustomParse<Guid>(reader["routekey"]);

                            if (reader["apptdatefrom"] != DBNull.Value)
                            {
                                BO.AppDateFrom = DateTime.Parse(reader["apptdatefrom"].ToString());
                            }
                            else
                            {
                                BO.AppDateFrom = null;
                            }
                            if (reader["apptdateto"] != DBNull.Value)
                            {
                                BO.AppDateTo = DateTime.Parse(reader["apptdateto"].ToString());
                            }
                            else
                            {
                                BO.AppDateTo = null;
                            }
                            if (reader["LastFreeDay"] != DBNull.Value)
                            {
                                BO.LastFreeDay = Utils.CustomParse<DateTime>(reader["lastfreeday"]);
                            }
                            else
                            {
                                BO.LastFreeDay = null;
                            }
                            if (reader["scheduledarrival"] != DBNull.Value)
                            {
                                BO.ScheduleArrival = DateTime.Parse(reader["scheduledarrival"].ToString());
                            }
                            else
                            {
                                BO.ScheduleArrival = null;
                            }
                            if (reader["scheduleddeparture"] != DBNull.Value)
                            {
                                BO.ScheduleDeparture = DateTime.Parse(reader["scheduleddeparture"].ToString());
                            }
                            else
                            {
                                BO.ScheduleDeparture = null;
                            }

                            BO.SchedulerNotes = Utils.CustomParse<string>(reader["schedulernotes"]);
                            BO.LegType = Utils.CustomParse<short>(reader["legtype"]);
                            BO.DriverNotes = Utils.CustomParse<string>(reader["drivernotes"]);
                        }
                    } while (reader.NextResult());
                    reader.Close();
                }

                return BO;
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


        public ScheduleOrderBO GetSchedulingDetail(Guid OrderDetailKey)
        {
            try
            {
                string sql = "dbo.fn_GetSchedulingDetailByKey";
                ScheduleOrderBO BO = new ScheduleOrderBO();
                conn = new NpgsqlConnection(connString);
                conn.Open();
                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("_orderdetailkey", NpgsqlTypes.NpgsqlDbType.Uuid, OrderDetailKey);
                    var reader = cmd.ExecuteReader();
                    do
                    {
                        while (reader.Read())
                        {
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
                            BO.ActualDeparture = TimeSpan.Parse(reader["actualdeparture"].ToString());

                        }
                    }
                    while (reader.NextResult());
                    reader.Close();
                }

                return BO;
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
