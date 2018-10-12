using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMS.Data.Interfaces;

namespace TMS.Data.TableOperations
{
    public class CompanyAppRepository : IBaseRepository<companyapp>
    {
        
        public Guid Add(companyapp t)
        {
            throw new NotImplementedException();
        }

        public bool Delete(companyapp t)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<companyapp> GetAll()
        {
            throw new NotImplementedException();
        }

        public companyapp GetbyField(object t)
        {
            throw new NotImplementedException();
        }

        public List<companyapp> GetAllApps (Guid companyKey)
        {
            throw new NotImplementedException();
        }

        public bool Update(companyapp t)
        {
            throw new NotImplementedException();
        }

        public companyapp GetbyId(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
