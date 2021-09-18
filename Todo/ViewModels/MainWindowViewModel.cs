using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using ReactiveUI;

namespace Todo.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {

        private bool _IsShowLeftMenu = false;
        private bool _IsShowRightMenu = false;
        private bool _CanSeeLeftMenu = false;
        private bool _CanSeeRightMenu = false;
        public bool _PanelIsShow = false;
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