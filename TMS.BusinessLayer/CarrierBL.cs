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
    public class CarrierBL
    {
        CarrierRepository repo = new CarrierRepository();

        public CarrierBO GetCarrier(string carrierName)
        {
            var _carrierEntity = repo.GetbyField(carrierName);
            if (_carrierEntity != null)
            {
                CarrierBO bo = new CarrierBO()
                {
                    CarrierName = carrierName,
                    CarrierId = _carrierEntity.carrierid,
                    CarrierKey = _carrierEntity.carrierkey,
                    Address = new DeliveryOrderDL().GetAddress(_carrierEntity.addrkey)                   
    };
                return bo;
            }
            return null;
        }
        public CarrierBO AddBroker(CarrierBO carrier)
        {
            carrier _carrier = new carrier();

            _carrier.carriername = carrier.CarrierName;
            if (carrier.Address != null)
            {
                _carrier.addrkey = carrier.Address.AddrKey;
            }
            _carrier.carrierid = carrier.CarrierId;
            var carrierguid = repo.Add(_carrier);
            carrier.CarrierKey = carrierguid;
            return carrier;

        }
        public List<CarrierBO> GetAll()
        {
            List<CarrierBO> list = new List<CarrierBO>();
            var allCarrier = repo.GetAll();
            foreach (var carrierEntity in allCarrier)
            {
                list.Add(new CarrierBO
                {
                    CarrierName = carrierEntity.carriername,
                    CarrierId = carrierEntity.carrierid,
                    CarrierKey = carrierEntity.carrierkey
                });
            }
            return list;
        }
    }

}
