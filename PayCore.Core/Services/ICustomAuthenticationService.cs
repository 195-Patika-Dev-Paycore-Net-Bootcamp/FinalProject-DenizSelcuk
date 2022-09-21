using PayCore.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayCore.Core.Services
{
    public interface ICustomAuthenticationService
    {
        Task<CustomResponseDto<TokenDto>> CreateToken(LoginDto loginDto);
        Task<CustomResponseDto<TokenDto>> CreateTokenByRefreshToken(string refreshToken);
        Task<CustomResponseDto<NoContentDto>> RevokeRefreshToken(string refreshToken); 
        
    }
}
