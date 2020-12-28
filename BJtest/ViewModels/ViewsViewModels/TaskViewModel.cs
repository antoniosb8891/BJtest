using System;
using BJtest.Models.RestModels;

namespace BJtest.ViewModels.ViewsViewModels
{
    public class TaskViewModel : BaseViewModel
    {
        private TaskRestModel _model;
        private bool _wasEdited = false;

        public TaskViewModel()
        {
            _model = new TaskRestModel();
        }

        public TaskViewModel(TaskRestModel model)
        {
            _model = model;
        }

        public TaskViewModel(TaskViewModel other)
        {
            _model = new TaskRestModel(other.GetModel);
        }

        public string UserName
        {
            get => _model.UserName;
            set
            {
                if (_model.UserName == value) return;
                _model.UserName = value;
                OnPropertyChanged(nameof(UserName));
            }
        }

        public string Email
        {
            get => _model.Email;
            set
            {
                if (_model.Email == value) return;
                _model.Email = value;
                OnPropertyChanged(nameof(Email));
            }
        }

        public string Text
        {
            get => _model.Text;
            set
            {
                if (_model.Text == value) return;
                _model.Text = value;
                OnPropertyChanged(nameof(Text));
            }
        }

        public bool IsCompleted
        {
            get => _model.IsCompleted;
            set
            {
                if (_model.IsCompleted == value) return;
                _model.IsCompleted = value;
                OnPropertyChanged(nameof(IsCompleted));
            }
        }

        public bool WasEdited
        {
            get => _wasEdited;
            set
            {
                SetProperty(ref _wasEdited, value, nameof(WasEdited));
            }
        }

        public int Id => _model.Id;
        public int Status => _model.Status;

        public TaskRestModel GetModel => _model;
    }
}
