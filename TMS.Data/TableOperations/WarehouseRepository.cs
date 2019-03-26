using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMS.Data.Interfaces;

namespace TMS.Data.TableOperations
{
    public class WarehouseRepository : IBaseRepository<warehouse>
    {
        App_modelEntities entities = new App_modelEntities();
        public Guid Add(warehouse t)
        {
            throw new NotImplementedException();
        }

        public bool Delete(warehouse t)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<warehouse> GetAll()
        {
            return entities.warehouses.ToList();
        }

        public warehouse GetbyField(object t)
        {
            throw new NotImplementedException();
        }

        public warehouse GetbyId(Guid id)
        {
            throw new NotImplementedException();
        }

        public bool Update(warehouse t)
        {
            throw new NotImplementedException();
        }
    }
}
