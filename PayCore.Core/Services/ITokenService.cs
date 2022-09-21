using PayCore.Core.DTOs;
using PayCore.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayCore.Core.Services
{
    /// <summary>
    /// Bu servis bir controllarda kullanılmayacak. Sadece token oluşturmak için kullanıulacak.
    /// </summary>
    public interface ITokenService
    {
        /// <summary>
        /// Authentication service için token oluşturur.
        /// </summary>
        /// <param name="userApp"></param>
        /// <returns>TokenDto</returns>
        TokenDto CreateToken(UserApp userApp);
    }
}
