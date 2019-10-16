using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMS.BusinessObjects;

namespace TMS.Data
{
   public class InvoiceDL
    {
        //string connString = "host=localhost;port=5432;Username=postgres;Password=TMS@123;Database=App_model;";
        string connString = "host=localhost;Username=postgres;Password=TMS@123;Database=App_model";
        NpgsqlConnection appmodelConnection;
        public InvoiceDL()
        {
            appmodelConnection = new NpgsqlConnection(connString);
          
        }
        public InvoiceHeaderBO GetInvoice(string invoiceNo)
        {
            string sql = "fn_getInvoice";
            var invoiceHeader = new InvoiceHeaderBO();
            appmodelConnection.Open();
            using (var cmd = new NpgsqlCommand(sql, appmodelConnection))
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
                return invoiceHeader;
            }
        }
        public InvoiceHeaderBO PostInvoice(InvoiceHeaderBO invoice)
        {
            string sql = "dbo.fn_insertinvoiceheader";
            using (appmodelConnection)
            {
                appmodelConnection.Open();
                using (var cmd = new NpgsqlCommand(sql, appmodelConnection))
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
                    cmd.Parameters.AddWithValue("_billtocopyaddrkey",
                      NpgsqlTypes.NpgsqlDbType.Uuid, invoice.BilltoAddrCopy);
                    cmd.Parameters.AddWithValue("_invoiceamount",
                      NpgsqlTypes.NpgsqlDbType.Numeric, invoice.InvoiceAmt);
                    cmd.Parameters.AddWithValue("_duedate",
                      NpgsqlTypes.NpgsqlDbType.Date, invoice.DueDate);
                    cmd.Parameters.AddWithValue("_invoicetype",
                  NpgsqlTypes.NpgsqlDbType.Smallint, invoice.InvoiceType);
                    cmd.Parameters.AddWithValue("_orderdetailkey",
                NpgsqlTypes.NpgsqlDbType.Uuid, invoice.OrderDetailKey);
                    var invoicekey = cmd.ExecuteScalar();
                    invoice.Invoicekey =Guid.Parse(invoicekey.ToString());
                    return invoice;
                }
            }
        }
        public InvoiceHeaderBO GetInvoicebyOrderDetailKey(string OrderDetailKey)
        {
            string sql = "fn_getInvoicebyOrderDetailKey";
            var invoiceHeader = new InvoiceHeaderBO();
            appmodelConnection.Open();
            using (var cmd = new NpgsqlCommand(sql, appmodelConnection))
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
                return invoiceHeader;
            }
        }
        public List<InvoiceDetailBO> GetInvoiceDetail(string OrderDetailKey)
        {
            var InvDtllist = new List<InvoiceDetailBO>();
            var sql = "fn_getinvoicedetail";
            using (var cmd = new NpgsqlCommand(sql, appmodelConnection))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("_orderdetailkey",
                   NpgsqlTypes.NpgsqlDbType.Uuid, Guid.Parse(OrderDetailKey));
                var reader = cmd.ExecuteReader();
                do
                {
                    while (reader.Read())
                    {
                        var BO = new InvoiceDetailBO
                        {
                            Itemkey = Utils.CustomParse<Guid>(reader["itemkey"]),
                            Description = Utils.CustomParse<string>(reader["description"]),
                            Price = Utils.CustomParse<double>(reader["price"]),
                            ItemType = Utils.CustomParse<short>(reader["itemtype"]),
                            Quantity = Utils.CustomParse<decimal>(reader["quantity"]),
                            InvoiceDescription = reader["invoicedescription"].ToString(),
                            UnitPrice = Utils.CustomParse<decimal>(reader["unitprice"]),
                            ExcessAmount = Utils.CustomParse<decimal>(reader["excessamount"])
                        };
                        InvDtllist.Add(BO);

                    }
                }
                while (reader.NextResult());
            }
            return InvDtllist;
        }

        public InvoiceDetailBO PostInvoiceDetail(InvoiceDetailBO detailBO)
        {
            string sql = "fn_insertinvoicedetail";
            using (appmodelConnection)
            {
                appmodelConnection.Open();
                using (var cmd = new NpgsqlCommand(sql, appmodelConnection))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("_invoicekey",
                        NpgsqlTypes.NpgsqlDbType.Varchar, detailBO.InvoiceKey);
                    cmd.Parameters.AddWithValue("_itemkey",
                        NpgsqlTypes.NpgsqlDbType.Uuid, detailBO.Itemkey);
                    cmd.Parameters.AddWithValue("_description",
                        NpgsqlTypes.NpgsqlDbType.Varchar, detailBO.Description);
                    cmd.Parameters.AddWithValue("_unitprice",
                      NpgsqlTypes.NpgsqlDbType.Uuid, detailBO.UnitPrice);
                    cmd.Parameters.AddWithValue("qty",
                      NpgsqlTypes.NpgsqlDbType.Uuid, detailBO.Quantity);
                    cmd.Parameters.AddWithValue("extamt",
                      NpgsqlTypes.NpgsqlDbType.Double, detailBO.ExcessAmount);
                  
                    var invoicedetailkey = cmd.ExecuteScalar();
                    detailBO.InvoiceLineKey = Guid.Parse(invoicedetailkey.ToString());
                    return detailBO;
                }
            }
        }


        public List<DeliveryOrderBO> GetOrderstoGenerateInvoice()
        {
            var DOHeaders = new List<DeliveryOrderBO>();           
            string sql = "dbo.fn_GetOrderstoGenerateInvoice";
            DeliveryOrderBO bo = new DeliveryOrderBO();
            using (appmodelConnection)
            {
                appmodelConnection.Open();
                using (var cmd = new NpgsqlCommand(sql, appmodelConnection))
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
                            //orderHeader.ReturnAddress = Utils.CustomParse<Guid>(reader["returnaddrkey"]);
                            //orderHeader.Status = Utils.CustomParse<short>(reader["status"]);
                            //orderHeader.StatusDate = Convert.ToDateTime(reader["statusdate"]);
                            //orderHeader.HoldReason = Utils.CustomParse<short>(reader["holdreason"]);
                            //orderHeader.Brokerkey = Utils.CustomParse<Guid>(reader["brokerkey"]);                                                
                            //orderHeader.PortofOriginKey = Utils.CustomParse<Guid>(reader["portoforiginkey"]);
                            //orderHeader.PortofDestinationKey = Utils.CustomParse<Guid>(reader["portofdestinationkey"]);
                            //orderHeader.CarrierKey = Utils.CustomParse<Guid>(reader["carrierkey"]);
                            orderHeader.VesselName = reader["vesselname"].ToString();
                            orderHeader.BillofLading = reader["billoflading"].ToString();
                            orderHeader.BookingNo = reader["bookingno"].ToString();
                            orderHeader.CutOffDate = Convert.ToDateTime(reader["cutoffdate"]);
                            orderHeader.Carrier = reader["carrier"].ToString();
                            //orderHeader.ordertypedescription = reader["ordertypedescription"].ToString();

                            orderHeader.OrderDetails.OrderDetailKey = Utils.CustomParse<Guid>(reader["orderdetailkey"]);
                            orderHeader.OrderDetails.ContainerNo = Utils.CustomParse<string>(reader["containerNo"]);
                            orderHeader.OrderDetails.ContainerSizeDesc = Utils.CustomParse<string>(reader["containerSizeDesc"]);
                            orderHeader.OrderDetails.ContainerSize = Utils.CustomParse<short>(reader["containerSize"]);
                            orderHeader.OrderDetails.Chassis = Utils.CustomParse<string>(reader["chassis"]);
                            orderHeader.OrderDetails.SealNo = Utils.CustomParse<string>(reader["sealNo"]);

                            DOHeaders.Add(orderHeader);
                        }
                    } while (reader.NextResult());
                }                
            }
            return DOHeaders;
        }


        public Int64 GetInvoiceMaxcount()
        {
            string sql = "SELECT COUNT(*) AS cnt FROM dbo.invoiceheader";
            using (appmodelConnection)
            {
                appmodelConnection.Open();
                using (var cmd = new NpgsqlCommand(sql, appmodelConnection))
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
        }
    }
}
