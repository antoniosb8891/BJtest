using System;
using System.Windows.Input;
using BJtest.Common.Managers.UserManager;
using BJtest.Commons.Managers.ContentManager;
using Xamarin.Forms;
using static BJtest.Commons.Helpers.Constants;

namespace BJtest.ViewModels.PagesViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        private readonly IContentManager _contentManager = DependencyService.Get<IContentManager>();
        private readonly IUserManager _userManager = DependencyService.Get<IUserManager>();

        private Page _page;

        private string _authButtonText = "Login";
        public string AuthButtonText
        {
            get => _authButtonText;
            set
            {
                SetProperty(ref _authButtonText, value, nameof(AuthButtonText));
            }
        }

        public MainPageViewModel(Page page)
        {
            _page = page;
        }

        public ICommand AuthCommand
        {
            get
            {
                return new Command(async () =>
                {
                    
                }, () => true);
            }
        }

        public ICommand LoadTasksCommand
        {
            get
            {
                return new Command(async () =>
                {
                    await _contentManager.LoadTasks(SortFieldEnum.ID, SortDirectionEnum.ASC, 1);
                    IsBusy = false;
                }, () => true);
            }
        }
    }
}
