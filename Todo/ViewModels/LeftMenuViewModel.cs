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
    public class TaskListInfo
    {
        public TaskListInfo(TodoTaskList list)
        {
            info = list;
        }
        public TodoTaskList info;
        public override string ToString()
        {
            return info.DisplayName;
        }
    }
    public partial class MainWindowViewModel : ViewModelBase
    {
        private int _Index = 0;

        public List<TaskListInfo> tasklists = new List<TaskListInfo>();
        
        public List<TaskListInfo> Menu
        {
            get
            {
                return tasklists;
            }
            set
            {
                
                this.RaiseAndSetIfChanged(ref tasklists, value);
            }
        }
        public int Index
        {
            get
            {
                return _Index;
            }
            set 
            {
                if (value < 0)
                    return;
                _ = OnLoadTaskList(Menu[value]);
                this.RaiseAndSetIfChanged(ref _Index, value);
                if (!CanSeeLeftMenu)
                    LeftMenuOut();
            }
        }
        public void IsSelectedChanged()
        {
            Console.WriteLine("1");
        }
        public void SelectedChanged()
        {
            Console.WriteLine("2");
        }

        public async Task NewTaskList()
        {
            int i = 1;
            var Name = "";
            while (true)
            {
                Name = "新增列表" + i;
                if(Menu.Find(res => res.info.DisplayName == Name) == default)
                {
                    break;
                }
                i++;
            }
            _ = await GraphHelper.graphClient.Me.Todo.Lists
            .Request()
            .AddAsync(new TodoTaskList() { DisplayName= Name });
            await GetAllTaskList();
        }
    }
}
