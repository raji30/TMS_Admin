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
            List<company> usercompany = new List<company>();
            Guid Addresskey = Guid.Empty;
            if(userDetailsBO.address != null) {
                Addresskey =  addressRepo.Add(new address
            {
              addrname = userDetailsBO.FirstName,
              addrkey = Guid.NewGuid(),
              address1 = userDetailsBO.address.Address1,
                address2 = userDetailsBO.address.Address2,
                city = userDetailsBO.address.City,
                state = userDetailsBO.address.State,
                zipcode = userDetailsBO.address.Zip,
                phone = userDetailsBO.address.Phone,
                fax = userDetailsBO.address.Fax,
                email = userDetailsBO.address.Email,
               
            });
            }
            if (userDetailsBO.CompanyKey != null)
            {
                CompanyRepository companyRepo = new CompanyRepository();
                var result =  companyRepo.GetbyId(userDetailsBO.CompanyKey.Value);
                if(result != null)
                usercompany.Add(result);
            }
            userinfo userinfo = new userinfo
            {
                firstname = userDetailsBO.FirstName,
                lastname = userDetailsBO.LastName,
                password = userDetailsBO.Password,
                addrkey = Addresskey,
                createdate = DateTime.Now,
                status = 1
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

        public UserDetailsBO GetUser(string userName)
        {
            UserInfoRepository repo = new UserInfoRepository();
          var userinfo=  repo.GetbyField(userName);
            if(userinfo!=null) { 
            UserDetailsBO bo = new UserDetailsBO();
            bo.FirstName = userinfo.firstname;
            bo.LastName = userinfo.lastname;
            bo.UserKey = userinfo.userkey;
                bo.UserId = userinfo.userid;
            if (userinfo.address != null)
            {
                bo.address = new AddressBO
                {
                    Phone = userinfo.address.phone,
                    Email = userinfo.address.email,
                    Fax = userinfo.address.fax,
                    Address1 = userinfo.address.address1,
                    Address2 = userinfo.address.address2,
                    City = userinfo.address.city,
                    State = userinfo.address.state
                };
            }
            return bo;
        }
            else return null;
        }
        
    }
}
