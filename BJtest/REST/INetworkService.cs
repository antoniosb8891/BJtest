using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BJtest.Models.RestModels;
using static BJtest.REST.NetworkService;

namespace BJtest.REST
{
    public interface INetworkService
    {
        void Reinitialize(string newActiveToken);
        Task<BaseResponseRestModel> PerformNetworkRequest(TaskType type, Dictionary<string, object> args, bool showIndicator = true);
    }
}
