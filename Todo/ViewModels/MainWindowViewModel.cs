using System;
using System.Collections.Generic;
using System.Text;
using Avalonia.Controls;
using ReactiveUI;

namespace Todo.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {

        bool _IsShowLeftMenu = true;
        bool _CanSeeLeftMenu = false;
        public bool CanSeeLeftMenu
        {
            get { return _CanSeeLeftMenu; }
            set { this.RaiseAndSetIfChanged(ref _CanSeeLeftMenu, value); }
        }

        public bool IsShowLeftMenu
        {
            get { return _IsShowLeftMenu; }
            set { this.RaiseAndSetIfChanged(ref _IsShowLeftMenu, value); }
        }
        public void LeftMenuOut()
        {

            IsShowLeftMenu = false;
        }
    }
}