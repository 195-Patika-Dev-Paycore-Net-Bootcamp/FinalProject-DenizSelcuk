using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PayCore.Core.DTOs;
using PayCore.Core.Services;
using System.Threading.Tasks;

namespace PayCore.API.Controllers
{
    public class RegisterController : CustomBaseController
    {
        private readonly IUserService _userService;

        public RegisterController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserDto createUserDto)
        {
            var response = await _userService.CreateUserAsync(createUserDto);
            return CreateActionResult(response);
        }


    }
}
