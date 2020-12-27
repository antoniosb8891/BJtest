using System;
using Newtonsoft.Json;

namespace BJtest.Models.RestModels
{
    public class BaseResponseRestModel
    {
        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("message")]
        public dynamic Data { get; set; }

        // ---

        [JsonIgnore]
        public string Message { get; set; }

        [JsonIgnore]
        public const string STATUS_SUCCESS = "ok";

        [JsonIgnore]
        public const string STATUS_ERROR = "error";

        [JsonIgnore]
        public bool IsStatusOk => Status == STATUS_SUCCESS;

        [JsonIgnore]
        public bool IsStatusError => Status == STATUS_ERROR;

    }
}
