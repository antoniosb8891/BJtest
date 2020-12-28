using System;
using System.Collections.Generic;
using BJtest.ViewModels.PagesViewModels;
using Xamarin.Forms;

namespace BJtest.Pages
{
    public partial class LoginPage : ContentPage
    {
        private LoginPageViewModel _viewModel;

        public LoginPage()
        {
            InitializeComponent();

            _viewModel = new LoginPageViewModel(this);
            BindingContext = _viewModel;
        }
    }
}
