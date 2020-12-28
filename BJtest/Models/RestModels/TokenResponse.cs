using System;
using Newtonsoft.Json;

namespace BJtest.Models.RestModels
{
    public class TokenResponse
    {
        [JsonProperty("token")]
        public string Token { get; set; }

    }
}
