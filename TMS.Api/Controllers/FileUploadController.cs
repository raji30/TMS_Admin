using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using TMS.BusinessLayer;
using TMS.BusinessObjects;
using TMS.Data;
using static TMS.BusinessObjects.Enums;

namespace TMS.Api.Controllers
{
    [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]
    public class FileUploadController : ApiController
    {
        [HttpPost]
        [Route("FileUpload")]
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

                            File.Move(finfo.FullName, Path.Combine(root, DO, 
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
        [HttpGet]
        public HttpResponseMessage GetDocNames(string DO)
        {
            DocumentDL dl = new DocumentDL();
            List<DocumentBO> list = dl.GetSupportingDocumentsForDO(Guid.Parse(DO)).ToList();
            List<string> FileNames = list.Select(y => y.FileName).ToList();
            return Request.CreateResponse(HttpStatusCode.OK, FileNames, Configuration.Formatters.JsonFormatter);
        }
        [HttpPost]
        public HttpResponseMessage GetDocuments(string DO,string FileName)
        {
            DocumentDL dl = new DocumentDL();
            string root = HttpContext.Current.Server.MapPath($"~/App_Data/Files/{DO}/");
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);

            //Set the File Path.
            //Check whether File exists.
            if (!File.Exists(root))
            {
                //Throw 404 (Not Found) exception if File not found.
                response.StatusCode = HttpStatusCode.NotFound;
                response.ReasonPhrase = string.Format("File not found: {0} .", FileName);
                throw new HttpResponseException(response);
            }

            //Read the File into a Byte Array.
            byte[] bytes = File.ReadAllBytes(root);

            //Set the Response Content.
            response.Content = new ByteArrayContent(bytes);

            //Set the Response Content Length.
            response.Content.Headers.ContentLength = bytes.LongLength;

            //Set the Content Disposition Header Value and FileName.
            response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
            response.Content.Headers.ContentDisposition.FileName = FileName;

            //Set the File Content Type.
            response.Content.Headers.ContentType = new MediaTypeHeaderValue(MimeMapping.GetMimeMapping(FileName));
            return response;
        }

    }
}
