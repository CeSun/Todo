using Microsoft.Graph;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Apis;

namespace Todo.ViewModels
{
    public partial class MainWindowViewModel : ViewModelBase
    {
        string _SingleTaskTitle;
        public string SingleTaskTitle
        {
            get {  return _SingleTaskTitle; }
            set { this.RaiseAndSetIfChanged(ref _SingleTaskTitle, value); }
        }

        public TodoTask todoTaskInfo;
        public bool _TaskIsDone;

        public string taskid;

        public string _TaskCreatedTime;
        public string TaskCreatedTime
        {
            get { return _TaskCreatedTime; }
            set { this.RaiseAndSetIfChanged(ref _TaskCreatedTime, value); }
        }
        public bool TaskIsDone
        {
            get { return _TaskIsDone; }
            set { this.RaiseAndSetIfChanged(ref _TaskIsDone, value); }
        }

        public async Task DeleteTask()
        {
            await GraphHelper.graphClient.Me.Todo.Lists[TaskListInfo.info.Id].Tasks[singleTodoTask.Id].Request().DeleteAsync();
            RightMenuOut();
            await this.OnLoadTaskList(TaskListInfo);
        }
        string _TaskContent;
        public string TaskContent
        {
            get { return _TaskContent; }
            set { this.RaiseAndSetIfChanged(ref _TaskContent, value); }
        }

        public async Task DoneThisTask()
        {
            if (singleTodoTask.Status == Microsoft.Graph.TaskStatus.Completed)
            {
                singleTodoTask.Status = Microsoft.Graph.TaskStatus.NotStarted;
            }
            else
            {
                singleTodoTask.Status = Microsoft.Graph.TaskStatus.Completed;
            }
            await GraphHelper.graphClient.Me.Todo.Lists[TaskListInfo.info.Id].Tasks[singleTodoTask.Id]
            .Request()
            .UpdateAsync(singleTodoTask);
            await OnLoadTaskList(TaskListInfo);
        }
    }
}
