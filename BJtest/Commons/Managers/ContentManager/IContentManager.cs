﻿using System;
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
        Task<bool> EditTask(int taskId, string text, int status, bool wasChanged);

        ObservableCollection<TaskViewModel> TasksList { get; }
        ObservableCollection<PageSelectorItemViewModel> PageSelectorsList { get; }
    }
}
