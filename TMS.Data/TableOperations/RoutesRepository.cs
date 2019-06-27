using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMS.Data.Interfaces;
using TMS.Data;
using TMS.Data.TableOperations;
namespace TMS.Data.TableOperations
{
    public class RoutesRepository : IBaseRepository<tms_routes>
    {
        App_modelEntities entities;
        public RoutesRepository()
        {
            entities = new App_modelEntities();
        }
        public Guid Add(tms_routes t)        {

            var addressnew = entities.tms_routes.Add(t);

            entities.SaveChanges();
            return addressnew.routekey;
        }

        public bool Delete(tms_routes t)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<tms_routes> GetAll()
        {
            return entities.tms_routes.ToList();
        }

        public tms_routes GetbyField(object t)
        {
            return entities.tms_routes.FirstOrDefault();
        }

        public tms_routes GetbyId(Guid id)
        {
            return entities.tms_routes.FirstOrDefault(i => i.routekey == id);
        }

        public bool Update(tms_routes t)
        {
            var addresstoUpdate = GetbyId(t.routekey);
            addresstoUpdate.driverkey = t.driverkey;
            entities.SaveChanges();
            return true;
        }
    }
}
