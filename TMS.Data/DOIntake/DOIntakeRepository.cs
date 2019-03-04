using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMS.BusinessObjects;
using TMS.BusinessObjects.Enum;
using TMS.Data.TableOperations;

namespace TMS.Data.DOIntake
{
    public class DOIntakeRepository
    {
        private string connString = string.Empty;
        public DOIntakeRepository()
        {
            connString = "host=localhost;Username=postgres;Password=Abc1234!;Database=App_Model";
        }
       
        public OrderHeaderBO GetTMSOrderHeader(Guid orderkey)
        {
            OrderHeaderBO obj = new OrderHeaderBO();
            using (var cmd = new NpgsqlCommand("SELECT * from dbo.fn_get_tms_order_header(@orderkey)", new NpgsqlConnection(connString)))
            {
                cmd.Parameters.Add(new NpgsqlParameter("@orderkey", DbType.Guid) { Direction = ParameterDirection.Input });
                using (var reader = cmd.ExecuteReader())
                {
                    obj.OrderNo = reader["orderno"].ToString();
                    obj.OrderDate = DateTime.Parse(reader["orderdate"].ToString());
                    obj.BookingNo = reader["bookingno"].ToString();
                    obj.BrokerRefNo = reader["brokerrefno"].ToString();
                   // obj.Customer = new CustomerRepository().GetbyId(Guid.Parse(reader["custkey"].ToString()));
                    obj.Broker = new BrokerRepository().GetbyId(Guid.Parse(reader["brokerkey"].ToString()));
                    //obj.Carrier =  Guid.Parse(reader["carrierkey"].ToString());
                    obj.SourceAddress = GetAddress(Guid.Parse(reader["sourceaddress"].ToString()));
                    obj.DestinationAddress = GetAddress(Guid.Parse(reader["destinationaddress"].ToString()));
                    obj.Status = Int16.Parse(reader["status"].ToString());
                    obj.VesselName = reader["vesselname"].ToString();
                   // obj.BillToAddress = Guid.Parse(reader["billingaddress"].ToString());
                   // obj.destinationaddrkey = Guid.Parse(reader["destinationaddrkey"].ToString());
                   // obj.statusdate = DateTime.Parse(reader["statusdate"].ToString());
                }
                return obj;
            }
        }

        public AddressBO GetAddress(Guid AddrKey)
        {
            AddressBO _address = new AddressBO();
            using (var cmd = new NpgsqlCommand("SELECT * from dbo.fn_get_address(@addrkey)", new NpgsqlConnection(connString)))
            {
                cmd.Parameters.Add(new NpgsqlParameter("@addrkey", DbType.Guid) { Direction = ParameterDirection.Input });
                using (var reader = cmd.ExecuteReader())
                {
                    _address.Name = reader["addrname"].ToString();
                    _address.Address1 = reader["address1"].ToString();
                    _address.Address2 = reader["address2"].ToString();
                    _address.City = reader["city"].ToString();
                    _address.State = reader["state"].ToString();
                    _address.Country = reader["country"].ToString();
                    _address.Zip = reader["zip"].ToString();
                    _address.Phone = reader["phone"].ToString();
                    _address.Fax = reader["fax"].ToString();
                    _address.WebSite = reader["website"].ToString();
                    _address.Email = reader["email"].ToString();
                }
            }
            return _address;
        }

       
    }
}
