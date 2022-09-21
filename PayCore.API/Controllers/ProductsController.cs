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
        private readonly IProductService _productService;
        private readonly IUserService _userService;

        public ProductsController(IUserService userService, IMapper mapper, IProductService productService)
        {
            _mapper = mapper;
            _productService = productService;
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _productService.GetAllAsync();
            var productsDtos = _mapper.Map<List<ProductDto>>(products.ToList());
            return CreateActionResult(CustomResponseDto<List<ProductDto>>.Success(200, productsDtos));
        }

        [HttpGet("MyProducts")]
        public async Task<IActionResult> GetProductsOfUser()
        {
            var userDto = await _userService.GetUserByNameAsync(HttpContext.User.Identity.Name);

            var productsDto = await _productService.GetProductsForUserIdAsync(userDto.Id);

            return CreateActionResult(CustomResponseDto<List<ProductDto>>.Success(200, productsDto.ToList()));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _productService.GetByIdAsync(id);
            var productsDto = _mapper.Map<ProductDto>(product);
            return CreateActionResult(CustomResponseDto<ProductDto>.Success(200, productsDto));
        }
        [HttpPost]
        public async Task<IActionResult> AddProductToListOfUser(ProductCreateDto productCreateDto)
        {
            var userDto = await _userService.GetUserByNameAsync(HttpContext.User.Identity.Name);

            var productDto = _mapper.Map<ProductDto>(productCreateDto);

            productDto.UserAppId = userDto.Id;

            var productsDto = await _productService.AddProduct(productDto);

            return CreateActionResult(CustomResponseDto<ProductDto>.Success(200, productsDto));
        }
        [HttpPut]
        public async Task<IActionResult> UpdateProductOfUser(ProductUpdateDto productUpdateDto)
        {
            var userDto = await _userService.GetUserByNameAsync(HttpContext.User.Identity.Name);
            await _productService.UpdateProductOfUser(productUpdateDto, userDto.Id);
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductOfUser(int id)
        {
            var userDto = await _userService.GetUserByNameAsync(HttpContext.User.Identity.Name);

            return CreateActionResult(await _productService.DeleteProductOfUser(id, userDto.Id));
        }

        [HttpPost("Buy")]
        public async Task<IActionResult> BuyProduct(int id)
        {
            var userDto = await _userService.GetUserByNameAsync(HttpContext.User.Identity.Name);

            return CreateActionResult(await _productService.BuyProduct(id, userDto.Id));
        }
    }
}
