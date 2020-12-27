using System;
using Newtonsoft.Json;

namespace BJtest.Models.RestModels
{
    public class GetTasksResponse
    {
        [JsonProperty("tasks")]
        public TaskRestModel[] Tasks { get; set; }

        [JsonProperty("total_task_count")]
        public string TotalTaskCount { get; set; }

    }
}
