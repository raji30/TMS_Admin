using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMS.Data.Interfaces;

namespace TMS.Data.TableOperations
{
    public class CarrierRepository : IBaseRepository<carrier>
    {
        App_modelEntities entity = new App_modelEntities();

        public Guid Add(carrier t)
        {
            t.carrierkey = Guid.NewGuid();
                      var newcarrier = entity.carriers.Add(t);
            entity.SaveChanges();
            return newcarrier.carrierkey;
        }

        public bool Delete(carrier t)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<carrier> GetAll()
        {
            return entity.carriers.ToList();
        }

        public carrier GetbyField(object t)
        {
            return entity.carriers.FirstOrDefault(b => b.carriername == t.ToString());
        }

        public carrier GetbyId(Guid id)
        {
            return entity.carriers.FirstOrDefault(d => d.carrierkey == id);
        }

        public bool Update(carrier t)
        {
            var car = GetbyId(t.carrierkey);
            if (car != null)
            {                
                car.carrierid = t.carrierid;
                car.carriername = t.carriername;
                car.addrkey = t.addrkey;

                entity.SaveChanges();
                return true;
            }
            return false;
        }
    }
}

