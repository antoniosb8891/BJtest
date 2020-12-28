using System;
using BJtest.Commons.Helpers;

namespace BJtest.ViewModels.ViewsViewModels
{
    public class SortFieldTypeViewModel
    {
        private Constants.SortFieldEnum _fieldType;
        private string _name;

        public SortFieldTypeViewModel(Constants.SortFieldEnum fieldType, string name)
        {
            _fieldType = fieldType;
            _name = name;
        }

        public Constants.SortFieldEnum FieldType => _fieldType;

        public string Name => _name;
    }
}
