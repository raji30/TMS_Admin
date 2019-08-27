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
            var newVendor = entities.vendors.Add(t);
            entities.SaveChanges();
            return newVendor.vendkey;
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
        
            return entities.vendors.FirstOrDefault(d => d.vendkey == id);
        }

    public bool Update(vendor t)
    {        
            var vend = GetbyId(t.vendkey);
            if (vend != null)
            {
                vend.vendname = t.vendname;
                vend.vendid = t.vendid;
                vend.addrkey = t.addrkey;
                entities.SaveChanges();
                return true;
            }
            return false;
        }
}
}
