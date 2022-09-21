using PayCore.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayCore.Core.Repositories
{
    public interface IOfferRepository 
    {

        Task<IEnumerable<Offer>> GetAllOffersForUserAppInBoxAsync(string userAppId);
        Task<List<Offer>> GetAllOffersForUserAppOutBoxAsync(string userAppId);
    }
}
