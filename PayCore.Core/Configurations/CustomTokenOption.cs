using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayCore.Core.Configurations
{
    public class CustomTokenOption
    {
        public List<string> Audience { get; set; } //İzin verilen siteler
        public string Issuer { get; set; } // Token sağlayıcı
        public int AccessTokenExpiration { get; set; }
        public int RefreshTokenExpiration { get; set; }
        public string SecurityKey { get; set; }
    }
}
