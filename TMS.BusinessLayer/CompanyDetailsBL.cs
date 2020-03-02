using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMS.BusinessObjects;
using TMS.Data.TableOperations;

namespace TMS.BusinessLayer
{
   public class CompanyDetailsBL
    {
        //public Guid AddCompany(CompanyDetailBO companyBO)
        //{
        //    CompanyRepository repo = new CompanyRepository();
        //    AddressRepository addrRepo = new AddressRepository();
        //    Guid AddrKey =Guid.Empty;
        //    if (companyBO.address != null) { 
        //    AddrKey = addrRepo.Add(new Data.address
        //    {
        //        addrkey = Guid.NewGuid(),
        //        addrname = companyBO.CompanyName,
        //        address1 = companyBO.address.Address1,
        //        address2 = companyBO.address.Address2,
        //        city = companyBO.address.City,
        //        state = companyBO.address.State,
        //        zipcode = companyBO.address.Zip,
        //        email = companyBO.address.Email,
        //        fax = companyBO.address.Fax

        //    });
        //    }
        //    var company = new Data.company()
        //    {
        //        companyname = companyBO.CompanyName,
        //        companykey = Guid.NewGuid(),
        //        addrkey = AddrKey

        //    };
        //   return repo.Add(company);

        //}

        //public CompanyDetailBO GetCompany(string name)
        //{
        //    CompanyRepository repo = new CompanyRepository();
        //    var company = repo.GetbyField(name);
        //   if(company != null)
        //    {
        //        return new CompanyDetailBO
        //        {
        //            CompanyKey = company.companykey,
        //            CompanyName = company.companyname,

        //            address = new AddressBO
        //            {
        //                Address1 = company.addressmaster?.address1,
        //                Address2 = company.addressmaster?.address2,
        //                City = company.addressmaster?.city,
        //                State = company.addressmaster?.state,
        //                Zip = company.addressmaster?.zipcode,
        //                Email = company.addressmaster?.email,
        //                Fax = company.addressmaster?.fax
        //            }
        //        };
        //    }
        //    return null;
        //}
    }
}
