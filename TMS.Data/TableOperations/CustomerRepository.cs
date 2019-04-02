using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMS.Data.Interfaces;

namespace TMS.Data.TableOperations
{
    public class CustomerRepository : IBaseRepository<customer>
    {
        private App_modelEntities entity;
        public CustomerRepository()
        {
            entity = new App_modelEntities();
        }
        public Guid Add(customer t)
        {
            var newcustomr= entity.customers.Add(t);
            entity.SaveChanges();
            return newcustomr.custkey;
        }

        public bool Delete(customer t)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<customer> GetAll()
        {
           return entity.customers.ToList();
        }

        public customer GetbyField(object t)
        {
            string Name = t.ToString();
            return entity.customers.FirstOrDefault(c => c.custname == Name);
        }

        public customer GetbyId(Guid id)
        {
            return entity.customers.FirstOrDefault(c => c.custkey == id);
        }

        public bool Update(customer t)
        {
            var customer = GetbyId(t.custkey);
            if (customer != null) { 
            customer.custname = t.custname;
            customer.customergroup = t.customergroup;
            customer.status = t.status;
            customer.creditlimit = t.creditlimit;
            customer.creditstatus = t.creditstatus;
            entity.SaveChanges();
            return true;
            }
            return false;
        }

    }
}
