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
            TitleBox.AddHandler(KeyDownEvent, (s, e) => {
                if (e.Key == Key.Enter)
                {
                    panel.Focus();
                    var vm = (MainWindowViewModel)DataContext;
                    vm.ChangeTaskListName(TitleBox.Text);
                }    
            }, handledEventsToo: true);
            var TaskAddBox = this.FindControl<TextBox>("AddTask");
            TaskAddBox.AddHandler(KeyDownEvent, async (s, e) =>
            {
                if (e.Key == Key.Enter)
                {
                    panel.Focus();
                    var TaskTitle = TaskAddBox.Text;
                    var vm = (MainWindowViewModel)DataContext;
                    await GraphHelper.AddTask(TaskTitle, vm.TaskListInfo.info.Id);
                    await vm.OnLoadTaskList(vm.TaskListInfo);
                    TaskAddBox.Text = "";
                }
            }, handledEventsToo: true);
            panel.AddHandler(PointerPressedEvent, (sender, e) =>
            {
                var point = e.GetPosition(null);
                Console.WriteLine("!!");
                // panel.Focus();
            }, handledEventsToo: true);


        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
