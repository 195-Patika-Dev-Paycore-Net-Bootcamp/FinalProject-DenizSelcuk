using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PayCore.API.Filters;
using PayCore.Core.DTOs;
using PayCore.Core.Models;
using PayCore.Core.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PayCore.API.Controllers
{
    [Authorize]
    public class ProductsController : CustomBaseController
    {
        private readonly IMapper _mapper;
        private readonly IGenericService<Product> _productService;

        public ProductsController(IMapper mapper, IGenericService<Product> productService)
        {
            _mapper = mapper;
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var products = await _productService.GetAllAsync();
            var productsDtos = _mapper.Map<List<ProductDto>>(products.ToList());
            return CreateActionResult(CustomResponseDto<List<ProductDto>>.Success(200, productsDtos));
        }

        [ServiceFilter(typeof(NotFoundFilter<Product>))] //NotFoundFilter direk bir attribute classını miras almadığı için doğrudan attribute olarak eklemezsin. NEDEN:1 dinamik kullanmak istiyoruz 2 constructor metodunda parametre kullandığımız için bir attirutede kullanamayız. Bu yüzden servisfilter kullanmalısın. ÇÖZÜM:ServiceFilter attributene kendi oluşturduğumuz filter'ı tip olarak verdik.
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _productService.GetByIdAsync(id);
            var productsDto = _mapper.Map<ProductDto>(product);
            return CreateActionResult(CustomResponseDto<ProductDto>.Success(200, productsDto));
        }
        [HttpPost]
        public async Task<IActionResult> Save(ProductDto productDto)
        {
            var product = await _productService.AddAsync(_mapper.Map<Product>(productDto));
            var productsDto = _mapper.Map<ProductDto>(product);
            return CreateActionResult(CustomResponseDto<ProductDto>.Success(201, productsDto));
        }
        [HttpPut]
        public async Task<IActionResult> Update(ProductUpdateDto productDto)
        {
            await _productService.UpdateAsync(_mapper.Map<Product>(productDto));
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            var product = await _productService.GetByIdAsync(id);
            await _productService.RemoveAsync(product);
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }


    }
}
