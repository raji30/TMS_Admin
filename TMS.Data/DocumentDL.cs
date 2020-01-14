using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMS.BusinessObjects;

namespace TMS.Data
{
   public class DocumentDL : BaseConnection
    {
        string connString = "host=localhost;Username=postgres;Password=TMS@123;Database=App_model";
        NpgsqlConnection connection;
        public DocumentDL()
        {
            connection = new NpgsqlConnection(connString);

        }
        public bool InsertDOHeaderDocument(OrderHeaderDocumentBO docBO)
        {
            try
            { 
       
            string sql = "dbo.fn_insert_orderheader_document";
            using (connection)
            {
                if(connection.State.ToString()=="Closed")
                {
                    connection = new NpgsqlConnection(connString);
                }
               
                connection.Open();
                using (var cmd = new NpgsqlCommand(sql, connection))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("documentkey", NpgsqlTypes.NpgsqlDbType.Uuid, docBO.Document.Dockey);
                    cmd.Parameters.AddWithValue("documenttype",NpgsqlTypes.NpgsqlDbType.Smallint, Convert.ToInt16(docBO.Document.DocType));
                    //cmd.Parameters.AddWithValue("createdate",
                    //    NpgsqlTypes.NpgsqlDbType.Date, docBO.Document.CreatedOn);
                    cmd.Parameters.AddWithValue("createuserkey",NpgsqlTypes.NpgsqlDbType.Uuid, docBO.Document.CreatedBy);
                    cmd.Parameters.AddWithValue("originalfilename", NpgsqlTypes.NpgsqlDbType.Varchar, docBO.Document.name);
                    cmd.Parameters.AddWithValue("originalfiletype",  NpgsqlTypes.NpgsqlDbType.Varchar, docBO.Document.FileType);
                    cmd.Parameters.AddWithValue("filesizeinmb",NpgsqlTypes.NpgsqlDbType.Integer, docBO.Document.FileSizeInMB);
                    cmd.Parameters.AddWithValue("orderno", NpgsqlTypes.NpgsqlDbType.Varchar, docBO.OrderNo);
                    
                   var reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            var result = bool.Parse(reader[0].ToString());
                            return result;
                        }
                    }
                }
            }
            catch
            {
                throw ;
            }
            return false;
        }

        public IList<DocumentBO> GetSupportingDocumentsForDO(Guid Orderkey)
        {
            var connection = OpenConnection();
            string sql = "SELECT originalfilename, originalfiletype from dbo.document d (nolock) inner join dbo.tms_orderheaderdocuments dod" +
                "on d.documentkey =dod.documentkey where dod.orderkey = _orderKey";
            var list = new List<DocumentBO>();
           
                using (var cmd = new NpgsqlCommand(sql, connection))
            {

                cmd.Parameters.AddWithValue("_orderKey", Orderkey);
                cmd.CommandType = System.Data.CommandType.Text;
                var reader = cmd.ExecuteReader();
               
                while (reader.Read())
                {

                    var docDO = new DocumentBO()
                    {
                        name = Convert.ToString(reader["originalfilename"]),
                        FileType = Convert.ToString(reader["originalfiletype"])
                    };
                    list.Add(docDO);
                }

            }
                connection.Close();
                return list;
            }


        public IList<DocumentBO> GetSupportingDocuments(string Orderno)
        {
            
            string sql = "SELECT d.originalfilename,d.filesizeinmb, d.originalfiletype from dbo.document d inner join dbo.tms_orderheaderdocuments dod" +
                @" on d.documentkey =dod.documentkey where dod.orderno ="+ "'"+Orderno+"'";
            var list = new List<DocumentBO>();
            using (connection)
            {
                if (connection.State.ToString() == "Closed")
                {
                    connection = new NpgsqlConnection(connString);
                    connection.Open();
                }
               
               // connection.Open();
                using (var cmd = new NpgsqlCommand(sql, connection))
            {
               // cmd.Parameters.AddWithValue("_orderno", Orderno);
                cmd.CommandType = System.Data.CommandType.Text;
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {

                    var docDO = new DocumentBO()
                    {
                        name = Convert.ToString(reader["originalfilename"]),
                        FileType = Convert.ToString(reader["originalfiletype"]),
                        size= Convert.ToInt32(reader["filesizeinmb"]),
                    };
                    list.Add(docDO);
                }

                }
            }
            connection.Close();
            return list;
        }
    }

    }


