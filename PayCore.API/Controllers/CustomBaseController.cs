using Microsoft.AspNetCore.Mvc;
using PayCore.Core.DTOs;

namespace PayCore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomBaseController : ControllerBase
    {
        public IActionResult CreateActionResult<TDto> (CustomResponseDto<TDto> response)
        {
            if (response.StatusCode == 204)
            {
                return new ObjectResult(null)
                {
                    StatusCode = response.StatusCode
                };
            }
            return new ObjectResult(response)
            {
                StatusCode=response.StatusCode
            };
        }
    }
}
