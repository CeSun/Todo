using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using System;
using System.Collections.Generic;
using Todo.ViewModels;

namespace Todo.Views
{
    public partial class MainWindow : Window
    {
        const float CanSeeLeftMenuWidth = 780;
        Dictionary<AvaloniaProperty, EventHandler<AvaloniaPropertyChangedEventArgs>> handlers = new Dictionary<AvaloniaProperty, EventHandler<AvaloniaPropertyChangedEventArgs>>();
        public MainWindow()
        {
            FontFamily = "Microsoft YaHei,Simsun,SimHei,苹方-简,宋体-简";
            InitializeComponent();
            this.PropertyChanged += MainWindow_PropertyChanged;
            handlers.Add(WidthProperty, (sender, e)=> {
                MainWindowViewModel vm = (MainWindowViewModel)DataContext;
                if (Width >= CanSeeLeftMenuWidth)
                {
                    vm.CanSeeLeftMenu = true;
                    vm.IsShowLeftMenu = true;
                }
                else
                {
                    vm.IsShowLeftMenu = false;
                    vm.CanSeeLeftMenu = false;
                }
                Console.WriteLine(vm.CanSeeLeftMenu);
            });
#if DEBUG
            this.AttachDevTools();
#endif
        }
        protected override void OnGotFocus(GotFocusEventArgs e)
        {
            base.OnGotFocus(e);

            MainWindowViewModel vm = (MainWindowViewModel)DataContext;
            if (Width >= CanSeeLeftMenuWidth)
            {
                vm.CanSeeLeftMenu = true;
            }
            else
            {
                vm.CanSeeLeftMenu = false;
            }
        }
        private void MainWindow_PropertyChanged(object sender, AvaloniaPropertyChangedEventArgs e)
        {
            handlers.GetValueOrDefault(e.Property)?.Invoke(sender, e);
            
        }
        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
            
        }



    }
}