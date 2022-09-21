using PayCore.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayCore.Core.Services
{
    /// <summary>
    /// Bu servis sadece user oluşturmak için kullanılır. Account oluşturulurken account user oluşturmak ve kaydetmek için kullanılır.
    /// </summary>
    public interface IUserService
    {
        Task<CustomResponseDto<UserAppDto>> CreateUserAsync(CreateUserDto createUserDto);
        Task<UserAppDto> GetUserByNameAsync(string userName);

    }
}
