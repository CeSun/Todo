using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using ReactiveUI;

namespace Todo.Component
{
    public class LeftMenu : UserControl
    {
        public LeftMenu()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

    }

    public class LeftMenuVM : ReactiveObject
    {
    }
}