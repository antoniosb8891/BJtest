using System;
using System.Collections.Generic;
using BJtest.ViewModels.PagesViewModels;
using BJtest.ViewModels.ViewsViewModels;
using Xamarin.Forms;

namespace BJtest.Pages
{
    public partial class TaskInfoPage : ContentPage
    {
        private TaskInfoPageViewModel _viewModel;
        public Action OnCompleted;

        public TaskInfoPage()
        {
            _constructor(null);
        }

        public TaskInfoPage(TaskViewModel taskViewModel)
        {
            _constructor(taskViewModel);
        }

        private void _constructor(TaskViewModel taskViewModel)
        {
            InitializeComponent();
            _viewModel = new TaskInfoPageViewModel(this, taskViewModel, () =>
            {
                OnCompleted?.Invoke();
            });
            BindingContext = _viewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel?.OnAppearing();
        }
    }
}
