using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMS.Data.TableOperations;

namespace TMS.BusinessLayer.Security
{
  public  class CompanyAccessFacade
    {
        public List<string> ListEnabledApps(string companyName)
        {
            CompanyRepository crepo = new CompanyRepository();
           var companyobj = crepo.GetbyField(companyName);
            if(companyobj != null)
            {
                CompanyAppRepository appRepo = new CompanyAppRepository();
              var apps=  appRepo.GetAllApps(companyobj.companykey);
              return  apps.Select(a => a.app.appname).ToList();
            }
            return null;
        }
        public bool IsAppEnabled(string CompanyName, string appName)
        {
           var listApps= ListEnabledApps(CompanyName);
            if (listApps.Exists(a => a == appName))
                return true;
            else return false;
        }
       
       
    }
}
