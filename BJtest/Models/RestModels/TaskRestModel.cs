using System;
using Newtonsoft.Json;

namespace BJtest.Models.RestModels
{
    public class TaskRestModel
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("username")]
        public string UserName { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("test")]
        public string Text { get; set; }

        [JsonProperty("statis")]
        public int Status { get; set; }
    }
}
