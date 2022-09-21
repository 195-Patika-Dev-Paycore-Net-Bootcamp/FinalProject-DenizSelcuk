using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayCore.Core.Models
{
    public class ProductFeature :BaseModel
    {
        public string Color { get; set; }
        public string Brand { get; set; } 
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
