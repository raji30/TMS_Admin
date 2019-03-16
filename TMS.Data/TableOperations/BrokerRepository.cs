﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMS.Data.Interfaces;

namespace TMS.Data.TableOperations
{
    public class BrokerRepository : IBaseRepository<broker>
    {
        App_modelEntities entity = new App_modelEntities();
        public Guid Add(broker t)
        {
            var newbroker = entity.brokers.Add(t);
            return newbroker.brokerkey;
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
            return entity.brokers.FirstOrDefault(b => b.brokername == t.ToString());
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
