using System;
using BJtest.Commons.Helpers;

namespace BJtest.ViewModels.ViewsViewModels
{
    public class SortDirectionTypeViewModel
    {
        private Constants.SortDirectionEnum _directionType;
        private string _name;

        public SortDirectionTypeViewModel(Constants.SortDirectionEnum directionType, string name)
        {
            _directionType = directionType;
            _name = name;
        }

        public Constants.SortDirectionEnum DirectionType => _directionType;

        public string Name => _name;
    }
}
