using System;
using BJtest.Models.RestModels;

namespace BJtest.ViewModels.ViewsViewModels
{
    public class TaskViewModel : BaseViewModel
    {
        private TaskRestModel _model;

        public TaskViewModel(TaskRestModel model)
        {
            _model = model;
        }
    }
}
