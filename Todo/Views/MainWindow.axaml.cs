using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Todo.Apis;
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
                    vm.LogoWidth = 330;
                }
                else
                {
                    vm.IsShowLeftMenu = false;
                    vm.CanSeeLeftMenu = false;
                    vm.LogoWidth = 150;
                }

                Console.WriteLine(vm.CanSeeLeftMenu);
            });
            _ = OnWindowInit();
#if DEBUG
            this.AttachDevTools();
#endif
        }
        MainWindowViewModel vm;
        public async Task OnWindowInit()
        {
            await Task.Delay(1);
            vm = (MainWindowViewModel)DataContext;
            if (Width >= CanSeeLeftMenuWidth)
            {
                vm.CanSeeLeftMenu = true;
                vm.IsShowLeftMenu = true;
                vm.LogoWidth = 330;
            }
            else
            {
                vm.CanSeeLeftMenu = false;
                vm.CanSeeLeftMenu = false;
                vm.LogoWidth = 150;
            }
            Auth();
        }
        public void Auth()
        {
           ThrowNoLogin();
        }


        private void ThrowNoLogin()
        {
            vm.IsLogin = false;
        }
        protected override void OnGotFocus(GotFocusEventArgs e)
        {
            base.OnGotFocus(e);
            
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