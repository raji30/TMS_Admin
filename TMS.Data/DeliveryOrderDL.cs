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
    public class DeliveryOrderDL
    {
        string connString = "host=localhost;Username=postgres;Password=Abc1234!;Database=App_model";
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
                        NpgsqlTypes.NpgsqlDbType.Date, orderBO.OrderDate);
                    cmd.Parameters.AddWithValue("_custkey",
                        NpgsqlTypes.NpgsqlDbType.Uuid, orderBO.CustKey);
                    cmd.Parameters.AddWithValue("_billtoaddrkey",
                       NpgsqlTypes.NpgsqlDbType.Uuid, orderBO.BillToAddress?.AddrKey);
                    cmd.Parameters.AddWithValue("_sourceaddrkey",
                       NpgsqlTypes.NpgsqlDbType.Uuid, orderBO.SourceAddress?.AddrKey);
                    cmd.Parameters.AddWithValue("_destinationaddrkey",
                       NpgsqlTypes.NpgsqlDbType.Uuid, orderBO.DestinationAddress?.AddrKey);
                    cmd.Parameters.AddWithValue("_returnaddrkey",
                       NpgsqlTypes.NpgsqlDbType.Uuid, orderBO.ReturnAddress?.AddrKey);
                    cmd.Parameters.AddWithValue("_source",
                       NpgsqlTypes.NpgsqlDbType.Smallint, orderBO.Source);
                    cmd.Parameters.AddWithValue("_ordertype",
                       NpgsqlTypes.NpgsqlDbType.Smallint, orderBO.OrderType);
                    cmd.Parameters.AddWithValue("_status",
                       NpgsqlTypes.NpgsqlDbType.Smallint, orderBO.Status);
                    cmd.Parameters.AddWithValue("_statusdate",
                       NpgsqlTypes.NpgsqlDbType.Uuid, orderBO.StatusDate);
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
                      NpgsqlTypes.NpgsqlDbType.Timestamp, orderBO.CutOffDate);
                    cmd.Parameters.AddWithValue("_ishazardous",
                      NpgsqlTypes.NpgsqlDbType.Boolean, orderBO.IsHazardous);
                    cmd.Parameters.AddWithValue("_priority",
                      NpgsqlTypes.NpgsqlDbType.Smallint, orderBO.Priority);
                    cmd.Parameters.AddWithValue("_createuserkey",
                      NpgsqlTypes.NpgsqlDbType.Uuid, orderBO.CreatedBy);
                   var reader= cmd.ExecuteReader();
                    while(reader.Read())
                    {
                        var OrderID = Guid.Parse(reader["orderkey"].ToString());
                        return OrderID;
                    }
                }
            }
            return Guid.Empty;
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
                    cmd.Parameters.AddWithValue("orderkey", 
                        NpgsqlTypes.NpgsqlDbType.Uuid, orderkey);
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
                        bo.OrderDate = Convert.ToDateTime(reader["orderdate"].ToString());
                        bo.CustKey = Guid.Parse(reader["custkey"].ToString());
                        bo.BillToAddress = GetAddress(Guid.Parse(reader["billingAddress"].ToString()));
                        bo.SourceAddress = GetAddress(Guid.Parse(reader["sourceaddress"].ToString()));
                        bo.DestinationAddress = GetAddress(Guid.Parse(reader["destinationaddress"].ToString()));
                        bo.ReturnAddress = GetAddress(Guid.Parse(reader["returnaddress"].ToString()));
                        bo.OrderType = short.Parse(reader["ordertype"].ToString());
                        bo.Status = short.Parse(reader["status"].ToString());
                        bo.StatusDate = DateTime.Parse(reader["statusdate"].ToString());
                        bo.HoldReason = short.Parse(reader["holdreason"].ToString());
                        bo.HoldDate = DateTime.Parse(reader["holdDate"].ToString());
                        bo.BrokerName = reader["brokername"].ToString();
                        bo.BrokerId = reader["brokerid"].ToString();
                        bo.BrokerRefNo = reader["brokerrefno"].ToString();
                        bo.PortofOriginKey = Guid.Parse(reader["portoforiginkey"].ToString());
                        bo.CarrierKey = Guid.Parse(reader["carrierkey"].ToString());
                        bo.VesselName = reader["vesselname"].ToString();
                        bo.BillofLading = reader["billoflading"].ToString();
                        bo.BookingNo = reader["bookingno"].ToString();
                        bo.CutOffDate = DateTime.Parse(reader["cutoffdate"].ToString());
                        bo.IsHazardous = bool.Parse(reader["ishazardous"].ToString());
                        bo.Priority = short.Parse(reader["priority"].ToString());
                        bo.CreatedDate = DateTime.Parse(reader["createdate"].ToString());
                        bo.CreatedBy = Guid.Parse(reader["createuserkey"].ToString());
                        
                    }
                    return bo;
                }
            }
        }

        public AddressBO GetAddress(Guid addrKey)
        {
            AddressBO addBO = new AddressBO();
            AddressRepository repo = new AddressRepository();
           var addr= repo.GetbyId(addrKey);
            addBO.Address1 = addr.address1;
            addBO.Address2 = addr.address2;
            addBO.City = addr.city;
            addBO.State = addr.state;
            addBO.Zip = addr.zipcode;
            addBO.Email = addr.email;
            addBO.Fax = addr.fax;
            addBO.Phone = addr.phone;
            return addBO;
        }

    }
}
