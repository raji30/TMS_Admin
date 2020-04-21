using Intuit.Ipp.OAuth2PlatformClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace TMS.Api.Controllers
{
    
    public class QBAuthController : Controller
    {
        // GET: QBAuth
        public static string clientid = ConfigurationManager.AppSettings["clientid"];
        public static string clientsecret = ConfigurationManager.AppSettings["clientsecret"];
        public static string redirectUrl = ConfigurationManager.AppSettings["redirectUrl"];
        public static string environment = ConfigurationManager.AppSettings["sandbox"];
        public static OAuth2Client auth2Client = new OAuth2Client(clientid, clientsecret, redirectUrl, environment);
        DiscoveryClient discoveryClient;
        DiscoveryResponse doc;
        public async Task<ActionResult> InitiateAuth()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            //Intialize DiscoverPolicy
            DiscoveryPolicy dpolicy = new DiscoveryPolicy();
            dpolicy.RequireHttps = true;
            dpolicy.ValidateIssuerName = true;


            //Assign the Sandbox Discovery url for the Apps' Dev clientid and clientsecret that you use
            //Or
            //Assign the Production Discovery url for the Apps' Production clientid and clientsecret that you use

            string discoveryUrl = ConfigurationManager.AppSettings["DiscoveryUrl"];

            if (discoveryUrl != null && clientid != null && clientsecret != null)
            {
                discoveryClient = new DiscoveryClient(discoveryUrl);
            }
            else
            {
                Exception ex = new Exception("Discovery Url missing!");
                throw ex;
            }
            doc = await discoveryClient.GetAsync();

            if (doc.StatusCode == HttpStatusCode.OK)
            {
                //Authorize endpoint
               // AppController.authorizeUrl = doc.AuthorizeEndpoint;

                //Token endpoint
                 var tokenEndpoint = doc.TokenEndpoint;
                var tokenClient = new TokenClient(tokenEndpoint);
                
                //Token Revocation enpoint
              //  AppController.revocationEndpoint = doc.RevocationEndpoint;

                //UserInfo endpoint
               // AppController.userinfoEndpoint = doc.UserInfoEndpoint;

                //Issuer endpoint
               // AppController.issuerEndpoint = doc.Issuer;

                //JWKS Keys
              //  AppController.keys = doc.KeySet.Keys;
            }

            //Get mod and exponent value
            //foreach (var key in AppController.keys)
            //{
            //    if (key.N != null)
            //    {
            //        //Mod
            //        AppController.mod = key.N;
            //    }
            //    if (key.E != null)
            //    {
            //        //Exponent
            //        AppController.expo = key.E;
            //    }
            //}
            return null;
        }
    }
}