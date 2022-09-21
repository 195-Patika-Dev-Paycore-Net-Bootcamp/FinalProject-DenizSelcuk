using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayCore.Core.Models
{
    public class Offer :BaseModel
    {
        public decimal BidPrice { get; set; }
        public bool IsRefused { get; set; } = false;
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public string UserAppId { get; set; } //Teklifi yapan hesap ıd
        public UserApp UserApp{ get; set; } //Teklifi yapan hesap
        
        
    }
}
