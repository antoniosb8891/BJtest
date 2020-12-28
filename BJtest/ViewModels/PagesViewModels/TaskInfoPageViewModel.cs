using System;
using System.Windows.Input;
using BJtest.Common.Managers.UserManager;
using BJtest.Commons.Helpers;
using BJtest.Commons.Managers.ContentManager;
using BJtest.ViewModels.ViewsViewModels;
using Xamarin.Forms;

namespace BJtest.ViewModels.PagesViewModels
{
    public class TaskInfoPageViewModel : BaseViewModel
    {
        private readonly IContentManager _contentManager = DependencyService.Get<IContentManager>();
        private readonly IUserManager _userManager = DependencyService.Get<IUserManager>();

        private Page _page;
        private TaskViewModel _taskViewModel;
        private bool _isNewTask = false;
        private Action _onCompleted;

        public TaskInfoPageViewModel(Page page, TaskViewModel taskViewModel, Action onCompleted)
        {
            _page = page;
            _onCompleted = onCompleted;
            if (taskViewModel == null)
            {
                _taskViewModel = new TaskViewModel();
                _isNewTask = true;
            }
            else
                _taskViewModel = new TaskViewModel(taskViewModel);
        }

        public bool IsAdmin => _userManager.IsLogged();

        public bool IsEditable => _isNewTask || IsAdmin;

        public void OnAppearing()
        {
            OnPropertyChanged(nameof(IsAdmin));
            OnPropertyChanged(nameof(IsEditable));
        }

        public string UserName
        {
            get => _taskViewModel.UserName;
            set
            {
                if (_taskViewModel.UserName == value) return;
                _taskViewModel.UserName = value;
                OnPropertyChanged(nameof(UserName));
            }
        }

        public string Email
        {
            get => _taskViewModel.Email;
            set
            {
                if (_taskViewModel.Email == value) return;
                _taskViewModel.Email = value;
                OnPropertyChanged(nameof(Email));
            }
        }

        public string Text
        {
            get => _taskViewModel.Text;
            set
            {
                if (_taskViewModel.Text == value) return;
                _taskViewModel.Text = value;
                OnPropertyChanged(nameof(Text));
            }
        }

        public bool IsCompleted
        {
            get => _taskViewModel.IsCompleted;
            set
            {
                if (_taskViewModel.IsCompleted == value) return;
                _taskViewModel.IsCompleted = value;
                OnPropertyChanged(nameof(IsCompleted));
            }
        }

        public ICommand SaveCommand
        {
            get
            {
                return new Command(async () =>
                {
                    if (String.IsNullOrEmpty(UserName))
                    {
                        await _page.DisplayAlert("", "Введите Имя пользователя", "Ok");
                        return;
                    }
                    if (!MyExtensions.IsValidEmail(Email))
                    {
                        await _page.DisplayAlert("", "Введите корректный E-mail", "Ok");
                        return;
                    }
                    if (String.IsNullOrEmpty(Text))
                    {
                        await _page.DisplayAlert("", "Введите Текст задачи", "Ok");
                        return;
                    }
                    if (await _contentManager.CreateTask(UserName, Email, Text))
                    {
                        await _page.DisplayAlert("", _isNewTask ? "Задача добавлена" : "Изменения сохранены", "Ok");
                        await _page.Navigation.PopAsync();
                        _onCompleted?.Invoke();
                    }
                }, () => IsEditable);
            }
        }
    }
}
