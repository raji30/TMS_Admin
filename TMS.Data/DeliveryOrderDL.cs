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
    public class DeliveryOrderDL :BaseConnection
    {
        string connString = "host=localhost;Username=postgres;Password=TMS@123;Database=App_model";
        NpgsqlConnection connection;
        public DeliveryOrderDL()
        {
            
        }
        public Guid CreateDeliveryOrder(DeliveryOrderBO orderBO)
        {
            Guid Orderkey = Guid.Empty;
            string sql = "dbo.fn_insert_order_header";

            using (connection = new NpgsqlConnection(connString))
            {
                
                connection.Open();
                using (var cmd = new NpgsqlCommand(sql, connection))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("_orderno",  NpgsqlTypes.NpgsqlDbType.Varchar, orderBO.OrderNo);
                    cmd.Parameters.AddWithValue("_orderdate",orderBO.OrderDate);                 
                    cmd.Parameters.AddWithValue("_custkey", NpgsqlTypes.NpgsqlDbType.Uuid, orderBO.CustKey);
                    cmd.Parameters.AddWithValue("_billtoaddrkey", NpgsqlTypes.NpgsqlDbType.Uuid, orderBO.BillToAddress);
                    cmd.Parameters.AddWithValue("_sourceaddrkey", NpgsqlTypes.NpgsqlDbType.Uuid, orderBO.SourceAddress);
                    cmd.Parameters.AddWithValue("_destinationaddrkey",NpgsqlTypes.NpgsqlDbType.Uuid, orderBO.DestinationAddress);
                   
                    cmd.Parameters.AddWithValue("_returnaddrkey", NpgsqlTypes.NpgsqlDbType.Uuid, orderBO.ReturnAddress);
                    
                    //cmd.Parameters.AddWithValue("_source",NpgsqlTypes.NpgsqlDbType.Smallint, orderBO.Source);
                    cmd.Parameters.AddWithValue("_ordertype", NpgsqlTypes.NpgsqlDbType.Smallint, orderBO.OrderType);
                    cmd.Parameters.AddWithValue("_status",NpgsqlTypes.NpgsqlDbType.Smallint, 1);//1- Inprogress- orderBO.Status
                    //cmd.Parameters.AddWithValue("_statusdate",NpgsqlTypes.NpgsqlDbType.Date, Convert.ToDateTime(orderBO.StatusDate));
                    cmd.Parameters.AddWithValue("_brokerkey",NpgsqlTypes.NpgsqlDbType.Uuid, orderBO.Brokerkey);

                    if(String.IsNullOrWhiteSpace(orderBO.BrokerRefNo)|| String.IsNullOrEmpty(orderBO.BrokerRefNo))
                    {
                        cmd.Parameters.AddWithValue("_brokerrefno", NpgsqlTypes.NpgsqlDbType.Varchar, "");
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("_brokerrefno", NpgsqlTypes.NpgsqlDbType.Varchar, orderBO.BrokerRefNo);
                    }
                   
                    if (String.IsNullOrWhiteSpace(orderBO.VesselName) || String.IsNullOrEmpty(orderBO.VesselName))
                    {
                        cmd.Parameters.AddWithValue("_vesselname", NpgsqlTypes.NpgsqlDbType.Varchar, "");
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("_vesselname", NpgsqlTypes.NpgsqlDbType.Varchar, orderBO.VesselName);
                    }                   
                        cmd.Parameters.AddWithValue("_portoforiginkey", NpgsqlTypes.NpgsqlDbType.Uuid, orderBO.PortofOriginKey);                   
                        cmd.Parameters.AddWithValue("_portofdestinationkey", NpgsqlTypes.NpgsqlDbType.Uuid, orderBO.PortofDestinationKey);
                    cmd.Parameters.AddWithValue("_carrierkey", NpgsqlTypes.NpgsqlDbType.Uuid, orderBO.CarrierKey);
                   

                    if (String.IsNullOrWhiteSpace(orderBO.BookingNo) || String.IsNullOrEmpty(orderBO.BookingNo))
                    {
                        cmd.Parameters.AddWithValue("_bookingno", NpgsqlTypes.NpgsqlDbType.Varchar, "");
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("_bookingno", NpgsqlTypes.NpgsqlDbType.Varchar, orderBO.BookingNo);
                    }

                    if (String.IsNullOrWhiteSpace(orderBO.BillofLading) || String.IsNullOrEmpty(orderBO.BillofLading))
                    {
                        cmd.Parameters.AddWithValue("_billoflading", NpgsqlTypes.NpgsqlDbType.Varchar, "");
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("_billoflading", NpgsqlTypes.NpgsqlDbType.Varchar, orderBO.BillofLading);
                    }

                    
                    cmd.Parameters.AddWithValue("_comment", NpgsqlTypes.NpgsqlDbType.Varchar, orderBO.Comment);

                    if (orderBO.CutOffDate==null)
                    {
                        cmd.Parameters.AddWithValue("_cutoffdate",NpgsqlTypes.NpgsqlDbType.Timestamp, orderBO.CutOffDate);

                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("_cutoffdate", orderBO.CutOffDate);
                    }
                    cmd.Parameters.AddWithValue("_ishazardous",NpgsqlTypes.NpgsqlDbType.Bit, orderBO.IsHazardous);
                    cmd.Parameters.AddWithValue("_priority",NpgsqlTypes.NpgsqlDbType.Smallint, orderBO.Priority);
                    cmd.Parameters.AddWithValue("_createuserkey",NpgsqlTypes.NpgsqlDbType.Uuid, orderBO.CreatedBy);

                    var OrderID  = cmd.ExecuteScalar();
                    Orderkey = Guid.Parse(OrderID.ToString());
                }
              
            }

            //if (Orderkey != Guid.Empty)
            //{
            //    if ( !String.IsNullOrEmpty(orderBO.Comment))
            //    {
            //        orderBO.commentBO = new CommentBO();
            //        orderBO.commentBO.createuserkey = orderBO.CreatedBy;
            //        orderBO.commentBO.description = orderBO.Comment;

            //        var commentkey = CreateComment(Orderkey, orderBO.commentBO);
            //        if (commentkey != Guid.Empty)
            //        {
            //            CreateOrderHeaderComment(Orderkey, commentkey, 0);
            //        }
            //    }
            //}
            return Orderkey;
        }


        public Guid CreateComment(Guid orderKey, CommentBO commentBO)
        {
            Guid commentkey = Guid.Empty;
            string sql = "dbo.fn_insert_comment";

            using (connection = new NpgsqlConnection(connString))
            {
                connection.Open();
                using (var cmd = new NpgsqlCommand(sql, connection))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("_description", NpgsqlTypes.NpgsqlDbType.Varchar, commentBO.description);
                    cmd.Parameters.AddWithValue("_createuserkey", NpgsqlTypes.NpgsqlDbType.Uuid, commentBO.createuserkey);
                   
                    var comment = cmd.ExecuteScalar();
                    commentkey = Guid.Parse(comment.ToString());
                }
            }
            return commentkey;
        }

        public bool CreateOrderHeaderComment(Guid orderKey, Guid commentkey, int type)
        {            
            string sql = "dbo.fn_insert_order_header_comment";

            using (connection = new NpgsqlConnection(connString))
            {
                connection.Open();
                using (var cmd = new NpgsqlCommand(sql, connection))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("_orderKey", NpgsqlTypes.NpgsqlDbType.Uuid, orderKey);
                    cmd.Parameters.AddWithValue("_commentkey", NpgsqlTypes.NpgsqlDbType.Uuid, commentkey);

                    var comment = cmd.ExecuteNonQuery();                   
                }
            }
            return false;
        }

        public IEnumerable<string> GetOrdersByUser(Guid userkey)
        {
            string sql = "dbo.fn_get_orders_by_user";
            List<string> list = new List<string>();
            using (connection = new NpgsqlConnection(connString))
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
            using (connection = new NpgsqlConnection(connString))
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
                            BO.OrderDate = Convert.ToDateTime(reader["orderdate"]);

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
            using (connection = new NpgsqlConnection(connString))
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
            using (connection = new NpgsqlConnection(connString))
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
                        bo.OrderNo = reader["orderno"].ToString();
                        var dateAndTime = Convert.ToDateTime(reader["orderdate"].ToString()).ToString("MM/dd/yyyy");
                        bo.OrderDate = Convert.ToDateTime(reader["orderdate"]);
                        //bo.OrderDate = Convert.ToDateTime(reader["orderdate"].ToString());
                        bo.CustKey = Guid.Parse(reader["custkey"].ToString());
                        bo.BillToAddress = Utils.CustomParse<Guid>(reader["billtoaddrkey"]);
                        bo.SourceAddress = Utils.CustomParse<Guid>(reader["sourceaddrkey"]);
                        bo.DestinationAddress = Utils.CustomParse<Guid>(reader["destinationaddrkey"]);
                        bo.ReturnAddress = Utils.CustomParse<Guid>(reader["returnaddrkey"]);
                        bo.OrderType = Utils.CustomParse<short>(reader["ordertype"]);
                        bo.Status = Utils.CustomParse<short>(reader["status"]);
                        bo.StatusDate = Convert.ToDateTime(reader["statusdate"]);
                        //bo.HoldReason = Utils.CustomParse<short>(reader["holdreason"]);
                        //bo.HoldDate = Convert.ToDateTime(reader["holdDate"]);
                        bo.Brokerkey = Utils.CustomParse<Guid>(reader["brokerkey"]);
                        bo.BrokerName = reader["brokername"].ToString();
                        bo.BrokerId = reader["brokerid"].ToString();
                        bo.BrokerRefNo = reader["brokerrefno"].ToString();
                        bo.PortofOriginKey = Utils.CustomParse<Guid>(reader["portoforiginkey"]);
                        bo.PortofDestinationKey = Utils.CustomParse<Guid>(reader["portofdestinationkey"]);
                        bo.CarrierKey = Utils.CustomParse<Guid>(reader["carrierkey"]);
                        bo.VesselName = reader["vesselname"].ToString();
                        bo.BillofLading = reader["billoflading"].ToString();
                        bo.BookingNo = reader["bookingno"].ToString();
                        //bo.CutOffDate = Utils.CustomParse<string>(reader["cutoffdate"]);
                        bo.CutOffDate = Convert.ToDateTime(reader["cutoffdate"]);
                        //bo.IsHazardous = Utils.CustomParse<bool>(reader["ishazardous"]);
                        bo.Priority = Utils.CustomParse<short>(reader["priority"]);
                        bo.CreatedDate = Convert.ToDateTime(reader["createdate"]);
                        bo.CreatedBy = Utils.CustomParse<Guid>(reader["createuserkey"]);
                     //   bo.Comment = Utils.CustomParse<string>(reader["commentdesc"]);
                        bo.statusdescription = reader["statusdescription"].ToString();
                        bo.ordertypedescription = reader["ordertypedescription"].ToString();

                        bo.BillToAddressBO = GetAddress(bo.BillToAddress);
                        //bo.BrokerAddressBO = GetAddress(bo.Brokerkey);
                        bo.ReturnAddressBO = GetAddress(bo.ReturnAddress);
                        bo.SourceAddressBO= GetAddress(bo.SourceAddress);
                        bo.DestinationAddressBO = GetAddress(bo.DestinationAddress);

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

        public List<DeliveryOrderBO> GetAllDOHeaderandDetails()
        {
            var DOHeaders = new List<DeliveryOrderBO>();
            string sql = "dbo.fn_getAllDOHeaderandDetails";
            DeliveryOrderBO bo = new DeliveryOrderBO();
            using (connection = new NpgsqlConnection(connString))
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
                            var orderHeader = new DeliveryOrderBO();
                            orderHeader.OrderDetails = new DeliveryOrderDetailBO();

                            orderHeader.OrderKey = Utils.CustomParse<Guid>(reader["orderkey"]);
                            orderHeader.OrderNo = Utils.CustomParse<string>(reader["orderno"]);
                            orderHeader.OrderDate = Convert.ToDateTime(reader["orderdate"].ToString());
                            orderHeader.OrderType = Utils.CustomParse<short>(reader["ordertype"]);
                            orderHeader.BrokerRefNo = Utils.CustomParse<string>(reader["brokerrefno"]);

                            orderHeader.CustKey = Guid.Parse(reader["custkey"].ToString());
                            orderHeader.BillToAddress = Utils.CustomParse<Guid>(reader["billtoaddrkey"]);
                            orderHeader.SourceAddress = Utils.CustomParse<Guid>(reader["sourceaddrkey"]);
                            orderHeader.DestinationAddress = Utils.CustomParse<Guid>(reader["destinationaddrkey"]);
                            orderHeader.ReturnAddress = Utils.CustomParse<Guid>(reader["returnaddrkey"]);                          
                            orderHeader.Status = Utils.CustomParse<short>(reader["status"]);
                            orderHeader.StatusDate = Convert.ToDateTime(reader["statusdate"]);
                            orderHeader.HoldReason = Utils.CustomParse<short>(reader["holdreason"]);                           
                            orderHeader.Brokerkey = Utils.CustomParse<Guid>(reader["brokerkey"]);
                           // orderHeader.BrokerName = reader["brokername"].ToString();
                           // orderHeader.BrokerId = reader["brokerid"].ToString();                          
                            orderHeader.PortofOriginKey = Utils.CustomParse<Guid>(reader["portoforiginkey"]);
                            orderHeader.PortofDestinationKey = Utils.CustomParse<Guid>(reader["portofdestinationkey"]);
                            orderHeader.CarrierKey = Utils.CustomParse<Guid>(reader["carrierkey"]);
                            orderHeader.VesselName = reader["vesselname"].ToString();
                            orderHeader.BillofLading = reader["billoflading"].ToString();
                            orderHeader.BookingNo = reader["bookingno"].ToString();                         
                            orderHeader.CutOffDate = Convert.ToDateTime(reader["cutoffdate"]);
                            //orderHeader.IsHazardous = Utils.CustomParse<bool>(reader["ishazardous"]);
                           // orderHeader.Priority = Utils.CustomParse<short>(reader["priority"]);                         

                            //orderHeader.statusdescription = reader["statusdescription"].ToString();
                            //orderHeader.ordertypedescription = reader["ordertypedescription"].ToString();

                            orderHeader.OrderDetails.OrderDetailKey = Utils.CustomParse<Guid>(reader["orderdetailkey"]);
                            orderHeader.OrderDetails.ContainerNo = Utils.CustomParse<string>(reader["containerno"]);
                            orderHeader.OrderDetails.ContainerSize = Utils.CustomParse<short>(reader["containersize"]);
                            orderHeader.OrderDetails.Chassis = Utils.CustomParse<string>(reader["chassis"]);
                            orderHeader.OrderDetails.SealNo = Utils.CustomParse<string>(reader["sealno"]);


                            DOHeaders.Add(orderHeader);
                        }
                    } while (reader.NextResult());
                }
                //connection.Close();
            }
            return DOHeaders;
        }
        public List<DeliveryOrderDetailBO> GetOrderDetails()
        {
            var orderDetails = new List<DeliveryOrderDetailBO>();
            string sql = "dbo.fn_getOrderDetails";
            DeliveryOrderBO bo = new DeliveryOrderBO();
            using (connection = new NpgsqlConnection(connString))
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
                            var orderDetail = new DeliveryOrderDetailBO();
                            orderDetail.OrderKey = Utils.CustomParse<Guid>(reader["orderkey"]);
                            orderDetail.OrderDetailKey = Utils.CustomParse<Guid>(reader["orderdetailkey"]);                            
                            orderDetail.ContainerNo = Utils.CustomParse<string>(reader["containerno"]);
                            orderDetail.ContainerSize = Utils.CustomParse<short>(reader["containersize"]);
                            orderDetail.Chassis = Utils.CustomParse<string>(reader["chassis"]);                                                       
                            orderDetail.SealNo = Utils.CustomParse<string>(reader["sealno"]);
                            orderDetail.Status = Utils.CustomParse<short>(reader["status"]);
                            orderDetail.StatusDate = Utils.CustomParse<string>(reader["statusdate"]);
                            orderDetail.HoldDate = Utils.CustomParse<string>(reader["holddate"]);
                            orderDetail.HoldReason = Utils.CustomParse<short>(reader["holdreason"]);
                            orderDetail.Comments = Utils.CustomParse<string>(reader["comment_description"]);
                            orderDetail.Weight = Utils.CustomParse<string>(reader["weight"]);
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

        public List<DeliveryOrderDetailBO> GetOrderDetailsByKey(string orderkey)
        {
            var orderDetails = new List<DeliveryOrderDetailBO>();
            string sql = "dbo.fn_get_order_detail";
            DeliveryOrderBO bo = new DeliveryOrderBO();
            using (connection = new NpgsqlConnection(connString))
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
                           // orderDetail.AppDateFrom = Convert.ToDateTime(reader["apptdatefrom"].ToString()).ToString("MM/dd/yyyy");
                            //orderDetail.AppDateTo = Convert.ToDateTime(reader["apptdateto"].ToString()).ToString("MM/dd/yyyy");
                            //orderDetail.Pickupdate = Convert.ToDateTime(reader["Pickupdate"].ToString()).ToString("MM/dd/yyyy");
                            //orderDetail.DropOffdate = Convert.ToDateTime(reader["DropOffdate"].ToString()).ToString("MM/dd/yyyy");
                            //orderDetail.Pickuptime =reader["Pickuptime"].ToString();
                            //orderDetail.DropOfftime = reader["DropOfftime"].ToString();
                            //if (!String.IsNullOrWhiteSpace(Convert.ToString(reader["ActualPickupdate"])) || !String.IsNullOrEmpty(Convert.ToString(reader["ActualPickupdate"])))
                            //{
                            //    orderDetail.ActualPickupDateTime = Convert.ToDateTime(reader["ActualPickupdate"].ToString()).ToString("MM/dd/yyyy");
                            //}
                            //if (!String.IsNullOrWhiteSpace(Convert.ToString(reader["ActualDropOffdate"])) || !String.IsNullOrEmpty(Convert.ToString(reader["ActualDropOffdate"])))
                            //{
                            //    orderDetail.ActualDropOffdate = Convert.ToDateTime(reader["ActualDropOffdate"].ToString()).ToString("MM/dd/yyyy");
                            //}
                            //    if (!String.IsNullOrWhiteSpace(Convert.ToString(reader["ActualPickuptime"])) || !String.IsNullOrEmpty(Convert.ToString(reader["ActualPickuptime"])))
                            //    {
                            //        orderDetail.ActualPickuptime = reader["ActualPickuptime"].ToString();
                            //    }
                            //        if (!String.IsNullOrWhiteSpace(Convert.ToString(reader["ActualDropOfftime"])) || !String.IsNullOrEmpty(Convert.ToString(reader["ActualDropOfftime"])))
                            //        {
                            //            orderDetail.ActualDropOfftime = reader["ActualDropOfftime"].ToString();
                            //        }
                            //orderDetail.AppDateFrom = Utils.CustomParse<string>(reader["apptdatefrom"]);
                            //orderDetail.AppDateTo = Utils.CustomParse<string>(reader["apptdateto"]);
                            orderDetail.SealNo = Utils.CustomParse<string>(reader["sealno"]);
                            orderDetail.Status = Utils.CustomParse<short>(reader["status"]);
                            orderDetail.StatusDate = Utils.CustomParse<string>(reader["statusdate"]);
                            orderDetail.HoldDate = Utils.CustomParse<string>(reader["holddate"]);
                            orderDetail.HoldReason = Utils.CustomParse<short>(reader["holdreason"]);
                            orderDetail.Comments = Utils.CustomParse<string>(reader["comment_description"]);
                            orderDetail.Weight= Utils.CustomParse<string>(reader["weight"]);
                            orderDetail.StatusDesc = Utils.CustomParse<string>(reader["status_description"]);
                            orderDetail.ContainerSizeDesc = Utils.CustomParse<string>(reader["containersize_description"]);
                            orderDetail.HoldReasonDesc = Utils.CustomParse<string>(reader["holdreason_description"]);
                            orderDetails.Add(orderDetail);
                        }
                        
                    } while (reader.NextResult()); reader.Close();
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
            using (connection = new NpgsqlConnection(connString))
            {
                connection.Open();
                foreach (var obj in objList)
                {
                    using (var cmd = new NpgsqlCommand(sql, connection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("_orderkey", NpgsqlTypes.NpgsqlDbType.Uuid, obj.OrderKey);
                        if (String.IsNullOrEmpty(obj.ContainerNo))
                        {
                            cmd.Parameters.AddWithValue("_containerno", NpgsqlTypes.NpgsqlDbType.Varchar, string.Empty);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("_containerno",NpgsqlTypes.NpgsqlDbType.Varchar, obj.ContainerNo);
                        }
                       
                            cmd.Parameters.AddWithValue("_containersize",NpgsqlTypes.NpgsqlDbType.Smallint, obj.ContainerSize);
                        
                        if(String.IsNullOrEmpty(obj.Chassis) || String.IsNullOrWhiteSpace(obj.Chassis))
                        {
                            cmd.Parameters.AddWithValue("_chassis", NpgsqlTypes.NpgsqlDbType.Varchar, string.Empty);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("_chassis",NpgsqlTypes.NpgsqlDbType.Varchar, Convert.ToString(obj.Chassis));
                        }
                        if (String.IsNullOrEmpty(obj.SealNo) || String.IsNullOrWhiteSpace(obj.SealNo))
                        {
                            cmd.Parameters.AddWithValue("_sealno",NpgsqlTypes.NpgsqlDbType.Varchar, string.Empty);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("_sealno", NpgsqlTypes.NpgsqlDbType.Varchar, obj.SealNo);
                        }
                        if (String.IsNullOrEmpty(obj.Weight) || String.IsNullOrWhiteSpace(obj.Weight))
                        {   
                            cmd.Parameters.AddWithValue("_weight", NpgsqlTypes.NpgsqlDbType.Numeric, 0);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("_weight", NpgsqlTypes.NpgsqlDbType.Numeric, Convert.ToDecimal(obj.Weight));
                        }

                        if (String.IsNullOrEmpty(obj.Comments)|| String.IsNullOrWhiteSpace(obj.Comments))
                        {
                            cmd.Parameters.AddWithValue("_comment_notes", NpgsqlTypes.NpgsqlDbType.Varchar, "");
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("_comment_notes", NpgsqlTypes.NpgsqlDbType.Varchar, obj.Comments);
                        }
                        cmd.Parameters.AddWithValue("_createuserkey", NpgsqlTypes.NpgsqlDbType.Uuid, obj.CreatedBy);
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

                        reader.Close();
                    }
                }
                connection.Close();
            }
            return OrderDetailCollection;
        }

        public bool UpdateOrderDetails(DeliveryOrderDetailBO detail)
        {
            //string sql = @"update dbo.tms_orderdetail set apptdatefrom=@apptdatefrom, apptdateto=@apptdateto, " +
            //    "status=@status,statusdate = @statusdate,holdreason=@holdreason, holddate=@holddate where orderdetailkey=@orderdetailkey and orderkey=@orderkey";

            //string sql = @"update dbo.tms_orderdetail set apptdatefrom=@apptdatefrom, apptdateto=@apptdateto,Pickupdate = @Pickupdate, Pickuptime = @Pickuptime,DropOffdate = @DropOffdate, DropOfftime = @DropOfftime " +
            //   ",status=@status where orderdetailkey=@orderdetailkey and orderkey=@orderkey";



            string sql = "dbo.fn_update_order_details";

            try
            {

                using (connection = new NpgsqlConnection(connString))
                {
                    connection.Open();
                    using (var cmd = new NpgsqlCommand(sql, connection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("_orderkey", NpgsqlTypes.NpgsqlDbType.Uuid, detail.OrderKey);
                        cmd.Parameters.AddWithValue("_orderdetailkey", NpgsqlTypes.NpgsqlDbType.Uuid, detail.OrderDetailKey);
                        cmd.Parameters.AddWithValue("_apptdatefrom", NpgsqlTypes.NpgsqlDbType.Timestamp, DateTime.Parse(detail.AppDateFrom, System.Globalization.CultureInfo.InvariantCulture));
                        cmd.Parameters.AddWithValue("_apptdateto", NpgsqlTypes.NpgsqlDbType.Timestamp, DateTime.Parse(detail.AppDateTo, System.Globalization.CultureInfo.InvariantCulture));
                        cmd.Parameters.AddWithValue("_status",   NpgsqlTypes.NpgsqlDbType.Smallint, detail.Status);
                        cmd.Parameters.AddWithValue("_statusdate", NpgsqlTypes.NpgsqlDbType.Date, DateTime.Now);
                        cmd.Parameters.AddWithValue("_pickupdatetime", NpgsqlTypes.NpgsqlDbType.Timestamp, DateTime.Parse(detail.PickupDateTime, System.Globalization.CultureInfo.InvariantCulture));
                        cmd.Parameters.AddWithValue("_dropoffdatetime", NpgsqlTypes.NpgsqlDbType.Timestamp, DateTime.Parse(detail.DropOffDateTime, System.Globalization.CultureInfo.InvariantCulture));

                        if(detail.SchedulerNotes == null)
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



        public bool UpdateDeliveryOrderDetailsStatus(DeliveryOrderDetailBO detail)
        {          
            string sql = @"update dbo.tms_orderdetail set status=@status , statusdate = @statusdate where orderdetailkey=@orderdetailkey and orderkey=@orderkey";

            try
            {
                using (connection = new NpgsqlConnection(connString))
                {
                    connection.Open();
                    using (var cmd = new NpgsqlCommand(sql, connection))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.Parameters.AddWithValue("@orderkey", NpgsqlTypes.NpgsqlDbType.Uuid, detail.OrderKey);
                        cmd.Parameters.AddWithValue("@orderdetailkey", NpgsqlTypes.NpgsqlDbType.Uuid, detail.OrderDetailKey);
                        cmd.Parameters.AddWithValue("@status", NpgsqlTypes.NpgsqlDbType.Smallint, detail.Status);
                        cmd.Parameters.AddWithValue("@statusdate", NpgsqlTypes.NpgsqlDbType.Timestamp, DateTime.Parse(DateTime.Now.ToLongDateString(), System.Globalization.CultureInfo.InvariantCulture));

                        int returnvalue = cmd.ExecuteNonQuery();
                        if (returnvalue < 0)
                        {
                            return false;
                        }
                        else return true;
                    }
                }               
            }
            catch
            {
                return false;
            }


        }
    }
    
}
