﻿using AutoService.Util;
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
        ISlotService slotService;
        IClientService clientService;
        IRegistrationService registrationService;
        public ObservableCollection<CarModel> Cars { get; set; }
        public ClientDTO Client { get; set; }

        public ObservableCollection<RegistrationDTO> Registrations { get; set; }

        public ObservableCollection<SlotDTO> RegistrationSlots { get; set; }

        private bool isDialogOpen;
        public bool IsDialogOpen
        {
            get
            {
                return isDialogOpen;
            }
            set
            {
                isDialogOpen = value;
                OnPropertyChanged();
            }
        }
        private RegistrationDTO selectedRegistration;
        public RegistrationDTO SelectedRegistration
        {
            get
            {
                return selectedRegistration;
            }
            set
            {
                selectedRegistration = value;
                RegistrationSlots.Clear();
                RegistrationSlots.AddRange(slotService.GetRegistrationSlots(selectedRegistration.id));
                IsDialogOpen = true;
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        public MainMenuViewModel(ICarService carService, IClientService clientService, IRegistrationService registrationService, ISlotService slotService) { 
            this.registrationService = registrationService;
            this.carService = carService;
            this.clientService = clientService;
            this.slotService = slotService;
            Client = clientService.GetClientDTO(2);
            Cars = new ObservableCollection<CarModel>(carService.GetAllCarDTO(Client.id).Select(i => new CarModel(i)));
            Registrations = new ObservableCollection<RegistrationDTO>(registrationService.GetClientRegistrations(Client.id));
            RegistrationSlots = new ObservableCollection<SlotDTO>();
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

        private RelayCommand cancelWindow;
        public RelayCommand CancelWindow
        {
            get
            {

                return cancelWindow ?? (
                    cancelWindow = new RelayCommand(obj =>
                    {
                        ViewNavigator.SwitchViewTo(Util.Views.MainWindowView);

                    }));
            }
        }
    }
}
