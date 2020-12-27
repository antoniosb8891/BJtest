using System;
using Refit;

namespace BJtest.Models.RestModels
{
    public class LoginRequest
    {
        [AliasAs("username")]
        public string UserName { get; set; }

        [AliasAs("password")]
        public string Password { get; set; }
    }
}
