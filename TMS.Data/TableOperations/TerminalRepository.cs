using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMS.Data.Interfaces;

namespace TMS.Data.TableOperations
{
    public class TerminalRepository : IBaseRepository<shippingportterminal>
    {
        App_modelEntities entities = new App_modelEntities();
        public Guid Add(shippingportterminal t)
        {
            throw new NotImplementedException();
        }

        public bool Delete(shippingportterminal t)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<shippingportterminal> GetAll()
        {
            return entities.shippingportterminals.ToList();
        }

        public shippingportterminal GetbyField(object t)
        {
            throw new NotImplementedException();
        }

        public shippingportterminal GetbyId(Guid id)
        {
            throw new NotImplementedException();
        }

        public bool Update(shippingportterminal t)
        {
            throw new NotImplementedException();
        }
    }
}
