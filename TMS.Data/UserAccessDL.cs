using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using TMS.Data.TableOperations;

namespace TMS.Data
{
   public class UserAccessDL
    {
        string securityconnString = "host=localhost;port=5432;Username=postgres;Password=Abc1234!;Database=App_Security;";
        NpgsqlConnection securityconnection;
        public UserAccessDL()
        {
            securityconnection = new NpgsqlConnection(securityconnString);
        }

        public userinfo Login(string userName, string password)
        {
            using (var cmd = new NpgsqlCommand("SELECT * from dbo.userinfo where userId= @userName and password = @password ", securityconnection))
            {
                securityconnection.Open();
                cmd.Parameters.AddWithValue("userName", userName);
                cmd.Parameters.AddWithValue("password", password);
                userinfo u_info = null;
               var reader= cmd.ExecuteReader();
                while(reader.Read())
                {
                    u_info = new userinfo();
                    u_info.userkey = Guid.Parse (reader["userkey"].ToString());
                    u_info.firstname = reader["firstname"].ToString();
                    u_info.lastname = reader["lastname"].ToString();
                    u_info.status = Convert.ToInt16(reader["status"].ToString());
                    u_info.lastlogindate = Convert.ToDateTime(reader["lastlogindate"].ToString());
                    u_info.loginattempts = Convert.ToInt16(reader["loginattempts"].ToString());
                }
                securityconnection.Close();
                return u_info;
            }
        }
        
        public bool isAuthorized (string userName, string companyName)
        {
            CompanyRepository cRepo = new CompanyRepository();
           company c =  cRepo.GetbyField(companyName);
            if(c != null)
            {
               var authUser=  c.userinfoes.FirstOrDefault(uc => uc.userid == userName);
                if (authUser != null)
                    return true;
                else return false;
            }
            return false;
        }

        public bool resetPassword (string userName, string newPassword)
        {
            using (var cmd = new NpgsqlCommand("update userInfo set password = @password where userId= @userName", securityconnection))
            {
                cmd.Parameters.AddWithValue("userName", userName);
                cmd.Parameters.AddWithValue("password", newPassword);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                   
                   bool returnval = Convert.ToBoolean(reader["status"].ToString());
                    return returnval;
                }
                return false;
            }
        }
    }
}
