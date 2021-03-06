﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BJtest.Common.Utils;
using BJtest.Commons.Managers.ContentManager;
using BJtest.Models.RestModels;
using BJtest.REST;
using BJtest.ViewModels.ViewsViewModels;
using Newtonsoft.Json.Linq;
using BJtest.Commons.Helpers;
using Xamarin.Forms;
using System.Diagnostics;
using System.Collections.ObjectModel;
using BJtest.Models.DBModels;

[assembly: Dependency(typeof(ContentManagerImpl))]
namespace BJtest.Commons.Managers.ContentManager
{
    public class ContentManagerImpl : IContentManager
    {
        private readonly INetworkService networkService = DependencyService.Get<INetworkService>();

        private ObservableCollection<TaskViewModel> _tasksList = new ObservableCollection<TaskViewModel>();
        public ObservableCollection<TaskViewModel> TasksList
        {
            get => _tasksList;
        }

        private ObservableCollection<PageSelectorItemViewModel> _pageSelectorsList = new ObservableCollection<PageSelectorItemViewModel>();
        public ObservableCollection<PageSelectorItemViewModel> PageSelectorsList
        {
            get => _pageSelectorsList;
        }

        public async Task<bool> LoadTasks(Constants.SortFieldEnum sortFieldType, Constants.SortDirectionEnum sortDirectionType, int pageNum)
        {
            var arg = new GetTasksRequest()
            {
                SortField = sortFieldType.ToString().ToLower(),
                SortDirection = sortDirectionType.ToString().ToLower(),
                Page = pageNum
            };
            var answer = await networkService.PerformNetworkRequest(NetworkService.TaskType.GET_TASKS, new Dictionary<string, object>
                {
                    { "arg1", arg },
                }, false);

            if (answer != null && answer.IsStatusOk && answer.Data is JObject data)
            {
                var model = data.ToObject<GetTasksResponse>();
                if (model != null)
                {
                    var dbList = await App.Database.TaskTable.GetAllItemsAsync();
                    if (dbList == null)
                        dbList = new List<TaskDBModel>();

                    _tasksList.Clear();
                    foreach (var it in model.Tasks)
                        _tasksList.Add(new TaskViewModel(it) { WasEdited = dbList.Where(x => x.Id == it.Id).Any() });

                    int tasksTotal = 0;
                    int.TryParse(model.TotalTaskCount, out tasksTotal);
                    int pagesTotal = (int)Math.Ceiling((double)tasksTotal / 3.0);

                    _pageSelectorsList.Clear();
                    for (int i = 0; i < pagesTotal; i++)
                        _pageSelectorsList.Add(new PageSelectorItemViewModel(i));

                    return true;
                }
            }

            return false;
        }

        public async Task<bool> CreateTask(string userName, string email, string text)
        {
            var arg = new CreateTaskRequest()
            {
                UserName = userName,
                Email = email,
                Text = text
            };
            var answer = await networkService.PerformNetworkRequest(NetworkService.TaskType.CREATE_TASK, new Dictionary<string, object>
                {
                    { "arg1", arg },
                });

            if (answer != null && answer.IsStatusOk && answer.Data is JObject data)
            {
                var model = data.ToObject<TaskRestModel>();
                return model != null;
            }
            return false;
        }

        public async Task<bool> EditTask(int taskId, string text, int status, bool wasChanged)
        {
            var arg = new EditTaskRequest()
            {
                Text = text,
                Status = status
            };
            var answer = await networkService.PerformNetworkRequest(NetworkService.TaskType.EDIT_TASK, new Dictionary<string, object>
                {
                    { "arg1", arg },
                    { "arg2", taskId.ToString() },
                });
            bool res = answer != null && answer.IsStatusOk;
            if (res && wasChanged)
            {
                var taskDb = await App.Database.TaskTable.GetItemByIdAsync(taskId);
                if (taskDb == null)
                {
                    taskDb = new TaskDBModel() { Id = taskId };
                    await App.Database.TaskTable.InsertItemAsync(taskDb);
                }
            }

            return res;
        }

    }
}
