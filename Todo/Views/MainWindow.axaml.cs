using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Media;

namespace Todo.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            FontFamily = "Microsoft YaHei,Simsun,苹方-简,宋体-简";
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
            // Background = Brushes.DeepPink
        }

    }
}