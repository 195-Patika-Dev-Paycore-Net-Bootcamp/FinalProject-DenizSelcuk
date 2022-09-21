using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PayCore.Core.Configurations;
using PayCore.Core.DTOs;
using PayCore.Core.Models;
using PayCore.Core.Services;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PayCore.Service.Services
{
    public class TokenService : ITokenService
    {
        private readonly CustomTokenOption _tokenOption;

        //IOption generic interface ile appsetting json'dan CustomTokenOption instance aldık.
        public TokenService(IOptions<CustomTokenOption> options) 
        {
            _tokenOption = options.Value;
        }

        #region Private Methods
        private string CreateRefreshToken()
        {
            var numberByte = new byte[32];

            using var random = RandomNumberGenerator.Create();

            random.GetBytes(numberByte);

            return Convert.ToBase64String(numberByte);
        }
        /// <summary>
        /// Kullanıcı için claim list oluştrur.
        /// </summary>
        /// <param name="userApp">Kullanıcı</param>
        /// <param name="audiences">Hangi Apilere istek yapacağını tutar</param>
        /// <returns></returns>
        private IEnumerable<Claim> GetClaims(UserApp userApp, List<string> audiences)
        {
            //JWT payload claimlerini key value şeklinde oluşturuyoruz. User için
            var userList = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, userApp.Id), 
                new Claim(JwtRegisteredClaimNames.Email, userApp.Email),
                new Claim(ClaimTypes.Name, userApp.UserName),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
            };
            userList.AddRange(audiences.Select(x => new Claim(JwtRegisteredClaimNames.Aud, x))); 

            return userList;
        }
        #endregion

        #region Public Methods
        public TokenDto CreateToken(UserApp userApp)
        {
            var accessTokenExpiration = DateTime.Now.AddMinutes(_tokenOption.AccessTokenExpiration);
            var refreshTokenExpiration = DateTime.Now.AddMinutes(_tokenOption.RefreshTokenExpiration);
            var securityKey = SignService.GetSymmetricSecurityKey(_tokenOption.SecurityKey);

            SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
                issuer: _tokenOption.Issuer,
                expires: accessTokenExpiration,
                notBefore: DateTime.Now,
                claims: GetClaims(userApp, _tokenOption.Audience),
                signingCredentials: signingCredentials
                );

            var handle = new JwtSecurityTokenHandler();

            var token = handle.WriteToken(jwtSecurityToken);

            var tokenDto = new TokenDto()
            {
                AccessToken = token,
                RefreshToken = CreateRefreshToken(),
                AccessTokenExpiration = accessTokenExpiration,
                RefreshTokenExpiration = refreshTokenExpiration,
            };
            return tokenDto;
        }
        
        #endregion
    }
}
