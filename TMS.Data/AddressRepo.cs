using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS.Data
{
   public class AddressRepo : ICRUDOperations<address>
    {
        public address Add(address entity)
        {
            using (var entities = new TMSDBContext())
            {
                entities.Addresses.Add(entity);
               int i= entities.SaveChanges();
            }
            return entity;
        }

        public address FindById(long i)
        {
            using (var db = new TMSDBContext())
            {
              return  db.Addresses.FirstOrDefault((a) => a.id == i);
            }
        }

        public IEnumerable<address> GetAll()
        {
            using (var db = new TMSDBContext())
            {
                return db.Addresses.ToList();
            }
        }
        public address Remove(address entity)
        {
                using (var db = new TMSDBContext())
                {
                    db.Addresses.Remove(entity);
                    db.SaveChanges();
                }
                return entity;
        }

        public address Update(address entity)
        {
            using (var db = new TMSDBContext())
            {
                var existingEntity = db.Addresses.FirstOrDefault((ad) => ad.id == entity.id);
                if (existingEntity != null)
                {
                    existingEntity.addressee = entity.addressee;
                    existingEntity.addressline1 = entity.addressline1;
                    existingEntity.addressline3 = entity.addressline3;
                    existingEntity.addrtype = entity.addrtype;
                    existingEntity.city = entity.city;
                    existingEntity.state = entity.state;
                    existingEntity.country = entity.country;
                    existingEntity.zip = entity.zip;
                    db.SaveChanges();
                }
            }
            return entity;
        }
    }
}
