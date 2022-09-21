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
        public ProductRepository(AppDbContext dbcontext) : base(dbcontext)
        {
        }

        

        public async Task<IEnumerable<Product>> GetAllProductsWithOffersForUserAppAsync(string userAppId)
        {
            var offers = await _dbContext.Products.Include(x => x.Offers).Where(x => x.UserAppId == userAppId).ToListAsync();

            return offers;  
        }

        public async Task<List<Product>> GetProductsForUserIdAsync(string userAppId)
        {
            return await _dbContext.Products.Where(x=>x.UserAppId==userAppId).ToListAsync();
        }
    }
}
