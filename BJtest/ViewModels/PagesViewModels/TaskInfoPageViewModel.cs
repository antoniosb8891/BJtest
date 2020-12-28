using System;
using System.Windows.Input;
using BJtest.Common.Managers.UserManager;
using BJtest.Commons.Helpers;
using BJtest.Commons.Managers.ContentManager;
using BJtest.Models.RestModels;
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
                _taskViewModel = taskViewModel;

            _userName = _taskViewModel.UserName;
            _email = _taskViewModel.Email;
            _text = _taskViewModel.Text;
            _isCompleted = _taskViewModel.IsCompleted;

            _page.Title = _isNewTask ? "Новая задача" : "Задача";
        }

        public bool IsAdmin => _userManager.IsLogged();

        public bool IsEditable => _isNewTask || IsAdmin;

        public void OnAppearing()
        {
            OnPropertyChanged(nameof(IsAdmin));
            OnPropertyChanged(nameof(IsEditable));
        }

        private string _userName;
        public string UserName
        {
            get => _userName;
            set
            {
                SetProperty(ref _userName, value, nameof(UserName));
            }
        }

        private string _email;
        public string Email
        {
            get => _email;
            set
            {
                SetProperty(ref _email, value, nameof(Email));
            }
        }

        private string _text;
        public string Text
        {
            get => _text;
            set
            {
                SetProperty(ref _text, value, nameof(Text));
            }
        }

        private bool _isCompleted;
        public bool IsCompleted
        {
            get => _isCompleted;
            set
            {
                SetProperty(ref _isCompleted, value, nameof(IsCompleted));
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
                    if (_isNewTask ?
                            await _contentManager.CreateTask(UserName, Email, Text) :
                            await _contentManager.EditTask(_taskViewModel.Id, Text, TaskRestModel.GetStatusByCompletedFlag(_isCompleted), Text != _taskViewModel.Text))
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
