using System;
using Refit;

namespace BJtest.Models.RestModels
{
    public class GetTasksRequest
    {
        [AliasAs("sort_field")]
        public string SortField { get; set; }

        [AliasAs("sort_direction")]
        public string SortDirection { get; set; }

        [AliasAs("page")]
        public int Page { get; set; }

    }
}
