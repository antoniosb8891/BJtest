using System;
using System.Threading.Tasks;
using Refit;
using BJtest.Models.RestModels;

namespace BJtest.REST
{
    public interface Api
    {
        [Post("/login")]
        Task<ApiResponse<BaseResponseRestModel>> Login([Body(BodySerializationMethod.UrlEncoded)] LoginRequest request, [AliasAs("developer")] string developer);

        [Get("/")]
        Task<ApiResponse<BaseResponseRestModel>> GetTasks(GetTasksRequest request, [AliasAs("developer")] string developer);

        [Post("/create")]
        Task<ApiResponse<BaseResponseRestModel>> CreateTask([Body(BodySerializationMethod.UrlEncoded)] CreateTaskRequest request, [AliasAs("developer")] string developer);

        [Post("/edit/{id}")]
        Task<ApiResponse<BaseResponseRestModel>> EditTask([Body(BodySerializationMethod.UrlEncoded)] EditTaskRequest request, [AliasAs("id")] string taskId, [AliasAs("developer")] string developer);

    }
}
