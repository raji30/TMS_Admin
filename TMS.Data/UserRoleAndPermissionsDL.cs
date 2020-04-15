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
    public class UserRoleAndPermissionsDL
    {
        string connString;//= "host=localhost;Username=postgres;Password=TMS@123;Database=App_model";      
        NpgsqlConnection conn;
        NpgsqlCommand cmd;

        public UserRoleAndPermissionsDL()
        {
            connString = ConfigurationManager.ConnectionStrings["App_model"].ConnectionString;
        }

        public List<UserPermissionsBO> getUserPermissionsByUserkey(string userkey)
        {
            try
            {
                string sql = "SELECT permissionkey,userkey, modulename, fview, fnew, fedit, fdelete, status, statusdate FROM dbo.user_permissions where userkey=@userkey and status=1 order by modulename asc";
                List<UserPermissionsBO> permissionlist = new List<UserPermissionsBO>();

                conn = new NpgsqlConnection(connString);
                conn.Open();

                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Parameters.AddWithValue("userkey", NpgsqlTypes.NpgsqlDbType.Uuid, Guid.Parse(userkey));
                    var reader = cmd.ExecuteReader();
                    do
                    {
                        while (reader.Read())
                        {
                            var BO = new UserPermissionsBO();
                            BO.PermissionKey = Guid.Parse(reader["permissionkey"].ToString());
                            BO.UserKey = Guid.Parse(reader["userkey"].ToString());
                            BO.Modulename = Utils.CustomParse<string>(reader["modulename"]);
                            BO.fView = Utils.CustomParse<Int16>(reader["fview"]);
                            BO.fNew = Utils.CustomParse<Int16>(reader["fnew"]);
                            BO.fEdit = Utils.CustomParse<Int16>(reader["fedit"]);
                            BO.fDelete = Utils.CustomParse<Int16>(reader["fdelete"]);
                            permissionlist.Add(BO);                            
                        }
                    }
                    while (reader.NextResult());
                    reader.Close();
                }

                return permissionlist;
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

        public List<UserPermissionsBO> getMenus()
        {
            try
            {
                string sql = "SELECT menukey, menuname, status, statusdate FROM dbo.menu where status=1 order by menuname asc";
                List<UserPermissionsBO> permissionlist = new List<UserPermissionsBO>();

                conn = new NpgsqlConnection(connString);
                conn.Open();

                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.CommandType = System.Data.CommandType.Text;                    
                    var reader = cmd.ExecuteReader();
                    do
                    {
                        while (reader.Read())
                        {
                            var BO = new UserPermissionsBO();                           
                            BO.Modulename = Utils.CustomParse<string>(reader["menuname"]);                           
                            permissionlist.Add(BO);
                        }
                    }
                    while (reader.NextResult());
                    reader.Close();
                }

                return permissionlist;
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

        public IList<Guid> AddUserPermissions(IList<UserPermissionsBO> objList)
        {

            try
            {
                var UserPermissionList = new List<Guid>();             
                string sql = "dbo.fn_insert_userpermissions";

                conn = new NpgsqlConnection(connString);
                conn.Open();

                foreach (var obj in objList)
                {
                    using (var cmd = new NpgsqlCommand(sql, conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("_userkey", NpgsqlTypes.NpgsqlDbType.Uuid, obj.UserKey);
                        cmd.Parameters.AddWithValue("_modulename", NpgsqlTypes.NpgsqlDbType.Varchar, obj.Modulename);
                        cmd.Parameters.AddWithValue("_fview", NpgsqlTypes.NpgsqlDbType.Smallint, obj.fView);
                        cmd.Parameters.AddWithValue("_fnew", NpgsqlTypes.NpgsqlDbType.Smallint, obj.fNew);
                        cmd.Parameters.AddWithValue("_fedit", NpgsqlTypes.NpgsqlDbType.Smallint, obj.fEdit);
                        cmd.Parameters.AddWithValue("_fdelete", NpgsqlTypes.NpgsqlDbType.Smallint, obj.fDelete);                    

                        var reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                var PermissionKey = Guid.Parse(reader[i].ToString());
                                UserPermissionList.Add(PermissionKey);
                            }
                        }

                        reader.Close();
                    }
                }

                return UserPermissionList;
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

        public Guid AddUserPermissions(UserPermissionsBO permission)
        {

            try
            {
                var PermissionKey = Guid.Empty;
                string sql = "dbo.fn_insert_userpermissions";

                conn = new NpgsqlConnection(connString);
                conn.Open();
                
                    using (var cmd = new NpgsqlCommand(sql, conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("_userkey", NpgsqlTypes.NpgsqlDbType.Uuid, permission.UserKey);
                        cmd.Parameters.AddWithValue("_modulename", NpgsqlTypes.NpgsqlDbType.Varchar, permission.Modulename);
                        cmd.Parameters.AddWithValue("_fview", NpgsqlTypes.NpgsqlDbType.Smallint, permission.fView);
                        cmd.Parameters.AddWithValue("_fnew", NpgsqlTypes.NpgsqlDbType.Smallint, permission.fNew);
                        cmd.Parameters.AddWithValue("_fedit", NpgsqlTypes.NpgsqlDbType.Smallint, permission.fEdit);
                        cmd.Parameters.AddWithValue("_fdelete", NpgsqlTypes.NpgsqlDbType.Smallint, permission.fDelete);

                        var reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                            PermissionKey = Guid.Parse(reader[i].ToString());                                
                            }
                        }
                        reader.Close();
                    }                

                return PermissionKey;
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

        public int UpdateUserPermissions(UserPermissionsBO obj)
        {
            try
            {              
                string sql = "dbo.fn_update_userpermissions";
                int returnvalue;
                conn = new NpgsqlConnection(connString);
                conn.Open();

                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("_permissionkey", NpgsqlTypes.NpgsqlDbType.Uuid, obj.PermissionKey);
                    cmd.Parameters.AddWithValue("_userkey", NpgsqlTypes.NpgsqlDbType.Uuid, obj.UserKey);
                    cmd.Parameters.AddWithValue("_modulename", NpgsqlTypes.NpgsqlDbType.Varchar, obj.Modulename);
                    cmd.Parameters.AddWithValue("_fview", NpgsqlTypes.NpgsqlDbType.Smallint, obj.fView);
                    cmd.Parameters.AddWithValue("_fnew", NpgsqlTypes.NpgsqlDbType.Smallint, obj.fNew);
                    cmd.Parameters.AddWithValue("_fedit", NpgsqlTypes.NpgsqlDbType.Smallint, obj.fEdit);
                    cmd.Parameters.AddWithValue("_fdelete", NpgsqlTypes.NpgsqlDbType.Smallint, obj.fDelete);

                    returnvalue = cmd.ExecuteNonQuery();
                }
                
                return returnvalue;
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


        public List<UserRoleBO> getRoles()
        {
            try
            {
                string sql = "SELECT rolekey, descrption FROM dbo.approles  order by descrption asc";
                List<UserRoleBO> rolelist = new List<UserRoleBO>();

                conn = new NpgsqlConnection(connString);
                conn.Open();

                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.CommandType = System.Data.CommandType.Text;
                    var reader = cmd.ExecuteReader();
                    do
                    {
                        while (reader.Read())
                        {
                            var BO = new UserRoleBO();
                            BO.rolekey = Utils.CustomParse<Guid>(reader["rolekey"]);
                            BO.description = Utils.CustomParse<string>(reader["descrption"]);
                            rolelist.Add(BO);
                        }
                    }
                    while (reader.NextResult());
                    reader.Close();
                }

                return rolelist;
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
        public UserRoleBO getUserRoleByRolekey(string rolekey)
        {
            try
            {
                string sql = "SELECT rolekey, descrption FROM dbo.approles where rolekey=@rolekey asc";
                var BO = new UserRoleBO();

                conn = new NpgsqlConnection(connString);
                conn.Open();

                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Parameters.AddWithValue("rolekey", NpgsqlTypes.NpgsqlDbType.Uuid, Guid.Parse(rolekey));
                    var reader = cmd.ExecuteReader();
                    do
                    {
                        while (reader.Read())
                        {                            
                            BO.rolekey = Utils.CustomParse<Guid>(reader["rolekey"]);
                            BO.description = Utils.CustomParse<string>(reader["descrption"]);                            
                        }
                    }
                    while (reader.NextResult());
                    reader.Close();
                }

                return BO;
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

        public UserRoleBO getUserRoleByUserkey(string userkey)
        {
            try
            {
                string sql = "SELECT rolekey, userkey FROM dbo.userroles where userkey=@userkey";
                var BO = new UserRoleBO();

                conn = new NpgsqlConnection(connString);
                conn.Open();

                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Parameters.AddWithValue("userkey", NpgsqlTypes.NpgsqlDbType.Uuid, Guid.Parse(userkey));
                    var reader = cmd.ExecuteReader();
                    do
                    {
                        while (reader.Read())
                        {
                            BO.rolekey = Utils.CustomParse<Guid>(reader["rolekey"]);
                            BO.userkey = Utils.CustomParse<Guid>(reader["userkey"]);
                        }
                    }
                    while (reader.NextResult());
                    reader.Close();
                }

                return BO;
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
        public int AddUserRole(UserRoleBO obj)
        {
            try
            {
                int returnvalue;
                string sql = "Insert into dbo.userroles (rolekey,userkey)values( @rolekey ,@userkey)";

                conn = new NpgsqlConnection(connString);
                conn.Open();

                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Parameters.AddWithValue("userkey", NpgsqlTypes.NpgsqlDbType.Uuid, obj.userkey);
                    cmd.Parameters.AddWithValue("rolekey", NpgsqlTypes.NpgsqlDbType.Uuid, obj.rolekey);
                    returnvalue = cmd.ExecuteNonQuery();
                }

                return returnvalue;
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
        public int UpdateUserRole(UserRoleBO obj)
        {
            try
            {                
                int returnvalue;
                string sql = "update dbo.userroles set rolekey = @rolekey where userkey=@userkey";

                conn = new NpgsqlConnection(connString);
                conn.Open();

                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Parameters.AddWithValue("userkey", NpgsqlTypes.NpgsqlDbType.Uuid, obj.userkey);
                    cmd.Parameters.AddWithValue("rolekey", NpgsqlTypes.NpgsqlDbType.Uuid, obj.rolekey);
                    returnvalue = cmd.ExecuteNonQuery();
                }

                return returnvalue;
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
