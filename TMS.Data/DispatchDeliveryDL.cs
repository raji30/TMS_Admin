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
    public class DispatchDeliveryDL
    {
        string connString;//= "host=localhost;Username=postgres;Password=TMS@123;Database=App_model";      
        NpgsqlConnection conn;
        NpgsqlCommand cmd;

        public DispatchDeliveryDL()
        {
            connString = ConfigurationManager.ConnectionStrings["App_model"].ConnectionString;
        }

        public List<DeliveryOrderDetailBO> GetOrderstoDispatchDelivery()
        {           
          try
            {
                string sql = "dbo.fn_get_orders_to_dispatch";
                List<DeliveryOrderDetailBO> orderDetails = new List<DeliveryOrderDetailBO>();

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
                            orderDetail.ContainerSizeDesc = Utils.CustomParse<string>(reader["containersizeDesc"]);
                            orderDetail.Chassis = Utils.CustomParse<string>(reader["chassis"]);
                            orderDetail.SealNo = Utils.CustomParse<string>(reader["sealno"]);
                            orderDetail.Weight = Utils.CustomParse<string>(reader["weight"]);
                            orderDetail.AppDateFrom = Utils.CustomParse<DateTime>(reader["apptdatefrom"]);
                            orderDetail.AppDateTo = Utils.CustomParse<DateTime>(reader["apptdateto"]);
                            orderDetail.PickupDateTime = Utils.CustomParse<DateTime>(reader["scheduledarrival"]);
                            orderDetail.DropOffDateTime = Utils.CustomParse<DateTime>(reader["scheduleddeparture"]);
                            orderDetail.LastFreeDay = Utils.CustomParse<DateTime>(reader["LastFreeDay"]);
                            orderDetail.SchedulerNotes = Utils.CustomParse<string>(reader["schedulernotes"]);

                            orderDetails.Add(orderDetail);
                        }
                    }
                    while (reader.NextResult());
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

        public List<DispatchBO> GetDispatchItems(Guid orderdetailkey)
        {          
            try
            {

                string sql = "dbo.fn_get_dispatchitemslist";
                List<DispatchBO> DispatchItems = new List<DispatchBO>();
                conn = new NpgsqlConnection(connString);
                conn.Open();

                cmd = new NpgsqlCommand(sql, conn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("_orderdetailkey", NpgsqlTypes.NpgsqlDbType.Uuid, orderdetailkey);
                    var reader = cmd.ExecuteReader();
                    do
                    {
                        while (reader.Read())
                        {
                            var data = new DispatchBO();
                            data.OrderDetailKey = Utils.CustomParse<Guid>(reader["orderdetailkey"]);
                            data.containerid = Utils.CustomParse<string>(reader["containerid"]);
                            data.containerno = Utils.CustomParse<string>(reader["containerno"]);
                            data.Routekey = Utils.CustomParse<Guid>(reader["routekey"]);
                            data.driverkey = Utils.CustomParse<Guid>(reader["driverkey"]);
                            data.driverid = Utils.CustomParse<string>(reader["driverid"]);
                            data.drivernotes = Utils.CustomParse<string>(reader["drivernotes"]);
                            data.legno = Utils.CustomParse<string>(reader["legno"]);
                            data.legtype = Utils.CustomParse<short>(reader["legtype"]);
                            data.legtypeDesc= Utils.CustomParse<string>(reader["legtypeDesc"]);

                            if(reader["actualarrival"]!=DBNull.Value)
                            {
                                data.actualarrival = Utils.CustomParse<DateTime>(reader["actualarrival"]);
                            }
                            if (reader["actualdeparture"] != DBNull.Value)
                            {

                                data.actualdeparture = Utils.CustomParse<DateTime>(reader["actualdeparture"]);
                            }
                            if (reader["portwaitingtimefrom"] != DBNull.Value)
                            {

                                data.portwaitingtimefrom = Utils.CustomParse<DateTime>(reader["portwaitingtimefrom"]);
                            }
                            if (reader["portwaitingtimeto"] != DBNull.Value)
                            {

                                data.portwaitingtimeto = Utils.CustomParse<DateTime>(reader["portwaitingtimeto"]);
                            }
                            if (reader["customerwaitingtimefrom"] != DBNull.Value)
                            {

                                data.customerwaitingtimefrom = Utils.CustomParse<DateTime>(reader["customerwaitingtimefrom"]);
                            }
                            if (reader["customerwaitingtimeto"] != DBNull.Value)
                            {

                                data.customerwaitingtimeto = Utils.CustomParse<DateTime>(reader["customerwaitingtimeto"]);
                            }
                            data.appointmentno = Utils.CustomParse<string>(reader["appointmentno"]);
                            DispatchItems.Add(data);
                        }
                    }
                    while (reader.NextResult());
                reader.Close();
                return DispatchItems;
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

        public List<DeliveryOrderBO> GetDispatchItemsList()
        {
            
          try
            {
                string sql = "dbo.fn_get_dispatchitems";
                List<DeliveryOrderBO> orderData = new List<DeliveryOrderBO>();

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
                            var order = new DeliveryOrderBO();
                            order.OrderDetails = new DeliveryOrderDetailBO();
                            order.OrderNo = Utils.CustomParse<string>(reader["orderno"]);
                            order.OrderKey = Utils.CustomParse<Guid>(reader["orderkey"]);
                            order.OrderDate = Utils.CustomParse<DateTime>(reader["orderdate"]);

                            order.OrderDetails.OrderDetailKey = Utils.CustomParse<Guid>(reader["orderdetailkey"]);
                            order.OrderDetails.containerid = Utils.CustomParse<string>(reader["containerid"]);
                            order.OrderDetails.ContainerNo = Utils.CustomParse<string>(reader["containerno"]);
                            order.OrderDetails.ContainerSize = Utils.CustomParse<short>(reader["containersize"]);
                            order.OrderDetails.ContainerSizeDesc = Utils.CustomParse<string>(reader["containersizeDesc"]);
                            order.OrderDetails.Chassis = Utils.CustomParse<string>(reader["chassis"]);
                            order.OrderDetails.SealNo = Utils.CustomParse<string>(reader["sealno"]);
                            order.OrderDetails.Weight = Utils.CustomParse<string>(reader["weight"]);
                            order.OrderDetails.AppDateFrom = Utils.CustomParse<DateTime>(reader["apptdatefrom"]);
                            order.OrderDetails.AppDateTo = Utils.CustomParse<DateTime>(reader["apptdateto"]);
                            //order.OrderDetails.PickupDateTime = Utils.CustomParse<string>(reader["scheduledarrival"]);
                            //order.OrderDetails.DropOffDateTime = Utils.CustomParse<string>(reader["scheduleddeparture"]);
                            order.OrderDetails.LastFreeDay = Utils.CustomParse<DateTime>(reader["LastFreeDay"]);
                            order.OrderDetails.SchedulerNotes = Utils.CustomParse<string>(reader["schedulernotes"]);
                            order.OrderDetails.StatusDesc = Utils.CustomParse<string>(reader["status"]);
                            orderData.Add(order);
                        }
                    }
                    while (reader.NextResult());
                    reader.Close();
                }
            
            return orderData;
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


        public List<DeliveryOrderBO> GetDispatch_OrderandDetails(Guid orderdetailkey)
        {
           
            try
            {
                string sql = "dbo.fn_get_dispatchitems";
                List<DeliveryOrderBO> orderData = new List<DeliveryOrderBO>();

                conn = new NpgsqlConnection(connString);
                conn.Open();

                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("_orderdetailkey", NpgsqlTypes.NpgsqlDbType.Uuid, orderdetailkey);
                    var reader = cmd.ExecuteReader();
                    do
                    {
                        while (reader.Read())
                        {
                            var order = new DeliveryOrderBO();
                            order.OrderDetails = new DeliveryOrderDetailBO();
                            order.OrderNo = Utils.CustomParse<string>(reader["orderno"]);
                            order.OrderKey = Utils.CustomParse<Guid>(reader["orderkey"]);
                            order.OrderDate = Utils.CustomParse<DateTime>(reader["orderdate"]);

                            order.BillToAddress = Utils.CustomParse<Guid>(reader["billingAddress"]);
                            order.SourceAddress = Utils.CustomParse<Guid>(reader["sourceaddress"]);
                            order.DestinationAddress = Utils.CustomParse<Guid>(reader["destinationaddress"]);
                            order.ReturnAddress = Utils.CustomParse<Guid>(reader["returnaddress"]);

                            order.BillToAddr = Utils.CustomParse<string>(reader["billtoaddr"]);
                            order.SourceAddr = Utils.CustomParse<string>(reader["sourceaddr"]);
                            order.DestinationAddr = Utils.CustomParse<string>(reader["destinationaddr"]);


                            order.OrderDetails.OrderDetailKey = Utils.CustomParse<Guid>(reader["orderdetailkey"]);
                            order.OrderDetails.containerid = Utils.CustomParse<string>(reader["containerid"]);
                            order.OrderDetails.ContainerNo = Utils.CustomParse<string>(reader["containerno"]);
                            order.OrderDetails.ContainerSize = Utils.CustomParse<short>(reader["containersize"]);
                            order.OrderDetails.ContainerSizeDesc = Utils.CustomParse<string>(reader["containersizeDesc"]);
                            order.OrderDetails.Chassis = Utils.CustomParse<string>(reader["chassis"]);
                            order.OrderDetails.SealNo = Utils.CustomParse<string>(reader["sealno"]);
                            order.OrderDetails.Weight = Utils.CustomParse<string>(reader["weight"]);
                            order.OrderDetails.AppDateFrom = Utils.CustomParse<DateTime>(reader["apptdatefrom"]);
                            order.OrderDetails.AppDateTo = Utils.CustomParse<DateTime>(reader["apptdateto"]);
                            //order.OrderDetails.PickupDateTime = Utils.CustomParse<string>(reader["scheduledarrival"]);
                            //order.OrderDetails.DropOffDateTime = Utils.CustomParse<string>(reader["scheduleddeparture"]);
                            order.OrderDetails.LastFreeDay = Utils.CustomParse<DateTime>(reader["LastFreeDay"]);
                            order.OrderDetails.SchedulerNotes = Utils.CustomParse<string>(reader["schedulernotes"]);

                            order.BillToAddressBO = GetAddress(order.BillToAddress);
                            //bo.BrokerAddressBO = GetAddress(bo.Brokerkey);
                            order.ReturnAddressBO = GetAddress(order.ReturnAddress);
                            order.SourceAddressBO = GetAddress(order.SourceAddress);
                            order.DestinationAddressBO = GetAddress(order.DestinationAddress);

                            orderData.Add(order);
                        }
                    }
                    while (reader.NextResult());
                    reader.Close();
                }
                return orderData;
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
                addBO.City = addr.city;
                addBO.State = addr.state;
                addBO.Zip = addr.zipcode;
                addBO.Email = addr.email;
                addBO.Fax = addr.fax;
                addBO.Phone = addr.phone;
            }
            return addBO;
        }
    }
}
