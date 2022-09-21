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
    public class AccountRepository : GenericRepository<Account>//, IAccountRepository
    {

        public AccountRepository(AppDbContext dbContext) : base(dbContext)
        {

        }

        //public List<Offer> GetAllOffersInbox(int accountId)
        //{
        //    List<Offer> offers = new List<Offer>();

        //    var products = _dbContext.Products.Where(x => x.AccountId == accountId); //accountId hesabının ürünleri

        //    foreach ( var product in products)
        //    {
        //        offers.AddRange(product.Offers);
        //    }

        //    return offers;
        //}

        //public List<Offer> GetAllOffersOutbox(int accountId)
        //{
        //    var account =  _dbContext.Accounts.Where(x => x.Id == accountId).First();

        //    return account.Offers.ToList();
        //}
    }
}
