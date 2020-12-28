using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using BJtest.Common.Managers.UserManager;
using BJtest.Commons.Managers.ContentManager;
using BJtest.ViewModels.ViewsViewModels;
using Xamarin.Forms;
using static BJtest.Commons.Helpers.Constants;

namespace BJtest.ViewModels.PagesViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        private readonly IContentManager _contentManager = DependencyService.Get<IContentManager>();
        private readonly IUserManager _userManager = DependencyService.Get<IUserManager>();

        private Page _page;
        private bool _firstStart = true;

        private int _currentPageNumber = 1;
        private SortDirectionEnum _curSortDirection = SortDirectionEnum.ASC;
        private SortFieldEnum _curSortField = SortFieldEnum.ID;

        public ObservableCollection<TaskViewModel> TasksList => _contentManager.TasksList;
        public ObservableCollection<PageSelectorItemViewModel> PagesList => _contentManager.PageSelectorsList;

        private string _authButtonText = "";
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

        public void OnAppearing()
        {
            CheckAuth();
            if (_firstStart)
            {
                _firstStart = false;
                UpdateList();
            }
        }

        private void CheckAuth()
        {
            AuthButtonText = _userManager.IsLogged() ? "Выйти" : "Войти";
        }

        public void UpdateList()
        {
            IsBusy = true;
        }

        public ICommand AuthCommand
        {
            get
            {
                return new Command(async () =>
                {
                    if (_userManager.IsLogged())
                    {

                    }
                }, () => true);
            }
        }

        public ICommand LoadTasksCommand
        {
            get
            {
                return new Command(async () =>
                {
                    IsBusy = true;
                    await _contentManager.LoadTasks(_curSortField, _curSortDirection, _currentPageNumber);
                    IsBusy = false;
                }, () => true);
            }
        }

        public void ChangeSortFiled(SortFieldEnum sortField)
        {
            _curSortField = sortField;
            LoadTasksCommand.Execute(null);
        }

        public void ChangeSortDirection(SortDirectionEnum sortDirection)
        {
            _curSortDirection = sortDirection;
            LoadTasksCommand.Execute(null);
        }

        public void ChangePage(int pageNumber)
        {
            _currentPageNumber = pageNumber;
            LoadTasksCommand.Execute(null);
        }
    }
}
