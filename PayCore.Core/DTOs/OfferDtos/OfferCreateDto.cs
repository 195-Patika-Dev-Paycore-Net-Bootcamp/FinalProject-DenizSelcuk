using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayCore.Core.DTOs
{
    public class OfferCreateDto
    {
        public decimal BidPrice { get; set; }
        public int ProductId { get; set; }
    }
}
