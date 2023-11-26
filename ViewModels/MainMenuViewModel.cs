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
using AutoService.Models;

namespace AutoService.ViewModels
{
    public class MainMenuViewModel : INotifyPropertyChanged
    {
        ICarService carService;
        IClientService clientService;
        public ObservableCollection<CarModel> Cars { get; set; }
        public ClientDTO Client { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        public MainMenuViewModel(ICarService carService, IClientService clientService) { 
        
            this.carService = carService;
            this.clientService = clientService;
            Client = clientService.GetClientDTO(1);
            Cars = new ObservableCollection<CarModel>(carService.GetAllCarDTO(Client.id).Select(i => new CarModel(i))); 
            
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
