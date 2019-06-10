using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMS.BusinessObjects;
using TMS.Data.TableOperations;

namespace TMS.Data
{
    public class DeliveryOrderDL :BaseConnection
    {
        string connString = "host=localhost;Username=postgres;Password=TMS@123;Database=App_model";
        NpgsqlConnection connection;
        public DeliveryOrderDL()
        {
            connection = new NpgsqlConnection(connString);
        }
        public Guid CreateDeliveryOrder(DeliveryOrderBO orderBO)
        {
            string sql = "dbo.fn_insert_order_header";
            using (connection)
            {
                connection.Open();
                using (var cmd = new NpgsqlCommand(sql, connection))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("_orderno",
                        NpgsqlTypes.NpgsqlDbType.Varchar, orderBO.OrderNo);
                    cmd.Parameters.AddWithValue("_orderdate",
                        NpgsqlTypes.NpgsqlDbType.Date, Convert.ToDateTime(orderBO.OrderDate));
                    cmd.Parameters.AddWithValue("_custkey",
                        NpgsqlTypes.NpgsqlDbType.Uuid, orderBO.CustKey);
                    cmd.Parameters.AddWithValue("_billtoaddrkey",
                       NpgsqlTypes.NpgsqlDbType.Uuid, orderBO.BillToAddress);
                    cmd.Parameters.AddWithValue("_sourceaddrkey",
                       NpgsqlTypes.NpgsqlDbType.Uuid, orderBO.SourceAddress);
                    cmd.Parameters.AddWithValue("_destinationaddrkey",
                       NpgsqlTypes.NpgsqlDbType.Uuid, orderBO.DestinationAddress);
                    cmd.Parameters.AddWithValue("_returnaddrkey",
                       NpgsqlTypes.NpgsqlDbType.Uuid, orderBO.ReturnAddress);
                    cmd.Parameters.AddWithValue("_source",
                       NpgsqlTypes.NpgsqlDbType.Smallint, orderBO.Source);
                    cmd.Parameters.AddWithValue("_ordertype",
                       NpgsqlTypes.NpgsqlDbType.Smallint, orderBO.OrderType);
                    cmd.Parameters.AddWithValue("_status",
                       NpgsqlTypes.NpgsqlDbType.Smallint, orderBO.Status);
                    cmd.Parameters.AddWithValue("_statusdate",
                       NpgsqlTypes.NpgsqlDbType.Date, Convert.ToDateTime(orderBO.StatusDate));
                    cmd.Parameters.AddWithValue("_brokerkey",
                       NpgsqlTypes.NpgsqlDbType.Uuid, orderBO.Brokerkey);
                    cmd.Parameters.AddWithValue("_brokerrefno",
                       NpgsqlTypes.NpgsqlDbType.Varchar, orderBO.BrokerRefNo);
                    cmd.Parameters.AddWithValue("_portoforiginkey",
                       NpgsqlTypes.NpgsqlDbType.Uuid, orderBO.PortofOriginKey);
                    cmd.Parameters.AddWithValue("_carrierkey",
                       NpgsqlTypes.NpgsqlDbType.Uuid, orderBO.CarrierKey);
                    cmd.Parameters.AddWithValue("_vesselname",
                       NpgsqlTypes.NpgsqlDbType.Varchar, orderBO.VesselName);
                    cmd.Parameters.AddWithValue("_billoflading",
                      NpgsqlTypes.NpgsqlDbType.Varchar, orderBO.BillofLading);
                    cmd.Parameters.AddWithValue("_bookingno",
                      NpgsqlTypes.NpgsqlDbType.Varchar, orderBO.BookingNo);
                    cmd.Parameters.AddWithValue("_cutoffdate",
                      NpgsqlTypes.NpgsqlDbType.Timestamp, Convert.ToDateTime(orderBO.CutOffDate));
                    cmd.Parameters.AddWithValue("_ishazardous",
                      NpgsqlTypes.NpgsqlDbType.Bit, orderBO.IsHazardous);
                    cmd.Parameters.AddWithValue("_priority",
                      NpgsqlTypes.NpgsqlDbType.Smallint, orderBO.Priority);
                    cmd.Parameters.AddWithValue("_createuserkey",
                      NpgsqlTypes.NpgsqlDbType.Uuid, orderBO.CreatedBy);

                    var OrderID  = cmd.ExecuteScalar();
                    return Guid.Parse(OrderID.ToString());

                    //var reader= cmd.ExecuteReader();
                    //while(reader.Read())
                    //{
                    //var OrderID = Guid.Parse(reader["orderkey"].ToString());
                    //return OrderID;
                    //}
                }
            }
            return Guid.Empty;
        }

        public IEnumerable<string> GetOrdersByUser(Guid userkey)
        {
            string sql = "dbo.fn_get_orders_by_user";
            List<string> list = new List<string>();
            using (connection)
            {
                connection.Open();
                using (var cmd = new NpgsqlCommand(sql, connection))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("_userkey",
                        NpgsqlTypes.NpgsqlDbType.Uuid, userkey);
                    var reader = cmd.ExecuteReader();
                    do
                    {
                        while (reader.Read())
                        {
                           // var thinOrder = new ThinOrderDO();
                           // thinOrder.OrderNo = Utils.CustomParse<string>(reader["orderno"]);
                           // thinOrder.OrderKey = Utils.CustomParse<Guid>(reader["orderkey"]);
                           // thinOrder.OrderDate = Utils.CustomParse<DateTime>(reader["orderdate"]);
                            list.Add(Utils.CustomParse<string>(reader["orderno"]));
                        }
                    }
                    while (reader.NextResult());
                }
            }
            return list;
        }

        public List<DeliveryOrderBO> GetOrders()
        {
            string sql = "dbo.fn_get_All_tms_order_header";
            List <DeliveryOrderBO> DOlist = new List <DeliveryOrderBO>();
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
                            // var thinOrder = new ThinOrderDO();
                            // thinOrder.OrderNo = Utils.CustomParse<string>(reader["orderno"]);
                            // thinOrder.OrderKey = Utils.CustomParse<Guid>(reader["orderkey"]);
                            // thinOrder.OrderDate = Utils.CustomParse<DateTime>(reader["orderdate"]);
                            //list.Add(Utils.CustomParse<string>(reader["orderno"]));

                            var BO = new DeliveryOrderBO();
                            BO.OrderNo = Utils.CustomParse<string>(reader["orderno"]);
                            BO.OrderType = Utils.CustomParse<short>(reader["ordertype"]);
                            BO.BrokerRefNo = Utils.CustomParse<string>(reader["brokerrefno"]);                            
                            BO.OrderKey = Utils.CustomParse<Guid>(reader["orderkey"]);
                            BO.OrderDate = Utils.CustomParse<string>(reader["orderdate"]);

                            BO.statusdescription = reader["statusdescription"].ToString();
                            BO.ordertypedescription = reader["ordertypedescription"].ToString();
                            DOlist.Add(BO);

                        }
                    }
                    while (reader.NextResult());
                }
            }
            return DOlist;
        }

        public bool UpdateDOStatus (string orderkey, int status, string userKey)
        {
            string sql = "update dbo.tms_orderheader set status=@status, lastupdatedate = NOW(), lastupdateuserkey =" +
                "@userkey  where orderkey = @orderkey";
            using (connection)
            {
                connection.Open();
                using (var cmd = new NpgsqlCommand(sql, connection))
                {
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Parameters.AddWithValue("orderkey",
                       NpgsqlTypes.NpgsqlDbType.Uuid, Guid.Parse(orderkey));
                    cmd.Parameters.AddWithValue("status",
                       NpgsqlTypes.NpgsqlDbType.Numeric, status);
                    cmd.Parameters.AddWithValue("userkey",
                       NpgsqlTypes.NpgsqlDbType.Uuid, Guid.Parse(userKey));
                    int returnvalue= cmd.ExecuteNonQuery();
                    if (returnvalue < 0)
                    {
                        return false;
                    }
                    else return true;
                }
            }
        }

        public DeliveryOrderBO GetDeliveryOrder(string orderkey)
        {
            string sql = "dbo.fn_get_tms_order_header";
            DeliveryOrderBO bo = new DeliveryOrderBO();
            using (connection)
            {
                connection.Open();
                using (var cmd = new NpgsqlCommand(sql,connection))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("deliveryorderkey", 
                        NpgsqlTypes.NpgsqlDbType.Uuid, Guid.Parse(orderkey));
                   var reader= cmd.ExecuteReader();
                    while(reader.Read())
                    {                        
                        AddressRepository addRepo = new AddressRepository();
                        /*orderno ,  orderdate ,  custkey, billtoaddrkey as billingAddress,
sourceaddrkey as sourceaddress,destinationaddrkey as destinationaddress,returnaddrkey as returnaddress,
  source,  ordertype ,  oh.status , oh.statusdate , oh.holdreason , oh.holddate ,
  br.brokername,br.brokerid ,brokerrefno ,portoforiginkey ,carrierkey,vesselname ,
  billoflading ,  bookingno ,  cutoffdate ,  ishazardous ,  priority ,  oh.createdate ,oh.createuserkey */
                        bo.OrderNo = reader["orderno"].ToString();
                        var dateAndTime = Convert.ToDateTime(reader["orderdate"].ToString()).ToShortDateString();
                        bo.OrderDate = Convert.ToDateTime(reader["orderdate"].ToString()).ToShortDateString();
                        //bo.OrderDate = Convert.ToDateTime(reader["orderdate"].ToString());
                        bo.CustKey = Guid.Parse(reader["custkey"].ToString());
                        bo.BillToAddress = Utils.CustomParse<Guid>(reader["billtoaddrkey"]);
                        bo.SourceAddress = Utils.CustomParse<Guid>(reader["sourceaddrkey"]);
                        bo.DestinationAddress = Utils.CustomParse<Guid>(reader["destinationaddrkey"]);
                        bo.ReturnAddress = Utils.CustomParse<Guid>(reader["returnaddrkey"]);
                        bo.OrderType = Utils.CustomParse<short>(reader["ordertype"]);
                        bo.Status = Utils.CustomParse<short>(reader["status"]);
                        bo.StatusDate = Utils.CustomParse<string>(reader["statusdate"]);
                        bo.HoldReason = Utils.CustomParse<short>(reader["holdreason"]);
                        bo.HoldDate = Utils.CustomParse<string>(reader["holdDate"]);
                        bo.BrokerName = reader["brokername"].ToString();
                        bo.BrokerId = reader["brokerid"].ToString();
                        bo.BrokerRefNo = reader["brokerrefno"].ToString();
                        bo.PortofOriginKey = Utils.CustomParse<Guid>(reader["portoforiginkey"]);
                        bo.CarrierKey = Utils.CustomParse<Guid>(reader["carrierkey"]);
                        bo.VesselName = reader["vesselname"].ToString();
                        bo.BillofLading = reader["billoflading"].ToString();
                        bo.BookingNo = reader["bookingno"].ToString();
                        bo.CutOffDate = Utils.CustomParse<string>(reader["cutoffdate"]);
                        //bo.IsHazardous = Utils.CustomParse<bool>(reader["ishazardous"]);
                        bo.Priority = Utils.CustomParse<short>(reader["priority"]);
                        bo.CreatedDate = Utils.CustomParse<string>(reader["createdate"]);
                        bo.CreatedBy = Utils.CustomParse<Guid>(reader["createuserkey"]);

                        bo.statusdescription = reader["statusdescription"].ToString();
                        bo.ordertypedescription = reader["ordertypedescription"].ToString();

                    }
                    //if(bo.OrderKey!=Guid.Empty)
                    //{
                    //  var orderDetails=  GetOrderDetails(bo.OrderKey);
                    //  bo.OrderDetail = orderDetails;
                    //}
                    return bo;
                }
            }
        }

        public AddressBO GetAddress(Guid? addrKey)
        {
           if(addrKey == null)
            {
                return null;
            }
            AddressBO addBO = new AddressBO();
            AddressRepository repo = new AddressRepository();
           var addr= repo.GetbyId(addrKey.Value);
            if(addr !=null) { 
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

        public List<DeliveryOrderDetailBO> GetOrderDetails(string orderkey)
        {
            var orderDetails = new List<DeliveryOrderDetailBO>();
            string sql = "dbo.fn_get_order_detail";
            DeliveryOrderBO bo = new DeliveryOrderBO();
            using (connection)
            {
                connection.Open();
                using (var cmd = new NpgsqlCommand(sql, connection))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("_orderkey", NpgsqlTypes.NpgsqlDbType.Uuid, Guid.Parse(orderkey));
                    var reader = cmd.ExecuteReader();
                    do
                    {
                        while (reader.Read())
                        {
                            var orderDetail = new DeliveryOrderDetailBO();
                            orderDetail.OrderDetailKey = Utils.CustomParse<Guid>(reader["orderdetailkey"]);
                            orderDetail.OrderKey = Guid.Parse(orderkey);
                            orderDetail.ContainerNo = Utils.CustomParse<string>(reader["containerno"]);
                            orderDetail.ContainerSize = Utils.CustomParse<short>(reader["containersize"]);
                            orderDetail.Chassis = Utils.CustomParse<string>(reader["chassis"]);
                            orderDetail.AppDateFrom = Utils.CustomParse<string>(reader["apptdatefrom"]);
                            orderDetail.AppDateTo = Utils.CustomParse<string>(reader["apptdateto"]);
                            orderDetail.SealNo = Utils.CustomParse<string>(reader["sealno"]);
                            orderDetail.Status = Utils.CustomParse<short>(reader["status"]);
                            orderDetail.StatusDate = Utils.CustomParse<string>(reader["statusdate"]);
                            orderDetail.HoldDate = Utils.CustomParse<string>(reader["holddate"]);
                            orderDetail.HoldReason = Utils.CustomParse<short>(reader["holdreason"]);
                            orderDetail.Comment = Utils.CustomParse<string>(reader["comment_description"]);
                            orderDetail.Weight= Utils.CustomParse<string>(reader["weight"]);
                            orderDetail.StatusDesc = Utils.CustomParse<string>(reader["status_description"]);
                            orderDetail.ContainerSizeDesc = Utils.CustomParse<string>(reader["containersize_description"]);
                            orderDetail.HoldReasonDesc = Utils.CustomParse<string>(reader["holdreason_description"]);
                            orderDetails.Add(orderDetail);
                        }
                    } while (reader.NextResult());
                }
                //connection.Close();
            }
            return orderDetails;
        }

        public IList<Guid> InsertOrderDetails(IList<DeliveryOrderDetailBO> objList)
        {
            var OrderDetailCollection = new List<Guid>();
            //string sql = "dbo.fn_insert_order_details";
            string sql = "dbo.fn_insert_order_details_DOIntake";
            using (connection)
            {
                connection.Open();
                foreach (var obj in objList)
                {
                    using (var cmd = new NpgsqlCommand(sql, connection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("_orderkey",
                            NpgsqlTypes.NpgsqlDbType.Uuid, obj.OrderKey);
                        cmd.Parameters.AddWithValue("_containerno",
                            NpgsqlTypes.NpgsqlDbType.Varchar, obj.ContainerNo);
                        cmd.Parameters.AddWithValue("_containersize",
                            NpgsqlTypes.NpgsqlDbType.Smallint, obj.ContainerSize);
                        cmd.Parameters.AddWithValue("_chassis",
                            NpgsqlTypes.NpgsqlDbType.Varchar, obj.Chassis);
                        cmd.Parameters.AddWithValue("_sealno",
                            NpgsqlTypes.NpgsqlDbType.Varchar, obj.SealNo);
                        cmd.Parameters.AddWithValue("_weight",
                            NpgsqlTypes.NpgsqlDbType.Numeric, Convert.ToDecimal(obj.Weight));
                        //cmd.Parameters.AddWithValue("_apptdatefrom",
                        //    NpgsqlTypes.NpgsqlDbType.Timestamp, obj.AppDateFrom);
                        //cmd.Parameters.AddWithValue("_apptdateto",
                        //    NpgsqlTypes.NpgsqlDbType.Timestamp, obj.AppDateTo);
                        //cmd.Parameters.AddWithValue("_status",
                        //    NpgsqlTypes.NpgsqlDbType.Smallint, obj.Status);
                        //cmd.Parameters.AddWithValue("_statusdate",
                        //    NpgsqlTypes.NpgsqlDbType.Timestamp, obj.StatusDate);
                        //cmd.Parameters.AddWithValue("_holdreason",
                        //    NpgsqlTypes.NpgsqlDbType.Smallint, obj.HoldReason);
                        //cmd.Parameters.AddWithValue("_holddate",
                        //    NpgsqlTypes.NpgsqlDbType.Timestamp, obj.HoldDate);
                        var reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {                            
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                var OrderDetailID = Guid.Parse(reader[i].ToString());
                                OrderDetailCollection.Add(OrderDetailID);
                            }
                        }


                    }
                }
                connection.Close();
            }
            return OrderDetailCollection;
        }

        public bool UpdateOrderDetails(DeliveryOrderDetailBO detail)
        {
            string sql = @"update dbo.tms_orderdetail set apptdatefrom=@apptdatefrom, apptdateto=@apptdateto, "+
                "status=@status,statusdate = @statusdate,holdreason=@holdreason, holddate=@holddate where orderdetailkey=@orderdetailkey AND orderkey=@orderkey";

    
            // string sql = "dbo.fn_update_order_details";
            using (connection)
            {
                connection.Open();
                  using (var cmd = new NpgsqlCommand(sql, connection))
                    {
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Parameters.AddWithValue("@orderkey",
                        NpgsqlTypes.NpgsqlDbType.Uuid, detail.OrderKey);
                    cmd.Parameters.AddWithValue("@orderdetailkey",
                          NpgsqlTypes.NpgsqlDbType.Uuid, detail.OrderDetailKey);
                
                    cmd.Parameters.AddWithValue("@apptdatefrom",
                        NpgsqlTypes.NpgsqlDbType.Date, Convert.ToDateTime(detail.AppDateFrom));
                    cmd.Parameters.AddWithValue("@apptdateto",
                        NpgsqlTypes.NpgsqlDbType.Date, Convert.ToDateTime(detail.AppDateTo));
                    cmd.Parameters.AddWithValue("@status",
                        NpgsqlTypes.NpgsqlDbType.Smallint, detail.Status);
                    cmd.Parameters.AddWithValue("@statusdate",
                        NpgsqlTypes.NpgsqlDbType.Date, Convert.ToDateTime(detail.StatusDate));
                    cmd.Parameters.AddWithValue("@holdreason",
                        NpgsqlTypes.NpgsqlDbType.Smallint, detail.HoldReason);
                    cmd.Parameters.AddWithValue("@holddate",
                        NpgsqlTypes.NpgsqlDbType.Timestamp, Convert.ToDateTime(detail.HoldDate));

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
