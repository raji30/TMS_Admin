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
            var newcarrier = entity.carriers.Add(t);
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
            throw new NotImplementedException();
        }

        public bool Update(carrier t)
        {
            throw new NotImplementedException();
        }
    }
}

