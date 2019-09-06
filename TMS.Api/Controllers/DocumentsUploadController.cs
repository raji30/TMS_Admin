using Swashbuckle.Swagger.Annotations;
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
using System.Web.Http.Cors;

namespace TMS.Api.Controllers
{
    [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]
    public class DocumentsUploadController : ApiController
    {
        [HttpPost]
      
        [SwaggerOperation("FileUpload")]
        public Task<HttpResponseMessage> Post()
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
                        foreach (var file in provider.FileData)
                        {
                            FileInfo finfo = new FileInfo(file.LocalFileName);

                            string guid = Guid.NewGuid().ToString();
                            if (!File.Exists(Path.Combine(root, file.Headers.ContentDisposition.FileName.Replace("\"", ""))))
                            {

                                File.Move(finfo.FullName, Path.Combine(root, file.Headers.ContentDisposition.FileName.Replace("\"", "")));
                            }

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

        [HttpPost]
      
        public HttpResponseMessage UploadFiles()
        {
            int iUploadedCnt = 0;

            // DEFINE THE PATH WHERE WE WANT TO SAVE THE FILES.
            string sPath = "";
            sPath = System.Web.Hosting.HostingEnvironment.MapPath("~/App_Data/Files/");

            System.Web.HttpFileCollection hfc = System.Web.HttpContext.Current.Request.Files;

            // CHECK THE FILE COUNT.
            for (int iCnt = 0; iCnt <= hfc.Count - 1; iCnt++)
            {
                System.Web.HttpPostedFile hpf = hfc[iCnt];

                if (hpf.ContentLength > 0)
                {
                    // CHECK IF THE SELECTED FILE(S) ALREADY EXISTS IN FOLDER. (AVOID DUPLICATE)
                    if (!File.Exists(sPath + Path.GetFileName(hpf.FileName)))
                    {
                        // SAVE THE FILES IN THE FOLDER.
                        hpf.SaveAs(sPath + Path.GetFileName(hpf.FileName));
                        iUploadedCnt = iUploadedCnt + 1;
                    }
                }
            }

            // RETURN A MESSAGE (OPTIONAL).
            if (iUploadedCnt > 0)
            {

                return Request.CreateResponse(HttpStatusCode.OK, new StringContent(iUploadedCnt + " Files Uploaded Successfully"), Configuration.Formatters.JsonFormatter);
            }
            else
            {
                return new HttpResponseMessage() { Content = new StringContent("Upload Failed") };
            }


        }

        [Route("FileUpload")]
        public  async Task<HttpResponseMessage> Upload()
        {
            try
            {
                var fileuploadPath = HttpContext.Current.Server.MapPath("~/App_Data/Files/");

                var provider = new MultipartFormDataStreamProvider(fileuploadPath);
                var content = new StreamContent(HttpContext.Current.Request.GetBufferlessInputStream(true));
                foreach (var header in Request.Content.Headers)
                {
                    content.Headers.TryAddWithoutValidation(header.Key, header.Value);
                }

                await content.ReadAsMultipartAsync(provider);

                //renaming the random file to Original file name
                string uploadingFileName = provider.FileData.Select(x => x.LocalFileName).FirstOrDefault();
                string originalFileName = String.Concat(fileuploadPath, "\\" + (provider.Contents[0].Headers.ContentDisposition.FileName).Trim(new Char[] { '"' }));

                if (File.Exists(originalFileName))
                {
                    File.Delete(originalFileName);
                }

                File.Move(uploadingFileName, originalFileName);

                return Request.CreateResponse(HttpStatusCode.Created, new StringContent(" Files Uploaded Successfully"), Configuration.Formatters.JsonFormatter);

            }
            catch (Exception)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError,new StringContent("Upload Failed"), Configuration.Formatters.JsonFormatter);
            }

        }
    }

}