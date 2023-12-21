using AutoService.Util;
using Interfaces.DTO;
using Interfaces.Services;
using System;
using System.Collections.Generic;
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
        public MechanicDTO Mechanic {get; set;}

        public MechanicMenuViewModel(IMechanicService mechanicService)
        {
            this.mechanicService = mechanicService;
            Mechanic = mechanicService.GetMechanic(m_id);
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
