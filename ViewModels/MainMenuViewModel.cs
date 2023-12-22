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
    public class MainMenuViewModel : INotifyPropertyChanged, IDataErrorInfo
    {
        ICarService carService;
        ISlotService slotService;
        IClientService clientService;
        IRegistrationService registrationService;
        public ObservableCollection<CarModel> Cars { get; set; }
        public ClientDTO Client { get; set; }
        public int ClientDiscount { get; set; }

        public ObservableCollection<RegistrationDTO> Registrations { get; set; }

        public ObservableCollection<SlotDTO> RegistrationSlots { get; set; }
        private SlotDTO selectedSlot;
        public SlotDTO SelectedSlot 
        { 
            get 
            { 
                return selectedSlot; 
            } 
            set 
            { 
                selectedSlot = value;
                OnPropertyChanged();
            } 
        }

        private Visibility visibilityDelRegBtn;
        public Visibility VisibilityDelRegBtn
        {
            get
            {
                return visibilityDelRegBtn;
            }
            set
            {
                visibilityDelRegBtn = value;
                OnPropertyChanged();
            }
        }

        private Visibility visibilityWarrantyBtn;
        public Visibility VisibilityWarrantyBtn
        {
            get
            {
                return visibilityWarrantyBtn;
            }
            set
            {
                visibilityWarrantyBtn = value;
                OnPropertyChanged();
            }
        }
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
                OnPropertyChanged();
                RegistrationSlots.Clear();
                switch (selectedRegistration.status){
                    case 4: //завершена
                        VisibilityDelRegBtn = Visibility.Collapsed;
                        VisibilityWarrantyBtn = Visibility.Visible;
                        break;
                    case 3: //отклонена
                        VisibilityDelRegBtn = Visibility.Collapsed;
                        VisibilityWarrantyBtn = Visibility.Collapsed;
                        break;
                    default: //в обработке или одобрена
                        VisibilityDelRegBtn = Visibility.Visible;
                        VisibilityWarrantyBtn = Visibility.Collapsed;
                        break;

                }
               
                if (SelectedRegistration != null)
                {
                    RegistrationSlots.AddRange(slotService.GetRegistrationSlots(selectedRegistration.id));
                    IsDialogOpen = true;
                }
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
        public string Error
        {
            get { return "...."; }
        }
        public string this[string columnName]
        {
            get
            {
                return Validate(columnName);
            }
        }

        private string Validate(string propertyName)
        {
            // Return error message if there is error on else return empty or null string
            string validationMessage = string.Empty;
            switch (propertyName)
            {
                case "SelectedSlot": 

                    if(SelectedSlot != null)
                    {
                        
                        DateTime warrantyDate = SelectedSlot.start_date;
                        warrantyDate = warrantyDate.Date.AddDays(SelectedSlot.breakdown_warranty*30);
                        if (warrantyDate < DateTime.Now)
                        {
                            validationMessage = "Гарантия истекла";
                        }
                            
                    }
                        
                    break;
            }

            return validationMessage;
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
            ClientDiscount = clientService.GetClientDiscount(Client.id);
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

        private RelayCommand deleteRegCommand;
        public RelayCommand DeleteRegCommand
        {
            get
            {

                return deleteRegCommand ?? (
                    deleteRegCommand = new RelayCommand(obj =>
                    {
                        registrationService.DeleteRegistration(SelectedRegistration.id);
                        Registrations.Remove(SelectedRegistration);
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
