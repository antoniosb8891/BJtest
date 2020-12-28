using System;
using Refit;

namespace BJtest.Models.RestModels
{
    public class CreateTaskRequest
    {
        [AliasAs("username")]
        public string UserName { get; set; }

        [AliasAs("email")]
        public string Email { get; set; }

        [AliasAs("text")]
        public string Text { get; set; }
    }
}
