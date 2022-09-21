using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayCore.Service.Services
{
    public static class SignService
    {
        /// <summary>
        /// Token oluştururken customtokenoption security key den token signature oluşturur.
        /// </summary>
        /// <param name="securityKey"></param>
        /// <returns></returns>
        public static SecurityKey GetSymmetricSecurityKey(string securityKey)
        {
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));
        }
    }
}
