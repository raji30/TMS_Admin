using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMS.Data.Interfaces;

namespace TMS.Data.TableOperations
{
    class UserActivityRepository : IBaseRepository<useractivity>
    {
        App_SecurityEntities entity;
        public UserActivityRepository()
        {
            entity = new App_SecurityEntities();
        }
        public Guid Add(useractivity t)
        {
           var a= entity.useractivities.Add(t);
            entity.SaveChanges();
            return a.activitykey;
        }

        public bool Delete(useractivity t)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<useractivity> GetAll()
        {
            throw new NotImplementedException();
        }
        public IEnumerable<useractivity> GetAll(Guid userId)
        {
            return entity.useractivities.Where(i => i.userinfo.userkey == userId);
        }

        public useractivity GetbyField(object t) //recent activity
        {
              return entity.useractivities.
                Where(i => i.userinfo.userkey == ((userinfo)t).userkey).
                OrderByDescending(o=>o.activitytimestamp).FirstOrDefault();
        }

        public useractivity GetbyId(Guid id)
        {
            return entity.useractivities.FirstOrDefault(i => i.activitykey == id);
        }

        public bool Update(useractivity t)
        {
            throw new NotImplementedException();
        }
    }
}
