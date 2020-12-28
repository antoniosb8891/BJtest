using System;
using BJtest.Commons.Utils;
using Newtonsoft.Json;

namespace BJtest.Models.RestModels
{
    public class TaskRestModel
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("username")]
        public string UserName { get; set; } = "";

        [JsonProperty("email")]
        public string Email { get; set; } = "";

        [JsonProperty("text")]
        public string Text { get; set; } = "";

        [JsonProperty("status")]
        public int Status { get; set; }

        public TaskRestModel()
        {
        }

        public TaskRestModel(TaskRestModel model)
        {
            this.CopyPropertiesFrom(model);
        }

        [JsonIgnore]
        public bool IsCompleted
        {
            get => Status == 10;
            set
            {
                Status = value ? 10 : 0;
            }
        }

        public static int GetStatusByCompletedFlag(bool isCompleted)
        {
            return isCompleted ? 10 : 0;
        }
    }
}
