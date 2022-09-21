using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayCore.Core.Models
{
    public class Product : BaseModel
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public bool IsOfferable { get; set; } = true;

        public bool IsSold { get; set; } = false;
        public UserApp UserApp { get; set; } //Ürün sahibinin hesabı
        public string UserAppId { get; set; } 
        public Category Category { get; set; }
        public int CategoryId { get; set; }
        public string Color { get; set; } //Ayrı bir productfeature entitysinde tutulacak
        public string Brand { get; set; } //Ayrı bir productfeature entitysinde tutulacak
        public ICollection<Offer> Offers { get; set; } //Ürüne gelen teklifler
    }
}
