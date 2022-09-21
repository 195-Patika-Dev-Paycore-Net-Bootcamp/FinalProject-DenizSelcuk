using PayCore.Core.DTOs;
using PayCore.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayCore.Core.Services
{
    public  interface IProductService :IGenericService<Product>
    {
        
        /// <summary>
        /// Kullanıcının sahip olduğu ürünleri listeler
        /// </summary>
        /// <param name="userAppId"></param>
        /// <returns></returns>
        Task<IEnumerable<ProductDto>> GetProductsForUserIdAsync(string userAppId);
        /// <summary>
        /// Kullanıcı ürün listesine ürün ekler
        /// </summary>
        /// <param name="productDto"></param>
        /// <returns></returns>
        Task<ProductDto> AddProduct(ProductDto productDto);
        /// <summary>
        /// Kullanıcın ürün listesinden ürün siler
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="userAppId"></param>
        /// <returns></returns>
        Task<CustomResponseDto<NoContentDto>> DeleteProductOfUser(int productId, string userAppId);
        /// <summary>
        /// Kullanıcının ürün listesindeki ürünü günceller
        /// </summary>
        /// <param name="productUpdateDto"></param>
        /// <param name="userAppId"></param>
        /// <returns></returns>
        Task<CustomResponseDto<ProductDto>> UpdateProductOfUser(ProductUpdateDto productUpdateDto, string userAppId);
        /// <summary>
        /// Ürün satın alır. Mevcut ürün isSold olarak işaretlenir ve tekrar satılamaz. Satın alan kullanıcının ürün listesine yeni ürün eklenir.
        /// </summary>
        /// <param name="productUpdateDto"></param>
        /// <param name="userAppId"></param>
        /// <returns></returns>
        Task<CustomResponseDto<ProductDto>> BuyProduct(int productId, string userAppId);




    }
}
