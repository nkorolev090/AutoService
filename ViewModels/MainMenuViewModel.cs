using AutoService.Util;
using Interfaces.DTO;
using Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AutoService.ViewModels
{
    public class MainMenuViewModel : INotifyPropertyChanged
    {
        ICarService carService;
        public ObservableCollection<CarDTO> Cars; 


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        public MainMenuViewModel(ICarService carService) { 
        
            this.carService = carService;
            Cars = new ObservableCollection<CarDTO>(carService.GetAllCarDTO()); 
        }


        private RelayCommand openAddRegCommand;
        public RelayCommand OpenAddRegCommand
        {
            get
            {
                
                return openAddRegCommand ?? (
                    openAddRegCommand = new RelayCommand(obj =>
                    {
                        ViewNavigator.SwitchViewTo(Util.Views.AddRegistrationView);

                    }));
            }
        }
    }
}
