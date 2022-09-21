using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PayCore.Core.Models
{
    public class UserApp : IdentityUser
    {
        public ICollection<Product> Products { get; set; }
        public ICollection<Offer> Offers { get; set; } //Yapılan teklifler
    }
}
