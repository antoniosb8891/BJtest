using System;
using Refit;

namespace BJtest.Models.RestModels
{
    public class EditTaskRequest
    {
        [AliasAs("status")]
        public int Status { get; set; }

        [AliasAs("text")]
        public string Text { get; set; }

        [AliasAs("token")]
        public string Token { get; set; }
    }
}
