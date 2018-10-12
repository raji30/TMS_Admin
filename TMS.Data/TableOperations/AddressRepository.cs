using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMS.Data.Interfaces;

namespace TMS.Data.TableOperations
{
    public class AddressRepository : IBaseRepository<address>
    {
        App_SecurityEntities entities;
        public AddressRepository()
        {
            entities = new App_SecurityEntities();
        }
        public Guid Add(address t)
        {
          var addressnew=  entities.addresses.Add(t);
            entities.SaveChanges();
            return addressnew.addrkey;
        }

        public bool Delete(address t)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<address> GetAll()
        {
            return entities.addresses.ToList();
        }

        public address GetbyField(object t)
        {
            return entities.addresses.FirstOrDefault(i => i.addrname == t.ToString());
        }

        public address GetbyId(Guid id)
        {
            return entities.addresses.FirstOrDefault(i => i.addrkey == id);
        }

        public bool Update(address t)
        {
            var addresstoUpdate = GetbyId(t.addrkey);
            addresstoUpdate.address1 = t.address1;
            addresstoUpdate.address2 = t.address2;
            addresstoUpdate.city = t.city;
            addresstoUpdate.addrname = t.addrname;
            addresstoUpdate.state = t.state;
            addresstoUpdate.email = t.email;
            addresstoUpdate.fax = t.fax;
            addresstoUpdate.phone = t.phone;
            entities.SaveChanges();
            return true;
        }
    }
}
