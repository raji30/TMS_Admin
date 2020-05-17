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
   public class InvoiceDL
    {
        string connString;//= "host=localhost;port=5432;Username=postgres;Password=TMS@123;Database=App_model";      
        NpgsqlConnection conn;
        NpgsqlCommand cmd;

        public InvoiceDL()
        {
           connString = ConfigurationManager.ConnectionStrings["App_model"].ConnectionString;
        }
        public InvoiceHeaderBO GetInvoice(string invoiceNo)
        {
            try
            {
                string sql = "fn_getInvoice";
                var invoiceHeader = new InvoiceHeaderBO();

                conn = new NpgsqlConnection(connString);
                conn.Open();

                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("_invoiceno",
                        NpgsqlTypes.NpgsqlDbType.Integer, int.Parse(invoiceNo));
                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        invoiceHeader.InvoiceNo = int.Parse(invoiceNo);
                        invoiceHeader.Invoicekey = Guid.Parse(reader["invoicekey"].ToString());
                        invoiceHeader.CustKey = Utils.CustomParse<Guid>(reader["custkey"].ToString());
                        invoiceHeader.BilltoAddrKey = Utils.CustomParse<Guid>(reader["billtoaddrkey"]);
                        invoiceHeader.BilltoAddrCopy = Utils.CustomParse<Guid>(reader["billtocopyaddrkey"]);
                        invoiceHeader.InvoiceAmt = Utils.CustomParse<decimal>(reader["invoiceamount"]);
                        invoiceHeader.DueDate = Convert.ToDateTime(reader["duedate"].ToString());
                        invoiceHeader.InvoiceDate = Convert.ToDateTime(reader["invoicedate"].ToString());
                    }
                    reader.Close();
                }
                return invoiceHeader;
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
        public InvoiceHeaderBO PostInvoice(InvoiceHeaderBO invoice)
        {        
           try
            {
                string sql = "dbo.fn_insertinvoiceheader";
                conn = new NpgsqlConnection(connString);
                conn.Open();

                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("_invoiceno",
                        NpgsqlTypes.NpgsqlDbType.Integer, invoice.InvoiceNo);
                    cmd.Parameters.AddWithValue("_invoicedate",
                        NpgsqlTypes.NpgsqlDbType.Date, Convert.ToDateTime(invoice.InvoiceDate));
                    cmd.Parameters.AddWithValue("_custkey",
                        NpgsqlTypes.NpgsqlDbType.Uuid, invoice.CustKey);
                    cmd.Parameters.AddWithValue("_billtoaddrkey",
                      NpgsqlTypes.NpgsqlDbType.Uuid, invoice.BilltoAddrKey);
                    //cmd.Parameters.AddWithValue("_billtocopyaddrkey",
                    //  NpgsqlTypes.NpgsqlDbType.Uuid, invoice.BilltoAddrCopy);
                    cmd.Parameters.AddWithValue("_invoiceamount",
                      NpgsqlTypes.NpgsqlDbType.Numeric, invoice.InvoiceAmt);
                    cmd.Parameters.AddWithValue("_duedate",
                      NpgsqlTypes.NpgsqlDbType.Date, invoice.DueDate);
                  //  cmd.Parameters.AddWithValue("_invoicetype",
                  //NpgsqlTypes.NpgsqlDbType.Smallint, invoice.InvoiceType);
                    cmd.Parameters.AddWithValue("_orderkey",
                NpgsqlTypes.NpgsqlDbType.Uuid, invoice.OrderKey);
                    var invoicekey = cmd.ExecuteScalar();
                    invoice.Invoicekey =Guid.Parse(invoicekey.ToString());
                    return invoice;
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
        public InvoiceHeaderBO GetInvoicebyOrderDetailKey(string OrderDetailKey)
        {           
            try
            {
                string sql = "dbo.fn_getInvoicebyOrderDetailKey";
                var invoiceHeader = new InvoiceHeaderBO();

                conn = new NpgsqlConnection(connString);
                conn.Open();

                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("_orderdetailkey",
                        NpgsqlTypes.NpgsqlDbType.Uuid, Guid.Parse(OrderDetailKey));
                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        invoiceHeader.InvoiceNo = int.Parse(reader["invoiceNo"].ToString());
                        invoiceHeader.Invoicekey = Guid.Parse(reader["invoicekey"].ToString());
                        invoiceHeader.CustKey = Utils.CustomParse<Guid>(reader["custkey"].ToString());
                        invoiceHeader.BilltoAddrKey = Utils.CustomParse<Guid>(reader["billtoaddrkey"]);
                        invoiceHeader.BilltoAddrCopy = Utils.CustomParse<Guid>(reader["billtocopyaddrkey"]);
                        invoiceHeader.InvoiceAmt = Utils.CustomParse<decimal>(reader["invoiceamount"]);
                        invoiceHeader.DueDate = Convert.ToDateTime(reader["duedate"].ToString());
                        invoiceHeader.InvoiceDate = Convert.ToDateTime(reader["invoicedate"].ToString());
                    }
                    reader.Close();
                    return invoiceHeader;
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
        public InvoiceHeaderBO GetInvoicebyinvoiceKey(string invoicekey)
        {
            try
            {
                string sql = "dbo.fn_getinvoicebyinvoicekey";
                var invoiceHeader = new InvoiceHeaderBO();

                conn = new NpgsqlConnection(connString);
                conn.Open();

                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("_invoicekey",
                        NpgsqlTypes.NpgsqlDbType.Uuid, Guid.Parse(invoicekey));
                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        invoiceHeader.InvoiceNo = int.Parse(reader["invoiceNo"].ToString());
                        invoiceHeader.Invoicekey = Guid.Parse(reader["invoicekey"].ToString());
                        invoiceHeader.CustKey = Guid.Parse(reader["custkey"].ToString());
                        invoiceHeader.BilltoAddrKey = Guid.Parse(reader["billtoaddrkey"].ToString());
                        //invoiceHeader.BilltoAddrCopy = Utils.CustomParse<Guid>(reader["billtocopyaddrkey"]);
                        invoiceHeader.InvoiceAmt = Utils.CustomParse<decimal>(reader["invoiceamount"]);
                        invoiceHeader.DueDate = Convert.ToDateTime(reader["duedate"].ToString());
                        invoiceHeader.InvoiceDate = Convert.ToDateTime(reader["invoicedate"].ToString());
                    }
                    reader.Close();
                    return invoiceHeader;
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

        public List<InvoiceDetailBO> GetInvoiceDetail(string invoicekey)
        {
            try
            {
                var InvDtllist = new List<InvoiceDetailBO>();
                var sql = "dbo.fn_getinvoicedetail";

                conn = new NpgsqlConnection(connString);
                conn.Open();

                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("_invoicekey",
                       NpgsqlTypes.NpgsqlDbType.Uuid, Guid.Parse(invoicekey));
                    var reader = cmd.ExecuteReader();
                    do
                    {
                        while (reader.Read())
                        {
                            var BO = new InvoiceDetailBO
                            {
                                Itemkey = Utils.CustomParse<Guid>(reader["itemkey"]),
                                Description = Utils.CustomParse<string>(reader["description"]),
                                Container = Utils.CustomParse<string>(reader["container"]),
                                Price = Utils.CustomParse<decimal>(reader["price"]),
                                //ItemType = Utils.CustomParse<short>(reader["itemtype"]),
                                Quantity = Utils.CustomParse<decimal>(reader["quantity"]),
                                InvoiceDescription = reader["description"].ToString(),
                                UnitPrice = Utils.CustomParse<decimal>(reader["unitprice"]),
                                ExcessAmount = Utils.CustomParse<decimal>(reader["price"])
                            };
                            InvDtllist.Add(BO);

                        }
                    }
                    while (reader.NextResult());
                    reader.Close();
                }
                return InvDtllist;
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

        public bool PostInvoiceDetail(InvoiceDetailBO[] InvoicedetailBO)
        {
            try
            {
                string sql = "dbo.fn_insert_invoicedetail";

                conn = new NpgsqlConnection(connString);
                conn.Open();

                foreach (var detailBO in InvoicedetailBO)
                {
                    using (var cmd = new NpgsqlCommand(sql, conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("_invoicekey",
                            NpgsqlTypes.NpgsqlDbType.Uuid, detailBO.InvoiceKey);
                        cmd.Parameters.AddWithValue("_itemkey",
                            NpgsqlTypes.NpgsqlDbType.Uuid, detailBO.Itemkey);
                        cmd.Parameters.AddWithValue("_description",
                            NpgsqlTypes.NpgsqlDbType.Varchar, detailBO.Description);
                        cmd.Parameters.AddWithValue("_unitprice",
                          NpgsqlTypes.NpgsqlDbType.Numeric, detailBO.UnitPrice);
                        cmd.Parameters.AddWithValue("_qty",
                          NpgsqlTypes.NpgsqlDbType.Integer, detailBO.Quantity);
                        cmd.Parameters.AddWithValue("_extamt",
                          NpgsqlTypes.NpgsqlDbType.Numeric, detailBO.Price);
                        cmd.Parameters.AddWithValue("_container",
                            NpgsqlTypes.NpgsqlDbType.Varchar, detailBO.Container);

                        var invoicedetailkey = cmd.ExecuteScalar();
                        //detailBO.InvoiceLineKey = Guid.Parse(invoicedetailkey.ToString());
                        //return detailBO;
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

        public bool PostInvoiceDetail(InvoiceDetailBO detailBO)
        {
            try
            {
                string sql = "dbo.fn_insert_invoicedetail";
                conn = new NpgsqlConnection(connString);
                conn.Open();

                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("_invoicekey",
                        NpgsqlTypes.NpgsqlDbType.Uuid, detailBO.InvoiceKey);
                    cmd.Parameters.AddWithValue("_itemkey",
                        NpgsqlTypes.NpgsqlDbType.Uuid, detailBO.Itemkey);
                    cmd.Parameters.AddWithValue("_description",
                        NpgsqlTypes.NpgsqlDbType.Varchar, detailBO.Description);
                    cmd.Parameters.AddWithValue("_unitprice",
                      NpgsqlTypes.NpgsqlDbType.Numeric, detailBO.UnitPrice);
                    cmd.Parameters.AddWithValue("_qty",
                      NpgsqlTypes.NpgsqlDbType.Integer, detailBO.Quantity);
                    cmd.Parameters.AddWithValue("_extamt",
                      NpgsqlTypes.NpgsqlDbType.Numeric, detailBO.Price);

                    cmd.Parameters.AddWithValue("_container",
                          NpgsqlTypes.NpgsqlDbType.Varchar, detailBO.Container);

                    var invoicedetailkey = cmd.ExecuteScalar();
                    //detailBO.InvoiceLineKey = Guid.Parse(invoicedetailkey.ToString());
                    //return detailBO;
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


        public InvoiceHeaderBO UpdateInvoice(InvoiceHeaderBO invoice)
        {
            try
            {
                string sql = "dbo.fn_update_invoiceheader";
                conn = new NpgsqlConnection(connString);
                conn.Open();

                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("_invoicekey", NpgsqlTypes.NpgsqlDbType.Uuid, invoice.Invoicekey);
                   
                    // cmd.Parameters.AddWithValue("_invoiceno", NpgsqlTypes.NpgsqlDbType.Integer, invoice.InvoiceNo);
                    cmd.Parameters.AddWithValue("_invoicedate", NpgsqlTypes.NpgsqlDbType.Date, Convert.ToDateTime(invoice.InvoiceDate));
                    cmd.Parameters.AddWithValue("_invoiceamount", NpgsqlTypes.NpgsqlDbType.Numeric, invoice.InvoiceAmt);
                    cmd.Parameters.AddWithValue("_duedate", NpgsqlTypes.NpgsqlDbType.Date, invoice.DueDate);
                    var invoicekey = cmd.ExecuteNonQuery();
                    //invoice.Invoicekey = Guid.Parse(invoicekey.ToString());
                    return invoice;
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
        public bool UpdateInvoiceDetail(InvoiceDetailBO detailBO)
        {          
            try
            {
                string sql = "dbo.fn_update_invoicedetail";
                conn = new NpgsqlConnection(connString);
                conn.Open();

                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("_invoicekey", NpgsqlTypes.NpgsqlDbType.Uuid, detailBO.InvoiceKey);
                    cmd.Parameters.AddWithValue("_invoicelinekey", NpgsqlTypes.NpgsqlDbType.Uuid, detailBO.InvoiceLineKey);
                    cmd.Parameters.AddWithValue("_itemkey", NpgsqlTypes.NpgsqlDbType.Uuid, detailBO.Itemkey);
                    cmd.Parameters.AddWithValue("_container", NpgsqlTypes.NpgsqlDbType.Varchar, detailBO.Container);
                    cmd.Parameters.AddWithValue("_description", NpgsqlTypes.NpgsqlDbType.Varchar, detailBO.Description);
                    cmd.Parameters.AddWithValue("_unitprice", NpgsqlTypes.NpgsqlDbType.Numeric, detailBO.UnitPrice);
                    cmd.Parameters.AddWithValue("_qty", NpgsqlTypes.NpgsqlDbType.Integer, detailBO.Quantity);
                    cmd.Parameters.AddWithValue("_extamt", NpgsqlTypes.NpgsqlDbType.Numeric, detailBO.Price);

                    var invoicedetailkey = cmd.ExecuteNonQuery();
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
        public List<DeliveryOrderBO> GetOrderstoGenerateInvoice()
        {
       
            try
            {
                var DOHeaders = new List<DeliveryOrderBO>();
                // string sql = "dbo.fn_GetOrderstoGenerateInvoice";
                string sql = "dbo.fn_get_orderstogenerateinvoice";
                DeliveryOrderBO bo = new DeliveryOrderBO();

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
                            var orderHeader = new DeliveryOrderBO();
                            orderHeader.OrderDetails = new DeliveryOrderDetailBO();

                            orderHeader.OrderKey = Utils.CustomParse<Guid>(reader["orderkey"]);
                            orderHeader.OrderNo = Utils.CustomParse<string>(reader["orderno"]);
                            orderHeader.OrderDate = Convert.ToDateTime(reader["orderdate"].ToString());
                            //orderHeader.OrderType = Utils.CustomParse<short>(reader["ordertype"]);
                            //orderHeader.BrokerRefNo = Utils.CustomParse<string>(reader["brokerrefno"]);
                            //orderHeader.CustKey = Guid.Parse(reader["custkey"].ToString());
                            //orderHeader.BillToAddress = Utils.CustomParse<Guid>(reader["billtoaddrkey"]);
                            //orderHeader.SourceAddress = Utils.CustomParse<Guid>(reader["sourceaddrkey"]);
                            //orderHeader.DestinationAddress = Utils.CustomParse<Guid>(reader["destinationaddrkey"]);
                            //orderHeader.ReturnAddress = Utils.CustomParse<Guid>(reader["returnaddrkey"]);
                            //orderHeader.Status = Utils.CustomParse<short>(reader["status"]);
                            //orderHeader.StatusDate = Convert.ToDateTime(reader["statusdate"]);
                            //orderHeader.HoldReason = Utils.CustomParse<short>(reader["holdreason"]);
                            //orderHeader.Brokerkey = Utils.CustomParse<Guid>(reader["brokerkey"]);                                                
                            //orderHeader.PortofOriginKey = Utils.CustomParse<Guid>(reader["portoforiginkey"]);
                            //orderHeader.PortofDestinationKey = Utils.CustomParse<Guid>(reader["portofdestinationkey"]);
                            //orderHeader.CarrierKey = Utils.CustomParse<Guid>(reader["carrierkey"]);
                            //orderHeader.VesselName = reader["vesselname"].ToString();
                            //orderHeader.BillofLading = reader["billoflading"].ToString();
                            //orderHeader.BookingNo = reader["bookingno"].ToString();
                            //orderHeader.CutOffDate = Convert.ToDateTime(reader["cutoffdate"]);
                            //orderHeader.Carrier = reader["carrier"].ToString();
                            ////orderHeader.ordertypedescription = reader["ordertypedescription"].ToString();

                            //orderHeader.OrderDetails.OrderDetailKey = Utils.CustomParse<Guid>(reader["orderdetailkey"]);
                            //orderHeader.OrderDetails.ContainerNo = Utils.CustomParse<string>(reader["containerNo"]);
                            //orderHeader.OrderDetails.ContainerSizeDesc = Utils.CustomParse<string>(reader["containerSizeDesc"]);
                            //orderHeader.OrderDetails.ContainerSize = Utils.CustomParse<short>(reader["containerSize"]);
                            //orderHeader.OrderDetails.Chassis = Utils.CustomParse<string>(reader["chassis"]);
                            //orderHeader.OrderDetails.SealNo = Utils.CustomParse<string>(reader["sealNo"]);

                            DOHeaders.Add(orderHeader);
                        }
                    } while (reader.NextResult());
                    reader.Close();
                }

                return DOHeaders;
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


        public DeliveryOrderBO getOrderDatabyKey(string orderkey)
        {          
          try
            {
                string sql = "dbo.fn_get_orderheaderbykey";
                DeliveryOrderBO bo = new DeliveryOrderBO();
                AddressDL DL = new AddressDL();

                conn = new NpgsqlConnection(connString);
                conn.Open();

                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("_orderkey",
                        NpgsqlTypes.NpgsqlDbType.Uuid, Guid.Parse(orderkey));
                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        bo.OrderKey = Utils.CustomParse<Guid>(reader["orderkey"]);
                        bo.OrderNo = reader["orderno"].ToString();
                        var dateAndTime = Convert.ToDateTime(reader["orderdate"].ToString()).ToString("MM/dd/yyyy");
                        bo.OrderDate = Convert.ToDateTime(reader["orderdate"]);
                        //bo.OrderDate = Convert.ToDateTime(reader["orderdate"].ToString());
                        bo.CustKey = Guid.Parse(reader["custkey"].ToString());
                        bo.BillToAddress = Utils.CustomParse<Guid>(reader["billtoaddress"]);
                        bo.BillToAddr = Utils.CustomParse<string>(reader["billtoaddr"]);
                        bo.SourceAddress = Utils.CustomParse<Guid>(reader["sourceaddress"]);
                        bo.SourceAddr = Utils.CustomParse<string>(reader["sourceaddr"]);
                        bo.DestinationAddress = Utils.CustomParse<Guid>(reader["destinationaddress"]);
                        bo.DestinationAddr = Utils.CustomParse<string>(reader["destinationaddr"]);
                        bo.ReturnAddress = Utils.CustomParse<Guid>(reader["returnaddress"]);
                        bo.OrderType = Utils.CustomParse<short>(reader["ordertype"]);
                        bo.Status = Utils.CustomParse<short>(reader["status"]);
                        //bo.StatusDate = Convert.ToDateTime(reader["statusdate"]);
                        bo.HoldReason = Utils.CustomParse<short>(reader["holdreason"]);
                        //bo.HoldDate = Convert.ToDateTime(reader["holdDate"]);
                        //bo.Brokerkey = Utils.CustomParse<Guid>(reader["brokerkey"]);
                        bo.BrokerName = reader["brokername"].ToString();
                        bo.BrokerId = reader["brokerid"].ToString();
                        bo.BrokerRefNo = reader["brokerrefno"].ToString();
                        bo.PortofOriginKey = Utils.CustomParse<Guid>(reader["portoforiginkey"]);
                        // bo.PortofDestinationKey = Utils.CustomParse<Guid>(reader["portofdestinationkey"]);
                        bo.CarrierKey = Utils.CustomParse<Guid>(reader["carrierkey"]);
                        bo.VesselName = reader["vesselname"].ToString();
                        bo.BillofLading = reader["billoflading"].ToString();
                        bo.BookingNo = reader["bookingno"].ToString();
                        //bo.CutOffDate = Utils.CustomParse<string>(reader["cutoffdate"]);
                        //bo.CutOffDate = Convert.ToDateTime(reader["cutoffdate"]);
                        //bo.IsHazardous = Utils.CustomParse<bool>(reader["ishazardous"]);
                        //bo.Priority = Utils.CustomParse<short>(reader["priority"]);
                        bo.CreatedDate = Convert.ToDateTime(reader["createdate"]);
                        bo.CreatedBy = Utils.CustomParse<Guid>(reader["createuserkey"]);
                        bo.Comment = Utils.CustomParse<string>(reader["commentdesc"]);
                        bo.statusdescription = reader["statusdescription"].ToString();
                        bo.ordertypedescription = reader["ordertypedescription"].ToString();

                        bo.BillToAddressBO = DL.GetAddressByKey(bo.BillToAddress);
                        //bo.BrokerAddressBO = GetAddress(bo.Brokerkey);
                        bo.ReturnAddressBO = DL.GetAddressByKey(bo.ReturnAddress);
                        bo.SourceAddressBO = DL.GetAddressByKey(bo.SourceAddress);
                        bo.DestinationAddressBO = DL.GetAddressByKey(bo.DestinationAddress);

                    }
                    reader.Close();
                    return bo;
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

        public IList<ThinInvoiceBO> AutoPullInvoiceCosts(string orderKey)
        {
           
            try
            {
                var InvoiceTotals = new List<ThinInvoiceBO>();
                string sql = "dbo.fn_autopullinvoicetotals";

                conn = new NpgsqlConnection(connString);
                conn.Open();

                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("_orderkey",
                  NpgsqlTypes.NpgsqlDbType.Uuid, Guid.Parse(orderKey));
                    var reader = cmd.ExecuteReader();
                    do
                    {
                        while (reader.Read())
                        {
                            ThinInvoiceBO bo = new ThinInvoiceBO();

                            bo.ContainerNo = Utils.CustomParse<string>(reader["containerno"]);
                            bo.ItemId = Utils.CustomParse<string>(reader["itemid"]);
                            bo.UnitPrice = Convert.ToDouble(reader["unitprice"].ToString());
                            bo.ItemKey = Utils.CustomParse<Guid>(reader["itemkey"].ToString());
                            InvoiceTotals.Add(bo);
                        }
                    } while (reader.NextResult());
                    reader.Close();
                }

                return InvoiceTotals;
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
        /// Get Containers 
        /// </summary>
        /// <param name="OrderKey"></param>
        /// <returns></returns>
        public List<ContainerBO> GetContainers(string OrderKey)
        {
         
            try
            {
                var ContainerList = new List<ContainerBO>();
                var sql = "dbo.fn_getcontainerlistbyorderkey";

                conn = new NpgsqlConnection(connString);
                conn.Open();

                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("_orderkey", NpgsqlTypes.NpgsqlDbType.Uuid, Guid.Parse(OrderKey));

                    var reader = cmd.ExecuteReader();
                    do
                    {
                        while (reader.Read())
                        {
                            var BO = new ContainerBO
                            {
                                ContainerNo = Utils.CustomParse<string>(reader["containerno"]),
                                ContainerSize = Utils.CustomParse<short>(reader["containersize"]),
                                ContainerSizeDesc = Utils.CustomParse<string>(reader["containersizedesc"]),
                                Chassis = Utils.CustomParse<string>(reader["chassis"]),
                                SealNo = Utils.CustomParse<string>(reader["sealno"]),
                                Weight = Utils.CustomParse<string>(reader["weight"])
                            };
                            ContainerList.Add(BO);

                        }
                    }
                    while (reader.NextResult());
                    reader.Close();
                }

                return ContainerList;
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

        public List<InvoiceDetailBO> getinvoicedetail(string invoicekey)
        {
            try
            {
                var invList = new List<InvoiceDetailBO>();
                string sql = "dbo.fn_getinvoicedetail";

                conn = new NpgsqlConnection(connString);
                conn.Open();

                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("_invoicekey", NpgsqlTypes.NpgsqlDbType.Uuid, Guid.Parse(invoicekey));
                    var reader = cmd.ExecuteReader();
                    do
                    {
                        while (reader.Read())
                        {
                            var inv = new InvoiceDetailBO();
                            inv.InvoiceKey = Guid.Parse(invoicekey);
                            inv.InvoiceLineKey = Utils.CustomParse<Guid>(reader["invoicelinekey"]);
                            inv.Itemkey = Utils.CustomParse<Guid>(reader["itemkey"]);
                            inv.Description = Utils.CustomParse<string>(reader["description"]);
                            inv.Container = Utils.CustomParse<string>(reader["container"]);
                            inv.Price = Utils.CustomParse<decimal>(reader["unitprice"]);
                            inv.Quantity = Utils.CustomParse<int>(reader["quantity"]);
                            inv.UnitPrice = Utils.CustomParse<decimal>(reader["price"]);

                            invList.Add(inv);
                        }
                    } while (reader.NextResult());
                    reader.Close();
                }

                return invList;
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

        public List<InvoiceHeaderBO> GetInvoiceHeaderList()
        {         

            try
            {
                var invList = new List<InvoiceHeaderBO>();
                string sql = "dbo.fn_getinvoicelist";

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
                            var inv = new InvoiceHeaderBO();
                            inv.OrderKey = Utils.CustomParse<Guid>(reader["orderkey"]);
                            inv.Invoicekey = Utils.CustomParse<Guid>(reader["invoicekey"]);
                            inv.InvoiceNo = Utils.CustomParse<int>(reader["invoiceno"]);
                            inv.InvoiceDate = Utils.CustomParse<DateTime>(reader["invoicedate"]);
                            inv.CustKey = Utils.CustomParse<Guid>(reader["custkey"]);
                            inv.BilltoAddrKey = Utils.CustomParse<Guid>(reader["billtoaddrkey"]);
                            inv.InvoiceAmt = Utils.CustomParse<decimal>(reader["invoiceamount"]);
                            inv.DueDate = Utils.CustomParse<DateTime>(reader["duedate"]);
                            inv.CustName = Utils.CustomParse<string>(reader["custname"]);

                            invList.Add(inv);
                        }
                    } while (reader.NextResult());
                    reader.Close();
                }

                return invList;
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



        public List<RateBO> getorderratesbykey(string orderkey)
        {
            try
            {
                var rateList = new List<RateBO>();
                string sql = "dbo.fn_get_orderratesbykey";

                conn = new NpgsqlConnection(connString);
                conn.Open();


                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("_orderkey", NpgsqlTypes.NpgsqlDbType.Uuid, Guid.Parse(orderkey));
                    var reader = cmd.ExecuteReader();
                    do
                    {
                        while (reader.Read())
                        {
                            var rate = new RateBO();

                            rate.containerno = Utils.CustomParse<string>(reader["containerno"]);
                            rate.itemkey = Utils.CustomParse<Guid>(reader["itemkey"]);
                            rate.itemid = Utils.CustomParse<string>(reader["itemid"]);
                            rate.description = Utils.CustomParse<string>(reader["itemdesc"]);
                            rate.unitprice = Utils.CustomParse<decimal>(reader["unitprice"]);
                           // rate.baserate = Utils.CustomParse<decimal>(reader["baserate"]);
                            rateList.Add(rate);
                        }
                    } while (reader.NextResult());
                    reader.Close();
                }

                return rateList;
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
        /// /
        /// </summary>
        /// <returns></returns>
        public Int64 GetInvoiceMaxcount()
        {          
            try
            {
                string sql = "SELECT COUNT(*) AS cnt FROM dbo.invoiceheader";

                conn = new NpgsqlConnection(connString);
                conn.Open();

                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.CommandType = System.Data.CommandType.Text;
                    object Obj = cmd.ExecuteScalar();
                    if (Obj != null)
                    {
                        return (Int64)cmd.ExecuteScalar();
                    }
                    else return 0;
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

        public string getOrderkeybyInvoicekey(string invoicekey)
        {
            try
            {
                string sql = "SELECT orderkey FROM dbo.tms_orderinvoices where invoicekey = " +
                   " '" + invoicekey + "' ";

                conn = new NpgsqlConnection(connString);
                conn.Open();

                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.CommandType = System.Data.CommandType.Text;                
                    //cmd.Parameters.AddWithValue("_invoicekey", NpgsqlTypes.NpgsqlDbType.Uuid, Guid.Parse(invoicekey));
                   
                    var reader = cmd.ExecuteScalar();

                    return reader.ToString();
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
