using AutoService.Util;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Interfaces.DTO;
using Interfaces.Services;
using System;

using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AutoService.ViewModels
{
    public class MechanicMenuViewModel : INotifyPropertyChanged
    {
        private static int m_id = 1;
        IMechanicService mechanicService;
        IRegistrationService registrationService;
        ISlotService slotService;
        IClientService clientService;
        public MechanicDTO Mechanic {get; set;}
        public ObservableCollection<RegistrationDTO> Registrations { get; set;}
        private RegistrationDTO selectedRegistration;
        public RegistrationDTO SelectedRegistration
        { get
            {
                return selectedRegistration;
            }
            set
            {
                selectedRegistration = value;
               
                if(selectedRegistration.status == 4)
                {
                    Statuses.Clear();
                    Statuses.Add(registrationService.GetStatus(selectedRegistration.status));
                }
                else
                {
                    Statuses.Clear();
                    Statuses.AddRange(registrationService.GetStatuses());
                }
                SelectedStatus = registrationService.GetStatus(selectedRegistration.status);
                Info = SelectedRegistration.info;
                OnPropertyChanged();
            }
        }
        
        public ObservableCollection<StatusDTO> Statuses { get; set;}

        private StatusDTO selectedStatus;
        public StatusDTO SelectedStatus
        {
            get
            {
                return selectedStatus;
            }
            set
            {
                selectedStatus = value;
                OnPropertyChanged();
            }
        }

        private string info;
        public string Info
        {
            get
            {
                    return info;
            }
            set
            {
                info = value;
                OnPropertyChanged();
            }
        }
        public MechanicMenuViewModel(IMechanicService mechanicService, IRegistrationService registrationService, ISlotService slotService, IClientService clientService)
        {
            this.mechanicService = mechanicService;
            this.registrationService = registrationService;
            this.slotService = slotService;
            this.clientService = clientService;
            Mechanic = mechanicService.GetMechanic(m_id);
            Registrations = new ObservableCollection<RegistrationDTO>(registrationService.GetMechanicRegistrations(Mechanic.id));
            Statuses = new ObservableCollection<StatusDTO>(registrationService.GetStatuses());
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
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
        private RelayCommand updateRegCommand;
        public RelayCommand UpdateRegCommand
        {
            get
            {

                return updateRegCommand ?? (
                    updateRegCommand = new RelayCommand(obj =>
                    {
                        if (MessageBox.Show("Вы действительно хотите отредактировать запись?",
                    "Изменение записи",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question) == MessageBoxResult.Yes)
                        {
                            SelectedRegistration.status = selectedStatus.id;
                            SelectedRegistration.info = info;
                            registrationService.UpdateRegistration(SelectedRegistration);
                            if(SelectedRegistration.status == 4)
                            {
                                clientService.UpdateClientDiscount(SelectedRegistration.client_id, (int)SelectedRegistration.reg_price);
                            }
                           
                        }
                        
                    }));
            }
        }
    }
}
