using System;
using System.IO;
using Xamarin.Essentials;

namespace BJtest.Commons.Helpers
{
    public static class Constants
    {
        public const string BaseUrl = "https://uxcandy.com/~shapoval/test-task-backend/v2";
        private const string DatabaseFilename = "BJtest.db3";
        public static string DatabasePath => Path.Combine(FileSystem.AppDataDirectory, DatabaseFilename);
        public const string REQUEST_PARAM_DEVELOPER = "AntonBedov";

        public enum SortFieldEnum
        {
            ID,
            USERNAME,
            EMAIL,
            STATUS
        }

        public enum SortDirectionEnum
        {
            ASC,
            DESC
        }

        public const string ERROR_OCCURED = "errorOccured";
        public const string START = "start";
        public const string STOP = "stop";
        public const string START_FINISH_NETWORK_REQUEST = "startFinishNetworkRequest";
        public const string NEED_RELOGIN = "need_relogin";
    }
}
