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
  public class InvoiceBL
    {
        InvoiceDL repo = new InvoiceDL();

        public DeliveryOrderBO getOrderDatabyKey(string orderkey)
        {
            DeliveryOrderBO bo = new DeliveryOrderBO();

            var data = repo.getOrderDatabyKey(orderkey);
            return data;
        }
               
        public List<RateBO> getorderratesbykey(string orderkey)
        {
           var data = repo.getorderratesbykey(orderkey);

            if (data != null)
            {

                return data;
            }
            else
            {
                return null;
            }            
        }

    }
}
