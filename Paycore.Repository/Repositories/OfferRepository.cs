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
    public class OfferRepository : GenericRepository<Offer>, IOfferRepository
    {
        public OfferRepository(AppDbContext dbcontext) : base(dbcontext)
        {
        }
        public async Task<IEnumerable<Offer>> GetAllOffersForUserAppInBoxAsync(string userAppId)
        {
            List<Offer> offers = new List<Offer>();

            var products = await _dbContext.Products.Include(x => x.Offers).Where(x => x.UserAppId == userAppId).ToListAsync();

            offers.AddRange(products.SelectMany(x => x.Offers));

            return offers;
        }
        public async Task<List<Offer>> GetAllOffersForUserAppOutBoxAsync(string userAppId)
        {
            return await _dbContext.Offers.Where(x => x.UserAppId == userAppId).ToListAsync();
        }
    }
}
