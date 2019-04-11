using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMS.BusinessObjects;
using TMS.Data;
using TMS.Data.TableOperations;

namespace TMS.BusinessLayer
{
   public class BrokerBL
    {
        BrokerRepository repo = new BrokerRepository();
        public BrokerBO GetBroker(string brokerName)
        {
            var _brokerEntity = repo.GetbyField(brokerName);
            if(_brokerEntity!=null) { 
            BrokerBO bo = new BrokerBO()
            {
                BrokerName = brokerName,
                BrokerId = _brokerEntity.brokerid,
                BrokerKey = _brokerEntity.brokerkey,
                Address = new DeliveryOrderDL().GetAddress(_brokerEntity.addrkey)
            };
            return bo;
            }
            return null;
        }
        public BrokerBO AddBroker(BrokerBO broker)
        {
            broker _broker = new broker();

            _broker.brokername = broker.BrokerName;
            if (broker.Address != null)
            {
                _broker.addrkey = broker.Address.AddrKey;
            }
            _broker.brokerid = broker.BrokerId;
            var brokerguid = repo.Add(_broker);
            broker.BrokerKey = brokerguid;
            return broker;

        }
        public IList<BrokerBO> GetAll()
        {
            List<BrokerBO> list = new List<BrokerBO>();
            var allbrokers = repo.GetAll();
            foreach(var brokerEntity in allbrokers)
            {
                list.Add(new BrokerBO
                {
                    BrokerName = brokerEntity.brokername,
                    BrokerId = brokerEntity.brokerid,
                    BrokerKey = brokerEntity.brokerkey,
                    Status = brokerEntity.status.Value
                });
            }
            return list;
        }
    }

}
