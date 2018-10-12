using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMS.Data.Interfaces;

namespace TMS.Data.TableOperations
{
    public class AppRepository : IBaseRepository<app>
    {
        App_SecurityEntities entity;
        public AppRepository()
        {
            entity = new App_SecurityEntities();
        }
        public Guid Add(app t)
        {
           var newApp= entity.apps.Add(t);
            entity.SaveChanges();
            return newApp.appkey;
        }

        public bool Delete(app t)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<app> GetAll()
        {
            return entity.apps.ToList<app>();
        }

        public app GetbyField(object t)
        {
            throw new NotImplementedException();
        }

        public app GetbyId(Guid id)
        {
            return entity.apps.FirstOrDefault(t => t.appkey == id);
        }

        public bool Update(app t)
        {
            var toUpdate = GetbyId(t.appkey);
            toUpdate.appname = t.appname;
            toUpdate.version = toUpdate.version;
            entity.SaveChanges();
            return true;
        }
    }
}
