using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using BLL.Services;
using Interfaces.DTO;
using Interfaces.Services;

namespace AutoService.ViewModels
{
    public class AddRegistrationViewModel : INotifyPropertyChanged
    {
        ISlotService slotService;
        public ObservableCollection<SlotDTO> Slots { get; set; }
        public AddRegistrationViewModel(ISlotService slotService) 
        {
            
            this.slotService = slotService;
            Slots = new ObservableCollection<SlotDTO>(slotService.GetAllSlots());
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
