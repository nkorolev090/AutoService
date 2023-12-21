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

namespace AutoService.ViewModels
{
    public class MechanicMenuViewModel : INotifyPropertyChanged
    {
        private static int m_id = 1;
        IMechanicService mechanicService;
        IRegistrationService registrationService;
        ISlotService slotService;
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
                }
                else
                {
                    Statuses.Clear();
                    Statuses.AddRange(registrationService.GetStatuses());
                }
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

        public MechanicMenuViewModel(IMechanicService mechanicService, IRegistrationService registrationService, ISlotService slotService)
        {
            this.mechanicService = mechanicService;
            this.registrationService = registrationService;
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
    }
}
