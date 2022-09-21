using PayCore.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayCore.Core.Repositories
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task AddOfferToProductAsync(Offer offer);
        IQueryable<Offer> GetAllOffersForProductAsync(int productId);
    }
}
