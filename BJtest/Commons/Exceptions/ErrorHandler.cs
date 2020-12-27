using System;
using System.Diagnostics;
using System.Net;
using BJtest.Models.RestModels;
using Xamarin.Forms;
using System.Threading.Tasks;
using BJtest.Commons.Helpers;

namespace BJtest.Common.Exceptions
{
    public class ErrorHandler
    {

        public static BaseResponseRestModel HandleException(Exception exception)
        {
            Debug.WriteLine(exception);
            var response = new BaseResponseRestModel()
            {
                Message = "Exception"
            };
            if (exception.GetType() == typeof(TaskCanceledException))
            {
                response.Message = "Timeout - Server not responding...";
            }
            else if (exception is Refit.ApiException apiException)
            {
                switch (apiException.StatusCode)
                {
                    case HttpStatusCode.Forbidden:
                        response.Message = "Forbidden";
                        break;
                    default:
                        response.Message = "Server Error\n" + apiException.Message;
                        break;
                }
            }
            else if (exception.GetType() == typeof(WebException) || (exception.InnerException != null && exception.InnerException.GetType() == typeof(WebException)))
            {
                response.Message = "Can not connect";
            }
            else
            {
                if (string.IsNullOrEmpty(exception.Data.ToString()))
                {
                    response.Message = "Internal error";
                }
                else
                {
                    response.Message = exception.Data.ToString();
                }
            }
            return response;
        }
    }
}
