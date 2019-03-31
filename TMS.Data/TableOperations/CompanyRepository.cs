using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMS.Data.Interfaces;

namespace TMS.Data.TableOperations
{
    public class CompanyRepository : IBaseRepository<company>
    {
        App_SecurityEntities entity;
        public CompanyRepository()
        {
            entity = new App_SecurityEntities();
        }
        public Guid Add(company t)
        {
              var c=  entity.companies.Add(t);
            entity.SaveChanges();
               return  c.companykey;
        }

        public bool Delete(company t)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<company> GetAll()
        {
           return entity.companies.ToList<company>();
        }

        public company GetbyField(object companyName)
        {
          return  entity.companies.FirstOrDefault(c => c.companyname == companyName.ToString());
        }

        public company GetbyId(Guid id)
        {
            return entity.companies.FirstOrDefault(c => c.companykey == id);
        }

        public bool Update(company t)
        {
            var company = GetbyId(t.companykey);
            if (company != null)
            {
               // company.addressmaster = t.addressmaster;
                company.companyname = t.companyname;
                company.companyapps = t.companyapps;
                entity.SaveChanges();
                return true;
            }
            else return false;
        }
    }
}
