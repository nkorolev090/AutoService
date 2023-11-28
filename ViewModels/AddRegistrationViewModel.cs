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
        public ObservableCollection<SlotDTO> Slots { get; set; }
        public SlotDTO SelectedSlot { get; set; }
        public ObservableCollection<BreakdownDTO> Breakdowns { get; set; }
        public BreakdownDTO SelectedBreakdown { get; set; }
        public AddRegistrationViewModel(ISlotService slotService, IRegistrationService registrationService, IBreakdownService breakdownService) 
        {
            
            this.slotService = slotService;
            this.registrationService = registrationService;
            this.breakdownService = breakdownService;
            slotIsChecked = false;
            Slots = new ObservableCollection<SlotDTO>(slotService.GetAllSlots());
            CartSlots = new ObservableCollection<SlotDTO>();
            Breakdowns = new ObservableCollection<BreakdownDTO>(breakdownService.GetAllBreakdowns());
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        private RelayCommand openAboutBreakdown;
        public RelayCommand OpenAboutBreakdown
        {
            get
            {

                return openAboutBreakdown ?? (
                    openAboutBreakdown = new RelayCommand(obj =>
                    {
                        MessageBox.Show(SelectedBreakdown.title + "\n" + SelectedBreakdown.info + "\n " + SelectedBreakdown.price + "Руб.");

                    }));
            }
        }

        private bool slotIsChecked;
        public bool SlotIsChecked
        {
            get
            {
                return slotIsChecked;
            }
            set
            {
                slotIsChecked = value;
                MessageBox.Show(SelectedSlot.id.ToString());
                
                CartSlots.Add(SelectedSlot);
                Slots.Remove(SelectedSlot);
            }
        }
    }
}
