using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS.Data
{
    public partial class UserRepo : ICRUDOperations<user>
    {
        public user Add(user entity)
        {
            using (var entities = new TMSDBContext())
            {
                entities.Users.Add(entity);
                int i = entities.SaveChanges();
            }
            return entity;
        }

        public user FindById(long i)
        {
            using (var db = new TMSDBContext())
            {
                return db.Users.FirstOrDefault((a) => a.id == i);
            }
        }

        public IEnumerable<user> GetAll()
        {
            using (var db = new TMSDBContext())
            {
                return db.Users.ToList();
            }
        }
        public user Remove(user entity)
        {
            using (var db = new TMSDBContext())
            {
                db.Users.Remove(entity);
                db.SaveChanges();
            }
            return entity;
        }

        public user Update(user entity)
        {
            using (var db = new TMSDBContext())
            {
                var existingEntity = db.Users.FirstOrDefault((ad) => ad.id == entity.id);
                if (existingEntity != null)
                {
                    existingEntity.email = entity.email;
                    existingEntity.firstname = entity.firstname;
                    existingEntity.lastname = entity.lastname;
                    existingEntity.password = entity.password;
                    existingEntity.modifiedby = entity.modifiedby;
                    existingEntity.modifiedon = entity.modifiedon;
                   db.SaveChanges();
                }
            }
            return entity;
        }
    }
}
