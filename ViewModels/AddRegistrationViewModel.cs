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
        ISlotService slotService;
        IBreakdownService breakdownService;
        IRegistrationService registrationService;
        public ObservableCollection<SlotDTO> CartSlots { get; set; }
        public SlotDTO SelectedCartSlot { get; set; }
        public ObservableCollection<SlotDTO> Slots { get; set; }
        public SlotDTO SelectedSlot { get; set; }

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
                List<SlotDTO> listS = slotService.GetSlotsByDate_Breakdown(StartDate, SelectedBreakdown.id);
                Slots.Clear();
                foreach (SlotDTO s in listS)
                {
                    Slots.Add(s);
                }
            }
        }
        public AddRegistrationViewModel(ISlotService slotService, IRegistrationService registrationService, IBreakdownService breakdownService) 
        {
            
            this.slotService = slotService;
            this.registrationService = registrationService;
            this.breakdownService = breakdownService;
            slotIsChecked = false;
            Slots = new ObservableCollection<SlotDTO>();
            CartSlots = new ObservableCollection<SlotDTO>();
            Breakdowns = new ObservableCollection<BreakdownDTO>(breakdownService.GetAllBreakdowns());
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
                SelectedCartSlot.breakdown_id = null;
                SelectedCartSlot.cost = 0;
                SelectedCartSlot.breakdown_name = null;
                slotService.UpdateSlot(SelectedCartSlot);
                CartSlots.Remove(SelectedCartSlot);
               
            }
        }
    }
}
