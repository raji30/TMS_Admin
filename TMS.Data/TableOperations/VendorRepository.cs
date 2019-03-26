using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMS.Data.Interfaces;

namespace TMS.Data.TableOperations
{
    public class VendorRepository : IBaseRepository<vendor>
    {
        App_modelEntities entities = new App_modelEntities();
        public Guid Add(vendor t)
        {
            throw new NotImplementedException();
        }

        public bool Delete(vendor t)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<vendor> GetAll()
    {
        return entities.vendors.ToList();
    }

    public vendor GetbyField(object t)
    {
        throw new NotImplementedException();
    }

    public vendor GetbyId(Guid id)
    {
        throw new NotImplementedException();
    }

    public bool Update(vendor t)
    {
        throw new NotImplementedException();
    }
}
}
