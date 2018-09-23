using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS.Data
{
    public class DeliveryOrderRepo : ICRUDOperations<deliveryorder>
    {
        public deliveryorder Add(deliveryorder entity)
        {
            using (var db = new TMSDBContext())
            {
                var addedvalue = db.Deliveryorders.Add(entity);
                return addedvalue;
            }
        }

        public deliveryorder FindById(long i)
        {
            using (var db = new TMSDBContext())
            {
                var retrievedvalue = db.Deliveryorders.FirstOrDefault((d) => d.id == i);
                if (retrievedvalue != null)
                    return retrievedvalue;
            }
            return null;
        }

        public IEnumerable<deliveryorder> GetAll()
        {
            using (var db = new TMSDBContext())
            {
                return db.Deliveryorders.ToList();
            }
        }
        public deliveryorder Remove(deliveryorder entity)
        {
            using (var db = new TMSDBContext())
            {
                var retrievedvalue = db.Deliveryorders.FirstOrDefault((d) => d.id == entity.id);
                if (retrievedvalue != null)
                    db.Deliveryorders.Remove(retrievedvalue);

            }
            return entity;
        }

        public deliveryorder Update(deliveryorder entity)
        {
            using (var db = new TMSDBContext())
            {
                var existingEntity = db.Deliveryorders.FirstOrDefault((ad) => ad.id == entity.id);
                if (existingEntity != null)
                {
                    existingEntity.billtoaddrid = entity.billtoaddrid;
                    existingEntity.bookingno = entity.bookingno;
                    existingEntity.boxes = entity.boxes;
                    existingEntity.brokername = entity.brokername;
                    existingEntity.brokerrefno = entity.brokerrefno;
                    existingEntity.consigneeaddrid = entity.consigneeaddrid;
                    existingEntity.customernotes = entity.customernotes;
                    existingEntity.cutoffdate = entity.cutoffdate;
                    existingEntity.fbrno = entity.fbrno;
                    existingEntity.freightcharges = entity.freightcharges;
                    existingEntity.portoforigin = entity.portoforigin;
                    existingEntity.shipmentweight = entity.shipmentweight;
                    existingEntity.status = entity.status;
                    existingEntity.vesselname = entity.vesselname;
                    existingEntity.isediorder = entity.isediorder;
                    existingEntity.modifiedby = entity.modifiedby;
                    existingEntity.modifiedon = entity.modifiedon;
                    db.SaveChanges();
                }
            }
            return entity;
        }
    }
}
