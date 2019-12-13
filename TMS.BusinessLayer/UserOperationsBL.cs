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
            AddressMasterRepository addressRepo = new AddressMasterRepository();
            List<company> usercompany = new List<company>();
            Guid Addresskey = Guid.Empty;
            if(userDetailsBO.Address != null) {
                Addresskey =  addressRepo.Add(new addressmaster
            {
              addrname = userDetailsBO.FirstName,
              addrkey = Guid.NewGuid(),
              address1 = userDetailsBO.Address.Address1,
                address2 = userDetailsBO.Address.Address2,
                city = userDetailsBO.Address.City,
                state = userDetailsBO.Address.State,
                zipcode = userDetailsBO.Address.Zip,
                phone = userDetailsBO.Address.Phone,
                fax = userDetailsBO.Address.Fax,
                email = userDetailsBO.Address.Email,
               
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
                userid = userDetailsBO.UserId,
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
                password = userDetailsBO.Password,
                //userid= userDetailsBO.UserId
        });
            if (userDetailsBO.Address != null)
            {
                var custaddress = new Data.addressmaster()
                {
                    addrkey = userDetailsBO.Address.AddrKey,
                    address1 = userDetailsBO.Address.Address1,
                    address2 = userDetailsBO.Address.Address2,
                    city = userDetailsBO.Address.City,
                    state = userDetailsBO.Address.State,
                    country = userDetailsBO.Address.Country,
                    zipcode = userDetailsBO.Address.Zip,
                    email = userDetailsBO.Address.Email,
                    fax = userDetailsBO.Address.Fax,
                    phone = userDetailsBO.Address.Phone,
                    website = userDetailsBO.Address.Website,
                    addrname = userDetailsBO.UserId
                };
                bool updated = new AddressMasterRepository().Update(custaddress);
            }
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
            if (userinfo.addressmaster != null)
            {
                bo.Address = new AddressBO
                {
                    AddrKey = userinfo.addressmaster.addrkey,                   
                    Address1 = userinfo.addressmaster.address1,
                    Address2 = userinfo.addressmaster.address2,
                    City = userinfo.addressmaster.city,
                    State = userinfo.addressmaster.state,
                    Zip = userinfo.addressmaster.zipcode,
                    Country = userinfo.addressmaster.country,
                    Website = userinfo.addressmaster.website,
                     Phone = userinfo.addressmaster.phone,
                    Email = userinfo.addressmaster.email,
                    Fax = userinfo.addressmaster.fax,
                };
            }
            return bo;
        }
            else return null;
        }

        public UserDetailsBO GetUserById(string userKey)
        {
            UserInfoRepository repo = new UserInfoRepository();
            var userinfo = repo.GetbyId(Guid.Parse(userKey));
            if (userinfo != null)
            {
                UserDetailsBO bo = new UserDetailsBO();
                bo.FirstName = userinfo.firstname;
                bo.LastName = userinfo.lastname;
                bo.UserKey = userinfo.userkey;
                bo.UserId = userinfo.userid;
                bo.Password = userinfo.password;
                if (userinfo.addressmaster != null)
                {
                    bo.Address = new AddressBO
                    {
                        AddrKey = userinfo.addressmaster.addrkey,
                        Phone = userinfo.addressmaster.phone,
                        Email = userinfo.addressmaster.email,
                        Fax = userinfo.addressmaster.fax,
                        Address1 = userinfo.addressmaster.address1,
                        Address2 = userinfo.addressmaster.address2,
                        City = userinfo.addressmaster.city,
                        State = userinfo.addressmaster.state,
                        Zip = userinfo.addressmaster.zipcode,
                        Country = userinfo.addressmaster.country,
                        Website = userinfo.addressmaster.website
                    };
                }
                return bo;
            }
            else return null;
        }

        public List<UserDetailsBO> GetAllUser()
        {
            UserInfoRepository repo = new UserInfoRepository();
            List<UserDetailsBO> list = new List<UserDetailsBO>();

            var userinfo = repo.GetAll();
            if (userinfo != null)
            {
                foreach (var user in userinfo)
                {
                    UserDetailsBO bo = new UserDetailsBO();
                    bo.FirstName = user.firstname;
                    bo.LastName = user.lastname;
                    bo.UserKey = user.userkey;
                    bo.UserId = user.userid;
                    bo.Password = user.password;
                    
                    if (user.addressmaster != null)
                    {
                        bo.Address = new AddressBO
                        {
                            Phone = user.addressmaster.phone,
                            Email = user.addressmaster.email,
                            Fax = user.addressmaster.fax,
                            Address1 = user.addressmaster.address1,
                            Address2 = user.addressmaster.address2,
                            City = user.addressmaster.city,
                            State = user.addressmaster.state,
                            Zip = user.addressmaster.zipcode,
                            Country =user.addressmaster.country,
                            Website = user.addressmaster.website
                        };
                    }
                    list.Add(bo);
                }
                //    UserDetailsBO bo = new UserDetailsBO();
                //bo.FirstName = userinfo.firstname;
                //bo.LastName = userinfo.lastname;
                //bo.UserKey = userinfo.userkey;
                //bo.UserId = userinfo.userid;
                //if (userinfo.addressmaster != null)
                //{
                //    bo.address = new AddressBO
                //    {
                //        Phone = userinfo.addressmaster.phone,
                //        Email = userinfo.addressmaster.email,
                //        Fax = userinfo.addressmaster.fax,
                //        Address1 = userinfo.addressmaster.address1,
                //        Address2 = userinfo.addressmaster.address2,
                //        City = userinfo.addressmaster.city,
                //        State = userinfo.addressmaster.state
                //    };
                //}
                return list;
            }
            else return null;
        }        
    }
}
