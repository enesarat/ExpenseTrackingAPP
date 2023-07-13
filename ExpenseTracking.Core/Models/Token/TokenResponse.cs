using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracking.Core.Models.Token
{
    public class TokenResponse
    {
        public DateTime ExpireTime { get; set; }

        public string AccessToken { get; set; }

        public string Role { get; set; }
        public string UserName { get; set; }
    }
}
