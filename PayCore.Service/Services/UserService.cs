using AutoMapper;
using Microsoft.AspNetCore.Identity;
using PayCore.Core.DTOs;
using PayCore.Core.Models;
using PayCore.Core.Repositories;
using PayCore.Core.Services;
using PayCore.Core.UnitOfWork;
using PayCore.Service.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayCore.Service.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<UserApp> _userManager;
        //private readonly IGenericService<Account> _accountService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public UserService(UserManager<UserApp> userManager, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<CustomResponseDto<UserAppDto>> CreateUserAsync(CreateUserDto createUserDto)
        {
            var user = new UserApp() { Email = createUserDto.Email, UserName = createUserDto.UserName };

            
            var result = await _userManager.CreateAsync(user, createUserDto.Password);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(x => x.Description).ToList();

                return CustomResponseDto<UserAppDto>.Fail(400, errors);
            }

            var createdUser = _mapper.Map<UserAppDto>(user);

            return CustomResponseDto<UserAppDto>.Success(200, createdUser);
        }

        public async Task<UserAppDto> GetUserByNameAsync(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);

            return _mapper.Map<UserAppDto>(user);
        }
    }
}
