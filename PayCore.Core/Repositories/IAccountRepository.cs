using PayCore.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayCore.Core.Repositories
{
    public interface IAccountRepository : IGenericRepository<Account>
    {
        List<Offer> GetAllOffersInbox(int accountId);
        List<Offer> GetAllOffersOutbox(int accountId);

    }
}
