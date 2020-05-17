using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMS.BusinessLayer.Common;
using TMS.BusinessObjects;
using TMS.Data;
using TMS.Data.TableOperations;
using static TMS.BusinessObjects.Enums;

namespace TMS.BusinessLayer
{
   public class AddressBL
    {
        App_modelEntities entities = new App_modelEntities();
        public IEnumerable<AddressBO> GetAddressesByType(int addressType)
        {
            DeliveryOrderDL dl = new DeliveryOrderDL();

            var list = EnumExtensions.GetEnumValues<AddressType>();
            List<AddressBO> returnList = new List<AddressBO>();

            var specifiedType = list.FirstOrDefault(a => a.Value == addressType);
            AddressType addType;
            Enum.TryParse(specifiedType.Name, out addType);

            switch (addType)
            {
                case AddressType.Customer:
                    {
                        var CustomerRepository = new CustomerRepository();
                        var allcustomers = CustomerRepository.GetAll();

                        foreach (var customer in allcustomers)
                        {
                            var addressBO = dl.GetAddress(customer.addrkey);
                            addressBO.AddrKey = customer.addrkey;
                            addressBO.Name = customer.custname;
                            returnList.Add(addressBO);
                        }
                    }
                    break;

                case AddressType.Company:
                    {
                        var DL = new CompanyDL();
                        var companies = DL.GetCompanies();

                        foreach (var company in companies)
                        {                          
                            var addressBO = DL.GetAddress(company.addrkey);
                            addressBO.AddrKey = company.addrkey;
                            addressBO.Name = company.compname;
                            returnList.Add(addressBO);
                        }
                    }
                    break;

                case AddressType.ShippingPort:
                    {
                        var shippingRepo = new ShippingPortRepository();
                        var allPorts = shippingRepo.GetAll();
                        foreach (var port in allPorts)
                        {
                            var addressBO = dl.GetAddress(port.addrkey);
                            addressBO.AddrKey = port.addrkey;
                            addressBO.Name = port.shippingportid;
                            returnList.Add(addressBO);
                        }
                    }
                    break;
                case AddressType.Terminal:
                    {
                        var shippingRepo = new TerminalRepository();
                        var allPorts = shippingRepo.GetAll();
                        foreach (var port in allPorts)
                        {
                            var addressBO = dl.GetAddress(port.addrkey);
                            addressBO.AddrKey = port.portkey;
                            returnList.Add(addressBO);
                        }
                    }
                    break;
                case AddressType.Warehouse:
                    {
                        var shippingRepo = new WarehouseRepository();
                        var allPorts = shippingRepo.GetAll();
                        foreach (var port in allPorts)
                        {
                            var addressBO = dl.GetAddress(port.addrkey);
                            addressBO.AddrKey = port.warehousekey;
                            addressBO.Name = port.warehouseid;
                            returnList.Add(addressBO);
                        }
                    }
                    break;
                case AddressType.Vendor:
                    {
                        var repo = new VendorRepository();
                        var allPorts = repo.GetAll();
                        foreach (var port in allPorts)
                        {
                            var addressBO = dl.GetAddress(port.addrkey);
                            addressBO.AddrKey = port.vendkey;
                            addressBO.Name = port.vendname;
                            returnList.Add(addressBO);
                        }
                    }
                    break;
            }
            return returnList;
        }
    }
}
