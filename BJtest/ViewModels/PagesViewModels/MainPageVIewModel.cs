using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using BJtest.Common.Managers.UserManager;
using BJtest.Commons.Managers.ContentManager;
using BJtest.Pages;
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
        private SortDirectionEnum _curSortDirection;
        private SortFieldEnum _curSortField;

        public ObservableCollection<TaskViewModel> TasksList => _contentManager.TasksList;
        public ObservableCollection<PageSelectorItemViewModel> PagesList => _contentManager.PageSelectorsList;

        public List<SortFieldTypeViewModel> SortFieldsList = new List<SortFieldTypeViewModel>()
        {
            { new SortFieldTypeViewModel(SortFieldEnum.ID, "По ID") },
            { new SortFieldTypeViewModel(SortFieldEnum.USERNAME, "По Имени") },
            { new SortFieldTypeViewModel(SortFieldEnum.EMAIL, "По E-mail") },
            { new SortFieldTypeViewModel(SortFieldEnum.STATUS, "По Статусу") }
        };

        public List<SortDirectionTypeViewModel> SortDirectionsList = new List<SortDirectionTypeViewModel>()
        {
            { new SortDirectionTypeViewModel(SortDirectionEnum.ASC, "По возрастанию") },
            { new SortDirectionTypeViewModel(SortDirectionEnum.DESC, "По убыванию") }
        };

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
            _curSortField = SortFieldsList[0].FieldType;
            _curSortDirection = SortDirectionsList[0].DirectionType;
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
                        _userManager.ResetLogin();
                        CheckAuth();
                    }
                    else
                        await _page.Navigation.PushAsync(new LoginPage());
                }, () => true);
            }
        }

        public ICommand LoadTasksCommand
        {
            get
            {
                return new Command(async () =>
                {
                    if (await _contentManager.LoadTasks(_curSortField, _curSortDirection, _currentPageNumber) && PagesList.Count >= _currentPageNumber && _currentPageNumber > 0)
                        PagesList[_currentPageNumber - 1].IsSelected = true;

                    IsBusy = false;
                }, () => true);
            }
        }

        public void ChangeSortField(SortFieldEnum sortField)
        {
            _curSortField = sortField;
            IsBusy = true;
        }

        public void ChangeSortDirection(SortDirectionEnum sortDirection)
        {
            _curSortDirection = sortDirection;
            IsBusy = true;
        }

        public void ChangePage(int pageNumber)
        {
            _currentPageNumber = pageNumber;
            IsBusy = true;
        }
    }
}
