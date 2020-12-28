using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BJtest.Pages;
using BJtest.ViewModels.PagesViewModels;
using BJtest.ViewModels.ViewsViewModels;
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

        private async void TasksList_SelectionChanged(System.Object sender, Xamarin.Forms.SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection == null)
                return;

            var ctrl = e.CurrentSelection.FirstOrDefault() as TaskViewModel;
            if (ctrl != null)
                await Navigation.PushAsync(new TaskInfoPage(ctrl)
                {
                    OnCompleted = () => { _viewModel?.UpdateList(); }
                });

            CollectionView lst = (CollectionView)sender;
            lst.SelectedItem = null;
        }

        private async void AddButtonClicked(System.Object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new TaskInfoPage()
            {
                OnCompleted = () => { _viewModel?.UpdateList(); }
            });
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel?.OnAppearing();
        }

        private void PageSelector_Tapped(System.Object sender, System.EventArgs e)
        {
            if (sender is Frame ctrl && ctrl.BindingContext is PageSelectorItemViewModel viewModel)
            {
                PagesCarousel.Position = viewModel.Index;
                _viewModel?.ChangePage(viewModel.Number);
            }
        }
    }
}
