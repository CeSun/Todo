using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Azure.Identity;
using System;
using System.Threading.Tasks;
using Todo.Apis;
using Todo.ViewModels;

namespace Todo.Views
{
    public partial class PleaseLogin : Window
    {
        public void SetPosition(PixelPoint point)
        {

            this.Position = new PixelPoint(x: (int)(point.X - this.Width/2), y: (int)(point.Y - this.Height/2));
        }
        static bool Once = false;
        private bool ShouldClose = true;
        PleaseLoginViewModel vm = null;
        public async Task Open()
        {
            await Task.Delay(1);
            try
            {
                vm = (PleaseLoginViewModel)DataContext;
                string scope = "offline_access;Tasks.ReadWrite;Tasks.ReadWrite.Shared";
                string clientId = "b600f125-dd3b-4d5b-a331-0bc8007795b6";
                GraphHelper.Initialize(clientId, scope.Split(";"), async (code, cancellation) => {
                    vm.Code = code.UserCode;
                    vm.LoginInfo = "3秒后将打开浏览器以登录你的微软账户, 请在浏览器中输入设备码【" + code.UserCode + "】";
                    await Task.Delay(1000);
                    vm.LoginInfo = "2秒后将打开浏览器以登录你的微软账户, 请在浏览器中输入设备码【" + code.UserCode + "】";
                    await Task.Delay(1000);
                    vm.LoginInfo = "1秒后将打开浏览器以登录你的微软账户, 请在浏览器中输入设备码【" + code.UserCode + "】";
                    await Task.Delay(1000);
                    Util.OpenBrowser(code.VerificationUri.ToString());
                    var tc = new TextCopy.Clipboard();
                    await tc.SetTextAsync(code.UserCode);
                    vm.LoginInfo = " 请在浏览器中输入设备码【" + code.UserCode + "】\n 已自动为您复制设备码";

                });
                var accessToken = await GraphHelper.GetAccessTokenAsync(scope.Split(";"));
                GlobalValue.Instance.AuthInfo = new AuthResponse() { AccessToken = accessToken };
                vm.Tips = "登录成功";
                ShouldClose = false;
                await Task.Delay(3000);
                Close();
            } catch(Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);

            }
        }
       
        public PleaseLogin()
        {
            if (!Once)
            {
                Once = !Once;
                return;
            }
            else
            {

            }
            InitializeComponent();
            _ = Open();
#if DEBUG
            this.AttachDevTools();
#endif
        }

        protected override void OnClosed(EventArgs e)
        {
            if (ShouldClose)
                Environment.Exit(0);
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
