using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Todo.Apis;
using Todo.ViewModels;

namespace Todo.Component
{
    public class TaskDetail : UserControl
    {
        public TaskDetail()
        {
            InitializeComponent();
            var taskName = this.FindControl<TextBox>("TaskName");
            var taskContent = this.FindControl<TextBox>("TaskContent");
            taskName.AddHandler(LostFocusEvent, async (s, e) => {
                var vm = (MainWindowViewModel)DataContext;
                vm.singleTodoTask.Title = taskName.Text;
                await GraphHelper.graphClient.Me.Todo.Lists[vm.TaskListInfo.info.Id].Tasks[vm.singleTodoTask.Id]
                .Request()
                .UpdateAsync(vm.singleTodoTask);
                await vm.OnLoadTaskList(vm.TaskListInfo);
            }, handledEventsToo: true);
            taskContent.AddHandler(LostFocusEvent, async (s, e) => {
                var vm = (MainWindowViewModel)DataContext;
                vm.singleTodoTask.Body.Content = taskContent.Text;
                await GraphHelper.graphClient.Me.Todo.Lists[vm.TaskListInfo.info.Id].Tasks[vm.singleTodoTask.Id]
                .Request()
                .UpdateAsync(vm.singleTodoTask);
                await vm.OnLoadTaskList(vm.TaskListInfo);
            }, handledEventsToo: true);

            var BackgroundGrid = this.FindControl<Grid>("Background");
            BackgroundGrid.AddHandler(PointerPressedEvent, (s, e) => { BackgroundGrid.Focus(); }, Avalonia.Interactivity.RoutingStrategies.Tunnel, true);

            this.AddHandler(PointerPressedEvent, (s, e) => { BackgroundGrid.Focus(); }, Avalonia.Interactivity.RoutingStrategies.Tunnel, true);
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}