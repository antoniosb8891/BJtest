using System;
namespace BJtest.ViewModels.ViewsViewModels
{
    public class PageSelectorItemViewModel : BaseViewModel
    {
        private int _index;

        public PageSelectorItemViewModel(int index)
        {
            _index = index;
        }

        public int Number => _index + 1;

        public int Index => _index;
    }
}
