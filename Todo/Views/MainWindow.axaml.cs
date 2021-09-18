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
                }
                else
                {
                    vm.IsShowLeftMenu = false;
                    vm.CanSeeLeftMenu = false;
                }
                Console.WriteLine(vm.CanSeeLeftMenu);
            });
            _ = Auth();
            
#if DEBUG
            this.AttachDevTools();
#endif
        }
        
        
        public async Task Auth()
        {

            await Task.Delay(1);
            MainWindowViewModel vm = (MainWindowViewModel)DataContext;
            if (Width >= CanSeeLeftMenuWidth)
            {
                vm.CanSeeLeftMenu = true;
            }
            else
            {
                vm.CanSeeLeftMenu = false;
            }
            if (LaunchArg.Code == null && GlobalValue.Instance.AuthInfo == null)
            {
                ThrowNoLogin();
                return;
            }
            if (LaunchArg.Code != null)
            {
                var response = await AuthApi.AuthAsync(LaunchArg.Code);
                GlobalValue.Instance.AuthInfo = response;
            }
            else
            {
                await AuthApi.Update();
            }
            
            await Update();
        }

        public async Task Update()
        {

        }

        private void ThrowNoLogin()
        {
            var tip = new PleaseLogin();
            var subPoint = new PixelPoint
                (
                x: (int)(this.Position.X + this.Width / 2),
                y: (int)(this.Position.Y + this.Height / 2)
                );
            tip.SetPosition(subPoint);
            tip.ShowDialog(this);
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