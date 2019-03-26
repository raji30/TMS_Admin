using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMS.Data.Interfaces;

namespace TMS.Data.TableOperations
{
    public class ShippingPortRepository : IBaseRepository<shippingport>
    {
        App_modelEntities entites = new App_modelEntities();
        public Guid Add(shippingport t)
        {
            throw new NotImplementedException();
        }

        public bool Delete(shippingport t)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<shippingport> GetAll()
        {
            return entites.shippingports.ToList();
        }

        public shippingport GetbyField(object t)
        {
            throw new NotImplementedException();
        }

        public shippingport GetbyId(Guid id)
        {
            throw new NotImplementedException();
        }

        public bool Update(shippingport t)
        {
            throw new NotImplementedException();
        }
    }
}
