using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using PayCore.Core.Configurations;
using PayCore.Service.Services;
using System;

namespace PayCore.API.StartUpExtension
{
    public static class ExtensionCustomizeAuthentication
    {
        public static void AddJwtBearerAuthentication(this IServiceCollection services, CustomTokenOption tokenOption)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, opts => {

                //var tokenOption = Configuration.GetSection("TokenOption").Get<CustomTokenOption>();
                opts.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                {
                    ValidIssuer = tokenOption.Issuer,
                    ValidAudience = tokenOption.Audience[0],

                    IssuerSigningKey = SignService.GetSymmetricSecurityKey(tokenOption.SecurityKey),
                    ValidateIssuerSigningKey = true,

                    ValidateAudience = true,
                    ValidateIssuer = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });
        }
    }
}
