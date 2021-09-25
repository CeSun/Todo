using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using System;
using Todo.Apis;
using Todo.ViewModels;

namespace Todo.Component
{
    public partial class Task : UserControl
    {
        public Task()
        {
            InitializeComponent();
            var panel = this.FindControl<DockPanel>("MainPanel");
            var TitleBox = this.FindControl<TextBox>("TitleBox");
            TitleBox.AddHandler(LostFocusEvent, (s, e) => {
                var vm = (MainWindowViewModel)DataContext;
                if (TitleBox.Text == null || TitleBox.Text.Trim() == "")
                {
                    TitleBox.Text = vm.TaskListInfo.info.DisplayName;
                    return;
                }
                vm.ChangeTaskListName(TitleBox.Text);
            }, handledEventsToo: true);
            var TaskAddBox = this.FindControl<TextBox>("AddTask");
            TaskAddBox.AddHandler(LostFocusEvent, async (s, e) =>
            {
                if (TaskAddBox.Text == null || TaskAddBox.Text.Trim() == "")
                {
                    return;
                }
                var TaskTitle = TaskAddBox.Text;
                var vm = (MainWindowViewModel)DataContext;
                await GraphHelper.AddTask(TaskTitle, vm.TaskListInfo.info.Id);
                await vm.OnLoadTaskList(vm.TaskListInfo);
                TaskAddBox.Text = "";
             
            }, handledEventsToo: true);
            TaskAddBox.AddHandler(KeyDownEvent, async (s, e) =>
            {
                if (e.Key == Key.Enter)
                {
                    panel.Focus();
                }
            }, handledEventsToo: true);
            TitleBox.AddHandler(KeyDownEvent, async (s, e) =>
            {

                if (e.Key == Key.Enter)
                {
                    panel.Focus();
                }
            }, handledEventsToo: true);

            panel.AddHandler(PointerPressedEvent, (sender, e) =>
            {
                Console.WriteLine("!!!!");
                panel.Focus();
            }, Avalonia.Interactivity.RoutingStrategies.Tunnel, handledEventsToo: true);


        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
