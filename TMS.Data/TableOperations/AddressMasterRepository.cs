using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMS.Data.Interfaces;

namespace TMS.Data.TableOperations
{
    public class AddressMasterRepository : IBaseRepository<addressmaster>
    {
        App_SecurityEntities entities;
        public AddressMasterRepository()
        {
            entities = new App_SecurityEntities();
        }
        public Guid Add(addressmaster t)
        {
            
            var addressnew = entities.addressmasters.Add(t);
            try
            {               

                entities.SaveChanges();
            }
            catch(Exception exception)
            {
                throw exception;
            }
            return addressnew.addrkey;
        }

        public bool Delete(addressmaster t)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<addressmaster> GetAll()
        {
            throw new NotImplementedException();
        }

        public addressmaster GetbyField(object t)
        {
            return entities.addressmasters.FirstOrDefault(i => i.addrname == t.ToString());
        }

        public addressmaster GetbyId(Guid id)
        {
            return entities.addressmasters.FirstOrDefault(i => i.addrkey == id);
        }

        public bool Update(addressmaster t)
        {
            var addresstoUpdate = GetbyId(t.addrkey);
            addresstoUpdate.address1 = t.address1;
            addresstoUpdate.address2 = t.address2;
            addresstoUpdate.city = t.city;
            addresstoUpdate.country = t.country;
            addresstoUpdate.addrname = t.addrname;
            addresstoUpdate.zipcode = t.zipcode;
            addresstoUpdate.website = t.website;
            addresstoUpdate.state = t.state;
            addresstoUpdate.email = t.email;
            addresstoUpdate.fax = t.fax;
            addresstoUpdate.phone = t.phone;
            entities.SaveChanges();
            return true;
        }
    }
}
