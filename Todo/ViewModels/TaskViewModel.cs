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
    public class MyTodoTask
    {
        MainWindowViewModel vm;
        string listId;
        public MyTodoTask(TodoTask a, string listId, MainWindowViewModel vm)
        {
            this.vm = vm;
            this.listId = listId;
            A = a;
        }
        public TodoTask A;
        public string Title
        {
            get { return A.Title; }
        }
        public async Task Done()
        {
            vm.LoadTaskIter++;
            vm.MoveToDone(A.Id);
            A.Status = Microsoft.Graph.TaskStatus.Completed;
            await GraphHelper.graphClient.Me.Todo.Lists[listId].Tasks[A.Id]
            .Request()
            .UpdateAsync(A);
            await vm.OnLoadTaskList(vm.TaskListInfo);
            vm.LoadTaskIter--;
        }

        public async Task OpenTask()
        {
            vm.RightMenuIn();
            vm.SingleTaskTitle = A.Title;
            vm.TaskContent = A.Body.Content;
            vm.TaskIsDone = A.Status == Microsoft.Graph.TaskStatus.Completed;
            vm.TaskCreatedTime = A.CreatedDateTime?.ToString("创建于 yyyy-MM-dd");
            vm.singleTodoTask = A;

        }
        public async Task UnDone()
        {
            vm.LoadTaskIter++;
            vm.MoveToUnDone(A.Id);
            A.Status = Microsoft.Graph.TaskStatus.NotStarted;
            await GraphHelper.graphClient.Me.Todo.Lists[listId].Tasks[A.Id]
            .Request()
            .UpdateAsync(A);
            await vm.OnLoadTaskList(vm.TaskListInfo);
            vm.LoadTaskIter--;
            
        }

        public async Task UpdateTaskListName()
        {

        }
    }

    public class TaskDetail
    {
        public TodoTask todoTask;
        public TaskDetail(TodoTask todoTask)
        {
            this.todoTask = todoTask;
        }

        public string Title { get { return todoTask.Title; } }

        public string Detail { get { return todoTask.Body.Content; } }
    }
    public partial class MainWindowViewModel : ViewModelBase
    {
        public TodoTask singleTodoTask;
        ITodoTaskListTasksCollectionPage tasks;
        List<MyTodoTask> _UnDoneTasks;
        bool _ShowDoneTasks = true;

        public TaskDetail taskDetail;
      
        string _TitleColor = "#ffffff";
        public string TitleColor
        {
            set
            {
                this.RaiseAndSetIfChanged(ref _TitleColor, value);
            }
            get
            {
                return _TitleColor;
            }
        }
        public async Task TitleInputGetFocus()
        {
            TitleColor = "#5F74C3";
        }
        public async Task TitleInputLostFocus()
        {
            TitleColor = "#ffffff";

        }

        public async Task DoneTask()
        {
            Console.WriteLine("??");
        }
        public bool ShowDoneTasks
        {
            set
            {
                this.RaiseAndSetIfChanged(ref _ShowDoneTasks, value);
            }
            get
            {
                return _ShowDoneTasks;
            }
        }
        public List<MyTodoTask> UnDoneTasks
        {
            set
            {
                this.RaiseAndSetIfChanged(ref _UnDoneTasks, value);
            }
            get
            {
                return _UnDoneTasks;
            }
        }
        List<MyTodoTask> _DoneTasks;

        public List<MyTodoTask> DoneTasks
        {
            set
            {
                if (value == null || value.Count == 0)
                    ShowDoneTasks = false;
                else
                    ShowDoneTasks = true;
                this.RaiseAndSetIfChanged(ref _DoneTasks, value);
            }
            get
            {
                return _DoneTasks;
            }
        }

        public TaskListInfo TaskListInfo;

        public int LoadTaskIter = 0;
        public async Task OnLoadTaskList(TaskListInfo info)
        {
            try
            {
                this.TaskListInfo = info;
                TaskTitle = info.info.DisplayName;
                var taskList = await GraphHelper.GetTaskList(info.info.Id);
                List<MyTodoTask> doneTasks = new List<MyTodoTask>();
                List<MyTodoTask> undoneTasks = new List<MyTodoTask>();
                if (LoadTaskIter > 1)
                {
                    return;
                }
                foreach (var item in taskList)
                {
                    if (item.Status == Microsoft.Graph.TaskStatus.Completed)
                    {
                        doneTasks.Add(new MyTodoTask(item, info.info.Id, this));
                    } else
                    {
                        undoneTasks.Add(new MyTodoTask(item, info.info.Id, this));
                    }
                }
                UnDoneTasks = undoneTasks;
                DoneTasks = doneTasks;

            }catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
            }
        }

        public void MoveToUnDone(string id)
        {
            var item = DoneTasks.Find(res => res.A.Id == id);
            if (item == null)
                return;
            var n = new List<MyTodoTask>();
            var m = new List<MyTodoTask>();
            n.AddRange(DoneTasks);
            n.Remove(item);
            DoneTasks = n;

            m.AddRange(UnDoneTasks);
            m.Add(item);
            UnDoneTasks = m;
        }
        public void MoveToDone(string id)
        {
            var item = UnDoneTasks.Find(res => res.A.Id == id);
            if (item == null)
                return;
            var n = new List<MyTodoTask>();
            var m = new List<MyTodoTask>();
            n.AddRange(UnDoneTasks);
            n.Remove(item);
            UnDoneTasks = n;
            m.AddRange(DoneTasks);
            m.Add(item);
            DoneTasks = m;
        }
        public ITodoTaskListTasksCollectionPage todoTasks
        {
            set
            {
                this.RaiseAndSetIfChanged(ref tasks, value);
            }
            get
            {
                return tasks;
            }
        }
        string _TaskTitle;
        public string TaskTitle
        {
            get
            {
                return _TaskTitle;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref _TaskTitle, value);
            }
        }

        bool _NoDoneTaskOpen = true;

        public bool NoDoneTaskOpen {
            set
            {
                this.RaiseAndSetIfChanged(ref _NoDoneTaskOpen, value);
            }
            get
            {
                return _NoDoneTaskOpen;
            }
        }
        public void OpenNoDoneTask()
        {
            NoDoneTaskOpen = !NoDoneTaskOpen;
        }

        public async Task ChangeTaskListName(string name)
        {
            TaskListInfo.info.DisplayName = name;
            await GraphHelper.graphClient.Me.Todo.Lists[TaskListInfo.info.Id]
            .Request()
            .UpdateAsync(TaskListInfo.info);
            await OnLoadTaskList(TaskListInfo);
        }

        public async Task DeleteTaskList()
        {
            await GraphHelper.graphClient.Me.Todo.Lists[TaskListInfo.info.Id]
           .Request()
           .DeleteAsync();
            await GetAllTaskList();
        }

    }
}
