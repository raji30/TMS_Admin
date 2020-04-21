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
using TMS.BusinessObjects;
using TMS.Data;
using static TMS.BusinessObjects.Enums;

namespace TMS.Api.Controllers
{
    //[EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]
    [AllowAnonymous]
   // [JwtAuthentication]
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

                string DO = provider.FormData[0];
                    string CreatedBy = provider.FormData[1];

                ////renaming the random file to Original file name
                //string uploadingFileName = provider.FileData.Select(x => x.LocalFileName).FirstOrDefault();
                //string originalFileName = String.Concat(fileuploadPath, "\\" + (provider.Contents[0].Headers.ContentDisposition.FileName).Trim(new Char[] { '"' }));

                //if (File.Exists(originalFileName))
                //{
                //    File.Delete(originalFileName);
                //}

                //File.Move(uploadingFileName, originalFileName);

                foreach (var file in provider.FileData)
                {
                    //renaming the random file to Original file name
                    string uploadingFile = file.LocalFileName;
                    string originalFile = String.Concat(fileuploadPath,DO + "\\" + (file.Headers.ContentDisposition.FileName).Trim(new Char[] { '"' }));
                    string strFileName = file.Headers.ContentDisposition.FileName.Replace('"',' ').Trim();
                    if (File.Exists(originalFile))
                    {
                        File.Delete(originalFile);
                    }

                    if(!Directory.Exists(String.Concat(fileuploadPath, DO)))
                    {
                        Directory.CreateDirectory(String.Concat(fileuploadPath, DO));
                    }

                    File.Move(uploadingFile, originalFile);

                    FileInfo finfo = new FileInfo(originalFile);
                    DocumentBO documentBO = new DocumentBO()
                    {
                        Dockey = Guid.NewGuid(),
                        DocType = (DocType)Enum.Parse(typeof(DocType), finfo.Extension.Replace('.', ' ').Trim().ToUpper()),
                        CreatedBy = Guid.Parse(CreatedBy),
                        FileSizeInMB = (int)finfo.Length / 1024,
                        FileType = finfo.Extension.Replace('.', ' ').Trim().ToUpper(), //not sure
                        name = strFileName
                    };
                 
                    OrderHeaderDocumentBO orderBO = new OrderHeaderDocumentBO
                    {
                        Document = documentBO,                        
                        OrderNo = DO
                    };
                                        
                    DocumentDL dl = new DocumentDL();
                    dl.InsertDOHeaderDocument(orderBO);
                }
                return Request.CreateResponse(HttpStatusCode.Created, new StringContent(" Files Uploaded Successfully"), Configuration.Formatters.JsonFormatter);

            }
            catch (Exception ex)
            {
                //return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message, Configuration.Formatters.JsonFormatter);

                return Request.CreateResponse(HttpStatusCode.InternalServerError,new StringContent("Upload Failed"), Configuration.Formatters.JsonFormatter);
            }

        }

        public List<DocumentBO> GetDocNames(string DO)
        {
            DocumentDL dl = new DocumentDL();
            List<DocumentBO> list = dl.GetSupportingDocuments(DO).ToList();
            return list;
           // List<string> FileNames = list.Select(y => y.FileName).ToList();
        }
    }

}