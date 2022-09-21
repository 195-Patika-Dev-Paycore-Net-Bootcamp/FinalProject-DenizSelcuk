using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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
    public class CustomAuthenticationService : ICustomAuthenticationService
    {
        private readonly ITokenService _tokenService;
        private readonly UserManager<UserApp> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<UserRefreshToken> _userRefreshTokenService;

        public CustomAuthenticationService(ITokenService tokenService, UserManager<UserApp> userManager, IUnitOfWork unitOfWork, IGenericRepository<UserRefreshToken> userRefreshTokenService)
        {
            _tokenService = tokenService;
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _userRefreshTokenService = userRefreshTokenService;
        }

        /// <summary>
        /// Gerekli tüm kontroller yapılır ve geriyw bir token döndürülür
        /// </summary>
        /// <param name="loginDto"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="Exception"></exception>
        public async Task<CustomResponseDto<TokenDto>> CreateToken(LoginDto loginDto)
        {
            if (loginDto == null)
            {
                throw new ArgumentNullException(nameof(loginDto));
            }
            var user = await _userManager.FindByEmailAsync(loginDto.Email);

            if (user == null) return CustomResponseDto<TokenDto>.Fail(400, "Email or password wrong");

            if (!await _userManager.CheckPasswordAsync(user, loginDto.Password))
            {
                return CustomResponseDto<TokenDto>.Fail(400, "Email or password wrong");
            }

            var token = _tokenService.CreateToken(user);

            var userRefreshToken = await _userRefreshTokenService.Where(x => x.UserId == user.Id).SingleOrDefaultAsync();

            if (userRefreshToken == null)
            {
                await _userRefreshTokenService.AddAsync(new UserRefreshToken { UserId = user.Id, Code = token.RefreshToken, Expiration = token.RefreshTokenExpiration });
            }
            else
            {
                userRefreshToken.Code = token.RefreshToken;
                userRefreshToken.Expiration = token.RefreshTokenExpiration;
            }

            await _unitOfWork.CommitAsync();

            return CustomResponseDto<TokenDto>.Success(200, token);
        }
        /// <summary>
        /// Refreshtoken ile token oluşturmak için kullanılır. Refresh tokenın ve refresh tokena sahip kullanıcının varlığını veri tabanında kontrol eder.
        /// </summary>
        /// <param name="refreshToken"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<CustomResponseDto<TokenDto>> CreateTokenByRefreshToken(string refreshToken)
        {
            var existRefreshToken = await _userRefreshTokenService.Where(x => x.Code == refreshToken).SingleOrDefaultAsync();

            if (existRefreshToken == null) return CustomResponseDto<TokenDto>.Fail(404, "Refresh token not found"); 

            var user = await _userManager.FindByIdAsync(existRefreshToken.UserId);

            if (user == null) return CustomResponseDto<TokenDto>.Fail(404, "Refresh token not found");

            var tokenDto = _tokenService.CreateToken(user);

            existRefreshToken.Code = tokenDto.RefreshToken; 
            existRefreshToken.Expiration = tokenDto.RefreshTokenExpiration;

            await _unitOfWork.CommitAsync();

            return CustomResponseDto<TokenDto>.Success(200, tokenDto);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="refreshToken"></param>
        /// <returns></returns>
        public async Task<CustomResponseDto<NoContentDto>> RevokeRefreshToken(string refreshToken)
        {
            var existRefreshToken = await _userRefreshTokenService.Where(x => x.Code == refreshToken).SingleOrDefaultAsync();

            if (existRefreshToken == null) return CustomResponseDto<NoContentDto>.Fail(404, "Refresh token not found");

            _userRefreshTokenService.Remove(existRefreshToken);

            await _unitOfWork.CommitAsync();

            return CustomResponseDto<NoContentDto>.Success(200);
        }
    }
}
