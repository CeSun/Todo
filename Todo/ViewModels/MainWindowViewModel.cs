using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Microsoft.Graph;
using ReactiveUI;
using Todo.Apis;

namespace Todo.ViewModels
{
    public partial class MainWindowViewModel : ViewModelBase
    {

        private bool _IsShowLeftMenu = false;
        private bool _IsShowRightMenu = false;
        private bool _CanSeeLeftMenu = false;
        private bool _CanSeeRightMenu = false;
        private bool _PanelIsShow = false;
        private int _LogoWidth = 150;
        private bool _LoginButtonCanPressed = true;
        private string _UserCode = "";
        private string _LoginTips = "";
        private bool _IsGetCode = false;
        public string LoginTips
        {
            get => _LoginTips;
            set => this.RaiseAndSetIfChanged(ref _LoginTips, value);
        }
        public string UserCode
        {
            get => _UserCode;
            set => this.RaiseAndSetIfChanged(ref _UserCode, value);
        }
        public async Task CopyCode()
        {
            var tc = new TextCopy.Clipboard();
            await tc.SetTextAsync(UserCode);
            
        }
        public bool IsGetCode
        {
            get => _IsGetCode;
            set => this.RaiseAndSetIfChanged(ref _IsGetCode, value);
        }
        public bool LoginButtonCanPressed
        {
            get => _LoginButtonCanPressed;
            set => this.RaiseAndSetIfChanged(ref _LoginButtonCanPressed, value);
        }

        public string _password;

        public string Password
        {
            get { return _password; }
            set { this.RaiseAndSetIfChanged(ref _password, value); }
        }
        public string _username;

        public string Username
        {
            get { return _username; }
            set { this.RaiseAndSetIfChanged(ref _username, value); }
        }
        public async Task Login()
        {
            try
            {
                LoginButtonCanPressed = false;
                string scope = "Tasks.ReadWrite";
                string clientId = "b600f125-dd3b-4d5b-a331-0bc8007795b6";
                await GraphHelper.Initialize(clientId, scope.Split(";"), async (code, cancellation) => {
                    UserCode = code.UserCode;
                    IsGetCode = true;
                    LoginTips = "3秒后将通过浏览器打开Microsoft账户授权页面\n请在页面中输入以下验证码";
                    await Task.Delay(1000);
                    LoginTips = "2秒后将通过浏览器打开Microsoft账户授权页面\n请在页面中输入以下验证码";
                    await Task.Delay(1000);
                    LoginTips = "1秒后将通过浏览器打开Microsoft账户授权页面\n请在页面中输入以下验证码";
                    await Task.Delay(1000);
                    LoginTips = "已复制验证码并通过浏览器打开Microsoft账户授权页面\n请在页面中输入以下验证码";
                    var tc = new TextCopy.Clipboard();
                    await tc.SetTextAsync(UserCode);
                    Util.OpenBrowser(code.VerificationUri.ToString());
                });
                GlobalValue.Instance.AuthInfo = new AuthResponse() { AccessToken = "123" };
                LoginTips = "登录成功, 正在加载！";
                var t1 = Task.Delay(1000);
                var t2 = GetAllTaskList();
                await t1;
                await t2;
                IsLogin = true;
            } catch(Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
            }
        }
        public User _user;

        public async  Task RefreshCode()
        {
            string scope = "offline_access;Tasks.ReadWrite;Tasks.ReadWrite.Shared";
            string clientId = "b600f125-dd3b-4d5b-a331-0bc8007795b6";
            GraphHelper.Initialize(clientId, scope.Split(";"), async (code, cancellation) => {
                UserCode = code.UserCode;
            });

        }
        public User User
        {
            get => _user;
            set => this.RaiseAndSetIfChanged(ref _user, value);
        }
        public async Task GetAllTaskList()
        {
            User = await GraphHelper.GetUser();
            var lists = await GraphHelper.GetTaskLists();
            List<TaskListInfo> list = new List<TaskListInfo>();
            foreach(var item in lists)
            {
                list.Add(new TaskListInfo(item));
            }
            Menu = list;
            

        }
        public int LogoWidth
        {
            get => _LogoWidth;
            set => this.RaiseAndSetIfChanged(ref _LogoWidth, value);
        }

        private bool _IsLogin = false;
        public bool IsLogin
        {
            get => _IsLogin;
            set => this.RaiseAndSetIfChanged(ref _IsLogin, value);
        }

        int _ProcessBarValue;
        public int ProcessBarValue
        {
            get => _ProcessBarValue;
            set => this.RaiseAndSetIfChanged(ref _ProcessBarValue, value);
            
        }

        public bool CanSeeLeftMenu
        {
            get { return _CanSeeLeftMenu; }
            set
            {
                this.RaiseAndSetIfChanged(ref _CanSeeLeftMenu, value);
                
            }
        }
    
        public bool PanelIsShow
        {
            get
            {
                return _PanelIsShow;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref _PanelIsShow, value);
            }
        }
      
        public bool CanSeeRightMenu
        {
            get { return _CanSeeRightMenu; }
            set { this.RaiseAndSetIfChanged(ref _CanSeeRightMenu, value); }
        }

        public bool IsShowRightMenu
        {
            get { return _IsShowRightMenu; }
            set
            {
                this.RaiseAndSetIfChanged(ref _IsShowRightMenu, value);
                if (value&& !CanSeeRightMenu)
                    PanelIsShow = true;
                else
                {
                    if (!IsShowLeftMenu || CanSeeLeftMenu)
                        PanelIsShow = false;
                }
            }
        }
        
        public bool IsShowLeftMenu
        {
            get { return _IsShowLeftMenu; }
            set
            {
                this.RaiseAndSetIfChanged(ref _IsShowLeftMenu, value);
                if (value && !CanSeeLeftMenu)
                    PanelIsShow = true;
                else
                {
                    if (!IsShowRightMenu || CanSeeRightMenu)
                        PanelIsShow = false;
                }
            }
        }
        public void LeftMenuOut()
        {

            IsShowLeftMenu = false;
        }

        public void LeftMenuIn()
        {
            IsShowLeftMenu = true;
        }
        public void RightMenuIn()
        {
            IsShowRightMenu = true;
        }
        public void RightMenuOut()
        {
            IsShowRightMenu = false;
        }

        public void CloseMenu()
        {
            if (!CanSeeLeftMenu)
                IsShowLeftMenu = false;
            if (!CanSeeRightMenu)
                IsShowRightMenu = false;
        }
    }

}