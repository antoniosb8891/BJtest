using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Refit;
using BJtest.Common;
using BJtest.Common.Exceptions;
using BJtest.Common.Managers.LoadingIndicatorManager;
using BJtest.Common.Managers.UserManager;
using BJtest.Models.RestModels;
using BJtest.REST;
using Xamarin.Forms;
using BJtest.Commons.Helpers;
using Xamarin.Essentials;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using Newtonsoft.Json;
using System.Linq;

[assembly: Dependency(typeof(NetworkService))]
namespace BJtest.REST
{
    public class NetworkService : INetworkService
    {
        private string BASE_URL => Constants.BaseUrl;

        private readonly ILoadingIndicatorManager loadingIndicatorManager = DependencyService.Get<ILoadingIndicatorManager>();

        private Api api;
        private HttpClient client;
        private CookieContainer cookieContainer;
        private string activeToken;

        public bool Initialized { get; set; } = false;

        public enum TaskType
        {
            LOGIN,
            GET_TASKS,
            CREATE_TASK,
            EDIT_TASK
        }

        public NetworkService()
        {
            activeToken = Settings.Token;
        }

        public void Reinitialize(string newActiveToken)
        {
            activeToken = newActiveToken;
            Initialized = false;
        }

        private void Init(string Url)
        {
            cookieContainer = new CookieContainer();
            var handler = new LoggingHandler(new HttpClientHandler()
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; }
            });
            client = new HttpClient(handler)
            {
                BaseAddress = new Uri(Url),
                Timeout = new TimeSpan(0, 0, 15),
            };
            api = RestService.For<Api>(client, new RefitSettings()
            {
                ContentSerializer = new NewtonsoftJsonContentSerializer(App.JsonSettings)
            });

            Initialized = true;
        }

        public async Task<BaseResponseRestModel> PerformNetworkRequest(TaskType type, Dictionary<string, object> args, bool showIndicator)
        {
            BaseResponseRestModel answer = null;
            IApiResponse apiResponse = null;

            try
            {
                if (Connectivity.NetworkAccess != NetworkAccess.Internet)
                    throw new InternalException("Network is not available");

                if (!Initialized)
                {
                    Init(BASE_URL);
                }

                if (showIndicator)
                    loadingIndicatorManager.ShowIndicator();
                switch (type)
                {
                    case TaskType.LOGIN:
                        {
                            if (args.TryGetValue("arg1", out object reqObj) && reqObj is LoginRequest request)
                            {
                                var response = await api.Login(request, Constants.REQUEST_PARAM_DEVELOPER);
                                apiResponse = response;
                                answer = response.Content;
                            }
                            break;
                        }
                    case TaskType.GET_TASKS:
                        {
                            if (args.TryGetValue("arg1", out object reqObj) && reqObj is GetTasksRequest request)
                            {
                                var response = await api.GetTasks(request, Constants.REQUEST_PARAM_DEVELOPER);
                                apiResponse = response;
                                answer = response.Content;
                            }
                            break;
                        }
                    case TaskType.CREATE_TASK:
                        {
                            if (args.TryGetValue("arg1", out object reqObj) && reqObj is CreateTaskRequest request)
                            {
                                var response = await api.CreateTask(request, Constants.REQUEST_PARAM_DEVELOPER);
                                apiResponse = response;
                                answer = response.Content;
                            }
                            break;
                        }
                    case TaskType.EDIT_TASK:
                        {
                            if (args.TryGetValue("arg1", out object reqObj) && reqObj is EditTaskRequest request
                                && args.TryGetValue("arg2", out object idObj) && idObj is string taskId)
                            {
                                request.Token = activeToken;
                                var response = await api.EditTask(request, taskId, Constants.REQUEST_PARAM_DEVELOPER);
                                apiResponse = response;
                                answer = response.Content;
                            }
                            break;
                        }
                }
                if (showIndicator)
                    loadingIndicatorManager.HideIndicator();
                if (answer == null)
                    throw new InternalException(apiResponse != null ? apiResponse.Error.Content : String.Format("Unknown arguments for {0} request", type.ToString()));

                if (answer.IsStatusOk)
                {
                    apiResponse?.Dispose();
                    return answer;
                }

                if (answer.Data is JObject dict)
                {
                    List<string> list = new List<string>();
                    foreach (JToken token in dict.Children())
                        if (token is JProperty)
                        {
                            var prop = token as JProperty;
                            list.Add(prop.Value.ToString());
                        }
                    throw new InternalException(String.Join("\n", list));
                }
                else
                    throw new InternalException(answer.Data as string);
            }
            catch (Exception e)
            {
                apiResponse?.Dispose();
                loadingIndicatorManager.HideIndicator();
                var exp = ErrorHandler.HandleException(e);
                MessagingCenter.Send<string>(exp != null ? exp.Message : e.Message, Constants.ERROR_OCCURED);
                return null;
            }
        }

    }
}
