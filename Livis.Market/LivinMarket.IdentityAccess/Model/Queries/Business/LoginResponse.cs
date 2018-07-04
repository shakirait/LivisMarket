using System;
using System.Collections.Generic;
using System.Text;

namespace LivinMarket.IdentityAccess.Model.Queries.Business
{
    public class LoginResponse
    {
        public bool IsPowerUser { get; set; }
        public bool IsValid { get; set; }
    }
}
