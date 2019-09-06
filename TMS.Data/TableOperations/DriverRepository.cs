using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMS.Data.Interfaces;

namespace TMS.Data.TableOperations
{
   public class DriverRepository : IBaseRepository<driver>
    {
        App_modelEntities entities;
        public DriverRepository()
        {
            entities = new App_modelEntities();
        }
        public Guid Add(driver t)
        {
            //throw new NotImplementedException();

            var newdriver = entities.drivers.Add(t);
            entities.SaveChanges();
            return newdriver.driverkey;
        }

        public bool Delete(driver t)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<driver> GetAll()
        {
          return  entities.drivers.ToList();
        }

        public driver GetbyField(object t)
        {
            return entities.drivers.FirstOrDefault(d => d.driverid == (string)t);
        }

        public driver GetbyId(Guid id)
        {
            return entities.drivers.FirstOrDefault(d => d.driverkey == id);
        }

        public bool Update(driver t)
        {
           // throw new NotImplementedException();

            var driv = GetbyId(t.driverkey);
            if (driv != null)
            {
                driv.firstname = t.firstname;
                driv.lastname = t.lastname;
                driv.carrierkey = t.carrierkey;
                driv.drivinglicenseno = t.drivinglicenseno;
                driv.drivinglicenseexpirydate = t.drivinglicenseexpirydate;

                entities.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
