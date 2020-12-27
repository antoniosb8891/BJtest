using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BJtest.ViewModels.PagesViewModels;
using Xamarin.Forms;

namespace BJtest
{
    public partial class MainPage : ContentPage
    {
        private MainPageViewModel _viewModel;

        public MainPage()
        {
            InitializeComponent();

            _viewModel = new MainPageViewModel(this);
            BindingContext = _viewModel;
        }

        private void TasksList_SelectionChanged(System.Object sender, Xamarin.Forms.SelectionChangedEventArgs e)
        {
        }

        private void AddButtonClicked(System.Object sender, System.EventArgs e)
        {
        }
    }
}
