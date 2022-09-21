using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;
using Paycore.Repository;
using Paycore.Repository.Repositories;
using PayCore.Core.Repositories;
using PayCore.Core.Services;
using PayCore.Core.UnitOfWork;
using PayCore.Service.Mapping;
using PayCore.Service.Services;


namespace PayCore.API.StartUpExtension
{
    public static class ExtensionService
    {
        public static void AddService(this IServiceCollection services)
        {
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MapProfile());
            });
            services.AddSingleton(mapperConfig.CreateMapper());

        }



    }
}
