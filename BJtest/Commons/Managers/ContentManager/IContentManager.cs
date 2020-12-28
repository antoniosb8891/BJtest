using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using BJtest.Commons.Helpers;
using BJtest.ViewModels.ViewsViewModels;

namespace BJtest.Commons.Managers.ContentManager
{
    public interface IContentManager
    {
        Task<bool> LoadTasks(Constants.SortFieldEnum sortFieldType, Constants.SortDirectionEnum sortDirectionType, int pageNum);
        Task<bool> CreateTask(string userName, string email, string text);

        ObservableCollection<TaskViewModel> TasksList { get; }
        ObservableCollection<PageSelectorItemViewModel> PageSelectorsList { get; }
    }
}
