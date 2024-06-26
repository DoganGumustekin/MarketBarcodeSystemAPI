﻿using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Security.Encryption
{
    public class SecurityKeyHelper
    {
        public static SecurityKey CreateSecurityKey(string securityKey) //SecurityKey api deki appsettings ten geliyor 
        {                                                               //securityKey ise o SecurityKey in karşılığı.
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey)); //byte değerini alıp onu bir (securitykeyi) simetrik anahtar haline getir. 
        }
    }
}
