using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS.Data
{
    public class DriverDetailRepo : ICRUDOperations<driverdetail>
    {
        public driverdetail Add(driverdetail entity)
        {
            using (var entities = new TMSDBContext())
            {
                entities.Driverdetails.Add(entity);
                int i = entities.SaveChanges();
            }
            return entity;
        }

        public driverdetail FindById(long i)
        {
            using (var db = new TMSDBContext())
            {
                return db.Driverdetails.FirstOrDefault((a) => a.id == i);
            }
        }

        public IEnumerable<driverdetail> GetAll()
        {
            using (var db = new TMSDBContext())
            {
                return db.Driverdetails.ToList();
            }
        }

        public driverdetail Remove(driverdetail entity)
        {
            using (var db = new TMSDBContext())
            {
                db.Driverdetails.Remove(entity);
                db.SaveChanges();
            }
            return entity;
        }

        public driverdetail Update(driverdetail entity)
        {
            using (var db = new TMSDBContext())
            {
                var existingEntity = db.Driverdetails.FirstOrDefault((ad) => ad.id == entity.id);
                if (existingEntity != null)
                {
                    existingEntity.drivername = entity.drivername;
                    existingEntity.driverno = entity.driverno;
                    db.SaveChanges();
                }
            }
            return entity;
        }
    }
}
