using Microsoft.EntityFrameworkCore;
using PayCore.Core.Models;
using PayCore.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paycore.Repository.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {

        public ProductRepository(AppDbContext dbContext) : base(dbContext)
        {

        }

        public async Task AddOfferToProductAsync(Offer offer)
        {
            var product = await _dbContext.Products.FindAsync(offer.ProductId);
            product.Offers.Add(offer);
        }

        public IQueryable<Offer> GetAllOffersForProductAsync(int productId)
        {
            var offers = _dbContext.Offers.Where(x=> x.ProductId==productId);
            return offers;
        }
    }
}
