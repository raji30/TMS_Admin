using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMS.Data.Interfaces;
using TMS.Data;

namespace TMS.BusinessLayer
{
    public class UserInfoRepository : IBaseRepository<userinfo>
    {
        App_SecurityEntities entity;
       public UserInfoRepository()
        {
            entity = new App_SecurityEntities();
        }
        
        public Guid Add(userinfo t)
        {
            
            using (var entity = new App_SecurityEntities())
            {
               var newlyadded =   entity.userinfoes.Add(t);
                entity.SaveChanges();
              return  newlyadded.userkey;
            }
        }

        public bool Delete(userinfo t)
        {
            throw new NotImplementedException();
        }

             
        public bool Update(userinfo t)
        {
            var existing = entity.userinfoes.Where(u => u.userid == t.userid).FirstOrDefault();
            existing.address = t.address;
            existing.firstname = t.firstname;
            existing.lastname = t.lastname;
            return true;
        }

       public IEnumerable<userinfo> GetAll()
        {
            return entity.userinfoes.ToList();
        }

        public userinfo GetbyField(object u_name)
        {
            var userName = u_name.ToString();
           
            var userInfo = entity.userinfoes.Where(u => u.userid == userName).FirstOrDefault();
            return userInfo;
        }

        public userinfo GetbyId(Guid id)
        {
          return  entity.userinfoes.FirstOrDefault(u => u.userkey == id);
        }
    }
}
