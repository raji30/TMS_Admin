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

namespace TMS.Api.Controllers
{
    public class FileUploadController : ApiController
    {
        [HttpPost]
        public Task<HttpResponseMessage> Post(string DO)
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

                        string guid = Guid.NewGuid().ToString();

                        File.Move(finfo.FullName, Path.Combine(root, file.Headers.ContentDisposition.FileName.Replace("\"", "")));
                        }
                        return new HttpResponseMessage()
                        {
                            Content = new StringContent("File uploaded.")
                        };
                    }
                );
                return task;
              
            }
            catch 
            {
                return null;
            }
        }
    }
}
