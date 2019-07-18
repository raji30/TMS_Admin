using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMS.BusinessObjects;

namespace TMS.Data
{
    public class DispatchAssignmentDL
    {
        string connString = "host=localhost;Username=postgres;Password=TMS@123;Database=App_model";
        NpgsqlConnection connection;
        public DispatchAssignmentDL()
        {
            connection = new NpgsqlConnection(connString);
            connection.Open();
        }
       
        public List<DeliveryOrderDetailBO> GetOrderstoDispatchAssignment()
        {
            string sql = "dbo.fn_get_orders_to_dispatch_assignment";
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

    }
}
