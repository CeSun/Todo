using ReactiveUI;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Todo.ViewModels
{
    public class PleaseLoginViewModel : ViewModelBase
    {
        private string _LoginInfo = "请稍后";
        private string _Tips = "您尚未登录！";
        private string _Code = "";

        public string Tips
        {
            get { return _Tips; }
            set { this.RaiseAndSetIfChanged(ref _Tips, value); }
        }
        public string Code
        {
            get { return _Code; }
            set { this.RaiseAndSetIfChanged(ref _Code, value); }
        }
        public string LoginInfo
        {
            get { return _LoginInfo; }
            set { this.RaiseAndSetIfChanged(ref _LoginInfo, value); }
        }
        public async Task CopyCode()
        {
            var tc = new TextCopy.Clipboard();
            await tc.SetTextAsync(Code);
        }
    }
}
