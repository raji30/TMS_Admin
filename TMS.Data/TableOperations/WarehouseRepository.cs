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
            var newWarehouse = entities.warehouses.Add(t);
            entities.SaveChanges();
            return newWarehouse.warehousekey;
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
            return entities.warehouses.FirstOrDefault(d => d.warehousekey == id);
        }

        public bool Update(warehouse t)
        {
            var ware = GetbyId(t.warehousekey);
            if (ware != null)
            {
                ware.warehouseid = t.warehouseid;
                ware.addrkey = t.addrkey;

                entities.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
