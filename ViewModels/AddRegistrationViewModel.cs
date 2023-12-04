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
        public CarDTO SelectedCar { get; set; }
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

        public string ClientDiscountName { get; set; }
        public int ClientDiscount { get; set; }

        //привязка для комбобокса с видами работ
        public ObservableCollection<BreakdownDTO> Breakdowns { get; set; }

        private BreakdownDTO selectedBreakdown;
        public BreakdownDTO SelectedBreakdown
        { get { return selectedBreakdown; }
            set
            {
                selectedBreakdown = value;
                List<SlotDTO> listS = slotService.GetSlotsByDate_Breakdown(StartDate, SelectedBreakdown.id);
                Slots.Clear();
                foreach (SlotDTO s in listS)
                {
                    Slots.Add(s);
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
                if (selectedBreakdown != null)
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
            Cars = new ObservableCollection<CarDTO>(carService.GetAllCarDTO(2));
            ClientDiscount = clientService.GetClientDiscount(client_id);
            ClientDiscountName = ClientDiscount.ToString() + "%";
            
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
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
                        //MessageBox.Show(SelectedBreakdown.title + "\n" + SelectedBreakdown.info + "\n " + SelectedBreakdown.price + "Руб.");
                      
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
                        ViewNavigator.SwitchViewTo(Util.Views.MainMenuView);

                    }));
            }
        }

        private RelayCommand createRegistraytion;
        public RelayCommand CreateRegistraytion
        {
            get
            {

                return createRegistraytion ?? (
                    createRegistraytion = new RelayCommand(obj =>
                    {
                        if (CartSlots.Count > 0)
                        {
                            RegistrationDTO registration = new RegistrationDTO();
                            registration.car_id = SelectedCar.id;
                            registration.review_id = null;
                            registration.info = null;
                            registration.status = 1;
                            registration.reg_price = RegPrice;
                            int reg_id = registrationService.CreateRegistration(registration);

                            foreach (SlotDTO slotDTO in CartSlots)
                            {
                                slotDTO.registration_id = reg_id;
                                slotService.UpdateSlot(slotDTO);
                                //CartSlots.Remove(slotDTO);
                            }
                            registration = registrationService.GetItem(reg_id);
                            registrationService.UpdateRegistration(registration);
                        }
                    }));
            }
        }
    }
}
