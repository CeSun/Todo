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
        ITodoTaskListTasksCollectionPage tasks;
        List<TodoTask> _UnDoneTasks;
        bool _ShowDoneTasks = true;

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
        public List<TodoTask> UnDoneTasks
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
        List<TodoTask> _DoneTasks;

        public List<TodoTask> DoneTasks
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


        public async Task OnLoadTaskList(string id, string title)
        {
            try
            {
                TaskTitle = title;
                var taskList = await GraphHelper.GetTaskList(id);
                List<TodoTask> doneTasks = new List<TodoTask>();
                List<TodoTask> undoneTasks = new List<TodoTask>();

                foreach (var item in taskList)
                {
                    if (item.Status == Microsoft.Graph.TaskStatus.Completed)
                    {
                        doneTasks.Add(item);
                    } else
                    {
                        undoneTasks.Add(item);
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
    }
}
