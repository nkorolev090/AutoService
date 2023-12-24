using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using AutoService.Util;
using BLL.Services;
using Interfaces.DTO;
using Interfaces.Services;


namespace AutoService.ViewModels
{
    
    public class AddRegistrationViewModel : INotifyPropertyChanged
    {
        const int client_id = 2;


        ISlotService slotService;
        IClientService clientService;
        IBreakdownService breakdownService;
        IRegistrationService registrationService;
        public ObservableCollection<SlotDTO> CartSlots { get; set; }
        public SlotDTO SelectedCartSlot { get; set; }
        public ObservableCollection<SlotDTO> Slots { get; set; }
        public SlotDTO SelectedSlot { get; set; }
        public ObservableCollection<CarDTO> Cars { get; set; }
        private CarDTO selectedCar;
        public CarDTO SelectedCar 
        {
            get
            {
                return selectedCar;
            }
            set
            {
                selectedCar = value;
                if(CartSlots.Count > 0)
                {
                    BtnCreateEnable = true;
                }
            }
        }
        private bool btnCreateEnable;
        public bool BtnCreateEnable
        {
            get
            {
                return btnCreateEnable;
            }
            set
            {
                btnCreateEnable = value;
                OnPropertyChanged();
            }
        }
        private bool btnAboutEnable;
        public bool BtnAboutEnable
        {
            get
            {
                return btnAboutEnable;
            }
            set
            {
                btnAboutEnable = value;
                OnPropertyChanged();
            }
        }
        
        private int regPrice;
        public int RegPrice
        {
            get
            {
                return regPrice;
            }
            set
            {
                regPrice = value;
                OnPropertyChanged("RegPrice");
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

        public int ClientDiscount { get; set; }

        //привязка для комбобокса с видами работ
        public ObservableCollection<BreakdownDTO> Breakdowns { get; set; }

        private BreakdownDTO selectedBreakdown;
        public BreakdownDTO SelectedBreakdown
        { get { return selectedBreakdown; }
            set
            {
                selectedBreakdown = value;
                OnPropertyChanged();
                if(selectedBreakdown != null && startDate.Date >= DateTime.Now.Date)
                {
                    BtnAboutEnable = true;
                    List<SlotDTO> listS = slotService.GetSlotsByDate_Breakdown(StartDate, SelectedBreakdown.id);
                    Slots.Clear();
                    foreach (SlotDTO s in listS)
                    {
                        Slots.Add(s);
                    }
                }
               
            }
        }
        //Привязка для календаря
        private DateTime startDate;
        public DateTime StartDate 
        {
            get {  return startDate; }
            set
            {
                startDate = value;
                if (selectedBreakdown != null && startDate.Date >= DateTime.Now.Date)
                {
                    List<SlotDTO> listS = slotService.GetSlotsByDate_Breakdown(StartDate, SelectedBreakdown.id);
                    Slots.Clear();
                    foreach (SlotDTO s in listS)
                    {
                        Slots.Add(s);
                    }
                }
            }
        }
        public AddRegistrationViewModel(ISlotService slotService, IRegistrationService registrationService, IBreakdownService breakdownService, ICarService carService, IClientService clientService) 
        {
            
            this.slotService = slotService;
            this.registrationService = registrationService;
            this.breakdownService = breakdownService;
            this.clientService = clientService;
            
            StartDate = DateTime.Now;
            slotIsChecked = false;
            RegPrice = 0;
            Slots = new ObservableCollection<SlotDTO>();
            CartSlots = new ObservableCollection<SlotDTO>();
            Breakdowns = new ObservableCollection<BreakdownDTO>(breakdownService.GetAllBreakdowns());
            SelectedBreakdown = null;
            Cars = new ObservableCollection<CarDTO>(carService.GetAllCarDTO(2));
            ClientDiscount = clientService.GetClientDiscount(client_id);
            
            
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
            
            string validationMessage = string.Empty;
            switch (propertyName)
            {
                case "StartDate":
                    if (StartDate != null)
                    {
                        if(StartDate.Date < DateTime.Now.Date)
                        {
                            validationMessage = "Выберете дату в будующем";
                        }
                    }
                   
                    break;
               
            }

            return validationMessage;
        }

        //Команда для вывода доп сведений об виде работ
        private RelayCommand openAboutBreakdown;
        public RelayCommand OpenAboutBreakdown
        {
            get
            {

                return openAboutBreakdown ?? (
                    openAboutBreakdown = new RelayCommand(obj =>
                    {
                        IsDialogOpen = true;
                      
                    }));
            }
        }
        //Команда для чекбоксов запроса
        private bool slotIsChecked;
        public bool SlotIsChecked
        {
            get
            {
                return slotIsChecked;
            }
            set
            {
                SelectedSlot.breakdown_id = SelectedBreakdown.id;
                SelectedSlot.cost = SelectedBreakdown.price;
                SelectedSlot.breakdown_name = SelectedBreakdown.title;
                slotService.UpdateSlot(SelectedSlot);
                RegPrice += (int)(SelectedSlot.cost * (double)((100-ClientDiscount)/100.0));
                CartSlots.Add(SelectedSlot);
                if (SelectedCar != null)
                {
                    BtnCreateEnable = true;
                }
                Slots.Remove(SelectedSlot);
            }
        }

        //Команда для чекбоксов корзины
        private bool slotIsUnChecked = true;
        public bool SlotIsUnChecked
        {
            get
            {
                return slotIsUnChecked;
            }
            set
            {
                RegPrice -= (int)(SelectedCartSlot.cost * (double)((100 - ClientDiscount) / 100.0));
                SelectedCartSlot.breakdown_id = null;
                SelectedCartSlot.cost = 0;
                SelectedCartSlot.breakdown_name = null;
                slotService.UpdateSlot(SelectedCartSlot);
                CartSlots.Remove(SelectedCartSlot);
                if(CartSlots.Count == 0)
                {
                    BtnCreateEnable = false;
                }
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
                        if(CartSlots.Count  > 0)
                        {
                            foreach(SlotDTO cartSlot in CartSlots)
                            {
                                cartSlot.breakdown_id = null;
                                cartSlot.cost = 0;
                                cartSlot.breakdown_name = null;
                                slotService.UpdateSlot(cartSlot);
                               
                            }
                            CartSlots.Clear();
                        }
                        ViewNavigator.SwitchViewTo(Util.Views.MainMenuView);

                    }));
            }
        }

        private RelayCommand createRegistration;
        public RelayCommand CreateRegistration
        {
            get
            {

                return createRegistration ?? (
                    createRegistration = new RelayCommand(obj =>
                    {
                        if (CartSlots.Count > 0)//перенести всю логику в сервис
                        {
                            RegistrationDTO registration = new RegistrationDTO();
                            registration.car_id = SelectedCar.id;
                            registration.review_id = null;
                            registration.info = null;
                            registration.status = 1;
                            registration.reg_date = DateTime.Now;
                            registration.reg_price = RegPrice;
                            registration = registrationService.CreateRegistration(registration);

                            foreach (SlotDTO slotDTO in CartSlots)
                            {
                                slotDTO.registration_id = registration.id;
                                slotService.UpdateSlot(slotDTO);
                            }
                            CartSlots.Clear();
                            registration = registrationService.GetItem(registration.id);
                            registrationService.UpdateRegistration(registration);
                        }
                    }));
            }
        }
    }
}
