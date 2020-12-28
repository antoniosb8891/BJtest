using System;
using System.Windows.Input;
using BJtest.Common.Managers.UserManager;
using Xamarin.Forms;

namespace BJtest.ViewModels.PagesViewModels
{
    public class LoginPageViewModel : BaseViewModel
    {
        private readonly IUserManager _userManager = DependencyService.Get<IUserManager>();

        private Page _page;
        private string _login;
        private string _password;

        public LoginPageViewModel(Page page)
        {
            _page = page;
        }

        public string Login
        {
            get => _login;
            set
            {
                SetProperty(ref _login, value, nameof(Login));
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                SetProperty(ref _password, value, nameof(Password));
            }
        }

        public ICommand LoginCommand
        {
            get
            {
                return new Command(async () =>
                {
                    if (String.IsNullOrEmpty(Login))
                    {
                        await _page.DisplayAlert("", "Введите Логин", "Ok");
                        return;
                    }
                    if (String.IsNullOrEmpty(Password))
                    {
                        await _page.DisplayAlert("", "Введите Пароль", "Ok");
                        return;
                    }
                    if (await _userManager.Login(Login, Password))
                    {
                        await _page.DisplayAlert("", "Успешно авторизованы", "Ok");
                        await _page.Navigation.PopAsync();
                    }
                }, () => true);
            }
        }
    }
}
