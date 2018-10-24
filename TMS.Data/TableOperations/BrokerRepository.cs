using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMS.Data.Interfaces;

namespace TMS.Data.TableOperations
{
    public class BrokerRepository : IBaseRepository<broker>
    {
        public Guid Add(broker t)
        {
            throw new NotImplementedException();
        }

        public bool Delete(broker t)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<broker> GetAll()
        {
            throw new NotImplementedException();
        }

        public broker GetbyField(object t)
        {
            throw new NotImplementedException();
        }

        public broker GetbyId(Guid id)
        {
            throw new NotImplementedException();
        }

        public bool Update(broker t)
        {
            throw new NotImplementedException();
        }
    }
}
