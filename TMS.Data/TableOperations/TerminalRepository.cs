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
            var newWarehouse = entities.shippingportterminals.Add(t);
            entities.SaveChanges();
            return newWarehouse.terminalkey;
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
            return entities.shippingportterminals.FirstOrDefault(d => d.terminalkey == id);
        }

        public bool Update(shippingportterminal t)
        {
            var terminal = GetbyId(t.terminalkey);
            if (terminal != null)
            {
                terminal.shippingport = t.shippingport;
                terminal.portkey = t.portkey;
                terminal.addrkey = t.addrkey;

                entities.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
