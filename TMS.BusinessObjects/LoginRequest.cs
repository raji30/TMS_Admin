﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS.BusinessObjects
{
   public class LoginRequest
    {
       public string UserName { get; set; }
        public string Password { get; set; }
        public string Company { get; set; }
    }
}
