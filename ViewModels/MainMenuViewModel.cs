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

using LiveCharts;
using LiveCharts.Definitions.Series;
using LiveCharts.Wpf;
using LiveCharts.Defaults;

namespace AutoService.ViewModels
{
    public class MainMenuViewModel : INotifyPropertyChanged, IDataErrorInfo
    {
        ICarService carService;
        ISlotService slotService;
        IClientService clientService;
        IRegistrationService registrationService;
        public ObservableCollection<CarDTO> Cars { get; set; }
        public ClientDTO Client { get; set; }
        public int ClientDiscount { get; set; }

        public ObservableCollection<RegistrationDTO> Registrations { get; set; }

        public ObservableCollection<SlotDTO> RegistrationSlots { get; set; }
        public Dictionary<string, int> CarSlots { get; set; }
        public SeriesCollection Series { get; set; }
        public DateTime SelectedDate { get; set; }
        public DateTime SelectedTime { get; set; }
        private CarDTO selectedCar;
        public CarDTO SelectedCar
        {
            get { return selectedCar; }
            set
            {
                selectedCar = value;
                
                CarSlots.Clear();
                CarSlots = slotService.GetCarSlotsReport(selectedCar.id);
                Series.Clear();
                Func<ChartPoint, string> PointLabel = chartPoint =>
                         string.Format("{0:P}", chartPoint.Participation);
                foreach (string breakdown in CarSlots.Keys)
                {
                    Series.Add(
                    new PieSeries
                    {
                        Title = breakdown,
                        Values = new ChartValues<ObservableValue> { new ObservableValue(CarSlots[breakdown]) },
                        DataLabels = true,
                        //LabelPoint = PointLabel
                    });
                }
                OnPropertyChanged();
                
                
            }
        }
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
                        warrantyDate = warrantyDate.Date.AddMonths(SelectedSlot.breakdown_warranty);
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
            Cars = new ObservableCollection<CarDTO>(carService.GetAllCarDTO(Client.id));
            if(Cars.Count > 0)
            {
                selectedCar = Cars.First();
                OnPropertyChanged(nameof(SelectedCar));
            }
            Registrations = new ObservableCollection<RegistrationDTO>(registrationService.GetClientRegistrations(Client.id));
            RegistrationSlots = new ObservableCollection<SlotDTO>();
            CarSlots = slotService.GetCarSlotsReport(SelectedCar.id);
            ClientDiscount = clientService.GetClientDiscount(Client.id);
            SelectedDate = DateTime.Now;
            SelectedTime = DateTime.Now;
            Series = new SeriesCollection();
            Func<ChartPoint, string> PointLabel = chartPoint =>
                          string.Format("{0:P}", chartPoint.Participation);
            foreach (string breakdown in CarSlots.Keys)
            {
                Series.Add(
                new PieSeries
                {
                    Title = breakdown,
                    Values = new ChartValues<ObservableValue> { new ObservableValue(CarSlots[breakdown]) },
                    DataLabels = true,
                    //LabelPoint = PointLabel
                });
            }

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
        private RelayCommand warrantyCommand;
        public RelayCommand WarrantyCommand
        {
            get
            {

                return warrantyCommand ?? (
                    warrantyCommand = new RelayCommand(obj =>
                    {
                       RegistrationDTO registration = new RegistrationDTO();
                        registration.reg_price = 0;
                        registration.reg_date = DateTime.Now;
                        registration.status = 1;
                        registration.car_id = SelectedRegistration.car_id;
                        registration.review_id = SelectedRegistration.review_id;
                        registration.info = "Гарантийный ремонт";
                        registration = registrationService.CreateRegistration(registration);
                        SlotDTO slot = new SlotDTO();
                        slot.start_time = SelectedTime.TimeOfDay;
                        slot.start_date = SelectedDate;
                        slot.registration_id = registration.id;
                        slot.cost = 0;
                        slot.breakdown_id = SelectedSlot.breakdown_id;
                        slot.mechanic_id = SelectedSlot.mechanic_id;
                        slot = slotService.CreateSlot(slot);
                        registrationService.UpdateRegistration(registration);

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
