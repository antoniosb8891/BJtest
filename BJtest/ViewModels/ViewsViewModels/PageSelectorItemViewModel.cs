using System;
namespace BJtest.ViewModels.ViewsViewModels
{
    public class PageSelectorItemViewModel : BaseViewModel
    {
        private int _index;
        private bool _isSelected = false;

        public PageSelectorItemViewModel(int index)
        {
            _index = index;
        }

        public int Number => _index + 1;

        public int Index => _index;

        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                SetProperty(ref _isSelected, value, nameof(IsSelected));
            }
        }
    }
}
