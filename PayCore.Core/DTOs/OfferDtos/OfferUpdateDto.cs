using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayCore.Core.DTOs
{
    public class OfferUpdateDto
    {
        public int Id { get; set; }
        public decimal BidPrice { get; set; }
        public int ProductId { get; set; }
        public string UserAppId { get; set; }
    }
}
