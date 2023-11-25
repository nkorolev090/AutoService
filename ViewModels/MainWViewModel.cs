
using AutoService.Util;
using Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Interfaces.DTO;

namespace AutoService.ViewModels
{
    public class MainWViewModel : INotifyPropertyChanged
    {
        


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }


        public MainWViewModel()
        {
            
        }
        private RelayCommand openMainMenuCommand;
        public RelayCommand OpenMainMenuCommand
        {
            get
            {
                return openMainMenuCommand ?? (
                    openMainMenuCommand = new RelayCommand(obj =>
                    {
                        ViewNavigator.SwitchViewTo(Util.Views.MainMenuView);
                        
                    }));
            }
        }
    }
}
