using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using PayCore.API.Filters;
using PayCore.Service.Validations;

namespace PayCore.API.StartUpExtension
{
    public static class AddControllersWithValidators
    {
        public static void AddControllersWithVAlidator(this IServiceCollection services)
        {
            services.AddControllers(options => options.Filters.Add(new ValidateFilterAttribute())).AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<ProductDtoValidator>());

            services.AddControllers(options => options.Filters.Add(new ValidateFilterAttribute())).AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<LoginDtoValidator>());

            services.AddControllers(options => options.Filters.Add(new ValidateFilterAttribute())).AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<OfferCreateDtoValidator>());

        }
    }
}
