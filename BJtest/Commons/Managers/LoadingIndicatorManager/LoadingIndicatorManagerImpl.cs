using System;
using BJtest.Common.Managers.LoadingIndicatorManager;
using BJtest.Commons.Helpers;
using Xamarin.Forms;

[assembly: Dependency(typeof(LoadingIndicatorManagerImpl))]
namespace BJtest.Common.Managers.LoadingIndicatorManager
{
    public class LoadingIndicatorManagerImpl : ILoadingIndicatorManager
    {
        private bool parallelRequestsCount = false;

        public void ShowIndicator()
        {
            parallelRequestsCount = true;
            MessagingCenter.Send<string>(Constants.START, Constants.START_FINISH_NETWORK_REQUEST);
        }

        public void HideIndicator()
        {
            if (parallelRequestsCount)
                MessagingCenter.Send<string>(Constants.STOP, Constants.START_FINISH_NETWORK_REQUEST);
            parallelRequestsCount = false;
        }
    }
}
