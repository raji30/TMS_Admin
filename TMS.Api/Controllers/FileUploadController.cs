using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using TMS.BusinessLayer;
using TMS.BusinessObjects;
using TMS.Data;
using static TMS.BusinessObjects.Enums;

namespace TMS.Api.Controllers
{
    public class FileUploadController : ApiController
    {
        [HttpPost]
        public Task<HttpResponseMessage> Post(string DO,string CreatedBy)
        {

            try
            {
                var fileuploadPath = HttpContext.Current.Server.MapPath("~/App_Data/Files/");
                var httpContext = HttpContext.Current;
              
                HttpRequestMessage request = this.Request;
                if (!request.Content.IsMimeMultipartContent())
                {
                    throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.UnsupportedMediaType));
                }

                string root = System.Web.HttpContext.Current.Server.MapPath("~/App_Data/Files");
                MultipartFormDataStreamProvider provider = new MultipartFormDataStreamProvider(root);

                var task = request.Content.ReadAsMultipartAsync(provider).
                    ContinueWith(o =>
                    {
                        foreach(var file in provider.FileData)
                        { 
                        FileInfo finfo = new FileInfo(file.LocalFileName);
                            DocumentBO documentBO = new DocumentBO
                            {
                                Dockey = Guid.NewGuid(),
                                DocType = (DocType)Enum.Parse(typeof(DocType), finfo.Extension),
                                CreatedBy = new UserInfoRepository().GetbyField(CreatedBy).userkey,
                                FileSizeInMB = (int)finfo.Length / 1024,
                                FileType = string.Empty, //not sure
                                FileName = finfo.Name
                            };
                            OrderHeaderDocumentBO orderBO = new OrderHeaderDocumentBO
                            {
                                Document = documentBO,
                                Orderkey = Guid.Parse(DO)
                            };

                            File.Move(finfo.FullName, Path.Combine(root, 
                            file.Headers.ContentDisposition.FileName.Replace("\"", "")));
                             DocumentDL dl = new DocumentDL();
                            dl.InsertDOHeaderDocument(orderBO);
                        }
                        return new HttpResponseMessage()
                        {
                            Content = new StringContent("File uploaded.")
                        };
                    }
                ) ;
                return task;
              
            }
            catch 
            {
                return null;
            }
        }


    }
}
