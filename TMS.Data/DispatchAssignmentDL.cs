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
    public class DispatchAssignmentDL
    {
        string connString;//= "host=localhost;Username=postgres;Password=TMS@123;Database=App_model";      
        NpgsqlConnection conn;
        NpgsqlCommand cmd;

        public DispatchAssignmentDL()
        {
            connString = ConfigurationManager.ConnectionStrings["App_model"].ConnectionString;
        }

        public List<DeliveryOrderDetailBO> GetOrderstoDispatchAssignment()
        {           
           try
            {
                string sql = "dbo.fn_get_orders_to_dispatch_assignment";
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

    }
}
