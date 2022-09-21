using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PayCore.Core.DTOs;
using PayCore.Core.Models;
using PayCore.Core.Repositories;
using PayCore.Core.Services;
using PayCore.Core.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayCore.Service.Services
{
    public class ProductService : GenericService<Product>, IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductService(IGenericRepository<Product> repository, IUnitOfWork unitOfWork, IProductRepository productRepository, IMapper mapper) : base(repository, unitOfWork)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<ProductDto> AddProduct(ProductDto productDto)
        {
            var product = _mapper.Map<Product>(productDto);
            await _repository.AddAsync(product);
            await _unitOfWork.CommitAsync();
            var productCreatedDto = _mapper.Map<ProductDto>(product);

            return productCreatedDto;
        }

        public async Task<CustomResponseDto<ProductDto>> BuyProduct(int productId, string userAppId)
        {
            var product = await  GetByIdAsync(productId);
            if (product == null)
            {
                return CustomResponseDto<ProductDto>.Fail(400,"Product not found");
            }
            if (product.IsSold)
            {
                return CustomResponseDto<ProductDto>.Fail(400, "Product is not exist");
            }

            product.IsSold = true;

            Product newProduct = new Product()
            {
                Color = product.Color,
                CategoryId = product.CategoryId,
                IsSold = false,
                Brand = product.Brand,
                Description = product.Description, 
                Category = product.Category,
                Price = product.Price,
                Name = product.Name,
                IsOfferable = product.IsOfferable,
                UserAppId = userAppId
            };

            await _productRepository.AddAsync(newProduct);

            await _unitOfWork.CommitAsync();

            var productDto = _mapper.Map<ProductDto>(newProduct);
            return CustomResponseDto<ProductDto>.Success(200, productDto);
        }

        public async Task<CustomResponseDto<NoContentDto>> DeleteProductOfUser(int productId, string userAppId)
        {
            var product = await _productRepository.GetByIdAsync(productId);

            if (product.UserAppId != userAppId)
            {
                return CustomResponseDto<NoContentDto>.Fail(400,"This product is not your"); 
            }

            _productRepository.Remove(product);

            _unitOfWork.Commit();

            return CustomResponseDto<NoContentDto>.Success(200);

        }

        public async Task<IEnumerable<ProductDto>> GetProductsForUserIdAsync(string userAppId)
        {
            var products = await _productRepository.Where(x=>x.UserAppId== userAppId).ToListAsync();
           
            var productsDto = _mapper.Map<List<ProductDto>>(products);
            return productsDto;
        }

        public async Task<CustomResponseDto<ProductDto>> UpdateProductOfUser(ProductUpdateDto productUpdateDto, string userAppId)
        {
            var product = await _productRepository.GetByIdAsync(productUpdateDto.Id);

            if (product.UserAppId != userAppId)
            {
                return CustomResponseDto<ProductDto>.Fail(400, "This product is not your");
            }
            
            _productRepository.Update(_mapper.Map<Product>(productUpdateDto));

            _unitOfWork.Commit();

            return CustomResponseDto<ProductDto>.Success(200, _mapper.Map<ProductDto>(productUpdateDto));

        }
    }
}
