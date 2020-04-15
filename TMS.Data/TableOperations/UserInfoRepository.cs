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
            try
            {

                using (var entity = new App_SecurityEntities())
                {
                    t.userkey = Guid.NewGuid();
                    var newlyadded = entity.userinfoes.Add(t);

                    entity.SaveChanges();
                    return newlyadded.userkey;
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public bool Delete(userinfo t)
        {
            throw new NotImplementedException();
        }

             
        public bool Update(userinfo t)
        {
            var existing = entity.userinfoes.Where(u => u.userid == t.userid).FirstOrDefault();

            if(existing!= null)
            {
                existing.firstname = t.firstname;
                existing.lastname = t.lastname;
                existing.userid = t.userid;

                if(t.addressmaster!=null)
                {
                    existing.addressmaster = t.addressmaster;
                }
                entity.SaveChanges();
                return true;
            }
          
            return false;
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
