using System;
using System.Collections.Generic;
using System.Text;
using Avalonia.Controls;
using ReactiveUI;

namespace Todo.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public string Greeting => "Welcome to Avalonia!";

        private bool _LeftMenuClassIn = false;
        public bool LeftMenuClassIn
        {
            get
            {
                return _LeftMenuClassIn;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref _LeftMenuClassIn, value);
            }
        }

        private bool _MenuIsShow = false;
        public bool MenuIsShow
        {
            get
            {
                return _MenuIsShow;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref _MenuIsShow, value);
            }
        }
        private bool _LeftMenuInit = true;
        public bool LeftMenuInit 
        {
            get
            {
                return _LeftMenuInit;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref _LeftMenuInit, value);
            }
        }
        public void MenuCommand()
        {
            LeftMenuInit = false;
            MenuIsShow = true;
            LeftMenuClassIn = true;
            LeftMenuClassOut = false;
        }
        
        
        
        private bool _LeftMenuClassOut = false;
        public bool LeftMenuClassOut
        {
            get
            {
                return _LeftMenuClassOut;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref _LeftMenuClassOut, value);
            }
        }
        public void CloseMenu()
        {
            MenuIsShow = false;
            LeftMenuClassOut = true;
            LeftMenuClassIn = false;
        }
    }
}