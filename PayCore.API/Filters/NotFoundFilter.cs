using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PayCore.Core.DTOs;
using PayCore.Core.Models;
using PayCore.Core.Services;
using System.Linq;
using System.Threading.Tasks;

namespace PayCore.API.Filters
{
    public class NotFoundFilter<T> : IAsyncActionFilter where T : BaseModel
    {
        public readonly IGenericService<T> _service;
        public NotFoundFilter(IGenericService<T> service)
        {
            _service = service;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var idValue = context.ActionArguments.Values.FirstOrDefault();
            if (idValue == null)
            {
                await next.Invoke();
                return;
            }

            var id = (int)idValue;
            var anyEntity = await _service.AnyAsync(x => x.Id == id); //id'ye sahip entity'nin olup olmadığını kontrol ettik. id ile arama yapabilmek için IAsyncActionFilter'i kalıtım alırken BaseEntity'i gösterdik.

            if (anyEntity) //Eğer entity varsa controller metodu çalıştırılır
            {
                await next.Invoke();
                return;
            }
            context.Result = new NotFoundObjectResult(CustomResponseDto<NoContentDto>.Fail(404, $"{typeof(T).Name} ({id}) not found"));//Entity yoksa CustomResponsoDto modeli NoContentDto olarak dönülür.

        }
    }
}
