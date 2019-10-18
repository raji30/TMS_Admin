using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMS.BusinessObjects;

namespace TMS.Data
{
    public class DispatchDeliveryDL
    {
        string connString = "host=localhost;Username=postgres;Password=TMS@123;Database=App_model";
        NpgsqlConnection connection;
        public DispatchDeliveryDL()
        {
            connection = new NpgsqlConnection(connString);
            connection.Open();
        }

        public List<DeliveryOrderDetailBO> GetOrderstoDispatchDelivery()
        {
            string sql = "dbo.fn_get_orders_to_dispatch_delivery";
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
                            orderDetail.ContainerNo = Utils.CustomParse<string>(reader["containerno"]);
                            orderDetail.ContainerSize = Utils.CustomParse<short>(reader["containersize"]);
                            orderDetail.ContainerSizeDesc = Utils.CustomParse<string>(reader["containersizeDesc"]);
                            orderDetail.Chassis = Utils.CustomParse<string>(reader["chassis"]);
                            orderDetail.SealNo = Utils.CustomParse<string>(reader["sealno"]);
                            orderDetail.Weight = Utils.CustomParse<string>(reader["weight"]);
                            orderDetail.AppDateFrom = Utils.CustomParse<string>(reader["apptdatefrom"]);
                            orderDetail.AppDateTo = Utils.CustomParse<string>(reader["apptdateto"]);
                            orderDetail.PickupDateTime = Utils.CustomParse<string>(reader["scheduledarrival"]);
                            orderDetail.DropOffDateTime = Utils.CustomParse<string>(reader["scheduleddeparture"]);

                            orderDetails.Add(orderDetail);
                        }
                    }
                    while (reader.NextResult());
                }
            }
            return orderDetails;
        }

    }
}
