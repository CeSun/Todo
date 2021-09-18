using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using System;
using System.Threading.Tasks;

namespace Todo.Views
{
    public partial class PleaseLogin : Window
    {
        public void SetPosition(PixelPoint point)
        {

            this.Position = new PixelPoint(x: (int)(point.X - this.Width/2), y: (int)(point.Y - this.Height/2));
        }

        public async Task Open()
        {
            try
            {
                Util.OpenBrowser("https://login.microsoftonline.com/common/oauth2/v2.0/authorize?" +
                "client_id=b600f125-dd3b-4d5b-a331-0bc8007795b6" +
                "&response_type=code" +
                "&redirect_uri=TodoCrossPlatform%3A%2F%2Fauth" +
                "&response_mode=query" +
                "&scope=offline_access%20Tasks.ReadWrite%20Tasks.ReadWrite.Shared" +
                "&state=");
                await Task.Delay(3000);
                System.Environment.Exit(0);
            } catch(Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);

            }
        }
        public PleaseLogin()
        {
            InitializeComponent();
            _ = Open();
#if DEBUG
            this.AttachDevTools();
#endif
        }
        protected override void OnClosed(EventArgs e)
        {
            System.Environment.Exit(0);
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
