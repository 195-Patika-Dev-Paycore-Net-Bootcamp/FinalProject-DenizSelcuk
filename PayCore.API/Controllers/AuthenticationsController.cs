
using Microsoft.AspNetCore.Mvc;
using PayCore.Core.DTOs;
using PayCore.Core.Services;
using System.Threading.Tasks;

namespace PayCore.API.Controllers
{
    public class AuthenticationsController :CustomBaseController
    {
        private readonly ICustomAuthenticationService _authenticationService;

        public AuthenticationsController(ICustomAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateToken(LoginDto loginDto)
        {
            var result = await _authenticationService.CreateToken(loginDto);

            return CreateActionResult(result);
        }

    }
}
