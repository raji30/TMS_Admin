using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace TMS.Api.Controllers
{
    [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]
    [JwtAuthentication]
    public class FileDownloadController : ApiController
    {
        [HttpPost]
        [Route("FileDownload")]
        public async Task<FileStream> post(string fileName,string orderno)
        {
            string root = System.Web.HttpContext.Current.Server.MapPath("~/App_Data/Files");
            string filen = @"C:\Users\Arun\Documents\GitHub\TMS_Admin\TMS.Api\App_Data\Files\DOJO200001\screen shots sandeep.docx";
            string test = @"D:\DOJO200001\screen shots sandeep.docx";
            //var currentDirectory = System.IO.Directory.GetCurrentDirectory();           
            //currentDirectory = currentDirectory + "\\src\\assets";
            //var file = Path.Combine(Path.Combine(currentDirectory, "attachments"), "DOJO200001\\screen shots sandeep.docx");// fileName);
            return new FileStream(fileName, FileMode.Open, FileAccess.Read);
        }

        [HttpGet]//http get as it return file 
        [Route("GetTestFile")]
        public HttpResponseMessage GetTestFile(string fileName, string orderno)
        {
            //below code locate physcial file on server 
           // var localFilePath = HttpContext.Current.Server.MapPath("~/timetable.zip");
            var localFilePath = String.Concat(HttpContext.Current.Server.MapPath("~/App_Data/Files/"), orderno + "\\" + fileName)  ;
           
            HttpResponseMessage response = null;
            if (!File.Exists(localFilePath))
            {
                //if file not found than return response as resource not present 
                response = Request.CreateResponse(HttpStatusCode.Gone);
            }
            else
            {
                //if file present than read file 
                var fStream = new FileStream(localFilePath, FileMode.Open, FileAccess.Read);
                //compose response and include file as content in it
                response = new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StreamContent(fStream)
                };
                //set content header of reponse as file attached in reponse
                response.Content.Headers.ContentDisposition =
                new ContentDispositionHeaderValue("attachment")
                {
                    FileName = Path.GetFileName(fStream.Name)
                };
                //set the content header content type as application/octet-stream as it returning file as reponse 
                response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
            }
            return response;
        }
    }
}
