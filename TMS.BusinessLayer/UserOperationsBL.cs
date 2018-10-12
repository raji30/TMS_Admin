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
   public class UserOperationsBL
    {
        public bool AddUser(UserDetailsBO userDetailsBO)
        {
            UserInfoRepository repo = new UserInfoRepository();
            AddressRepository addressRepo = new AddressRepository();
          var addressKey =  addressRepo.Add(new address
            {
                address1 = userDetailsBO.Address1,
                address2 = userDetailsBO.Address2,
                city = userDetailsBO.City,
                state = userDetailsBO.State,
                zipcode = userDetailsBO.Zip,
                phone = userDetailsBO.Phone,
                fax = userDetailsBO.Fax,
                email = userDetailsBO.Email,
               
            });
            userinfo userinfo = new userinfo
            {
                firstname = userDetailsBO.FirstName,
                lastname = userDetailsBO.LastName,
                password = userDetailsBO.Password,
                addrkey = addressKey,
                createdate = DateTime.Now
              
            };
            var usrKey= repo.Add(userinfo);
            if(usrKey !=null || usrKey !=Guid.Empty)
            {
                return true;
            }
            return false;
        }
        public bool UpdateUser(UserDetailsBO userDetailsBO)
        {
            UserInfoRepository repo = new UserInfoRepository();
           bool result= repo.Update(new userinfo
            {
                firstname = userDetailsBO.FirstName,
                lastname = userDetailsBO.LastName,
                password = userDetailsBO.Password

            });
            return result;

        }

        public UserDetailsBO GetUser(Guid id)
        {
            UserInfoRepository repo = new UserInfoRepository();
          var userinfo=  repo.GetbyId(id);
            UserDetailsBO bo = new UserDetailsBO();
            bo.FirstName = userinfo.firstname;
            bo.LastName = userinfo.lastname;
            bo.Phone = userinfo.address.phone;
            bo.Email = userinfo.address.email;
            bo.Fax = userinfo.address.fax;
            bo.Address1 = userinfo.address.address1;
            bo.Address2 = userinfo.address.address2;
            bo.City = userinfo.address.city;
            bo.State = userinfo.address.state;
            return bo;
        }
    }
}
