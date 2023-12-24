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
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;

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
        public SeriesCollection Series { get; set; }
        public ObservableCollection<RegistrationDTO> Registrations { get; set;}
        public Dictionary<string, int> MechanicSlots { get; set; }
        public List<string> Intervals { get; }
        private bool btnUpdateEnable;
        public bool BtnUpdateEnable 
        {  
            get
            {
                return btnUpdateEnable;
            }

            set
            {
                btnUpdateEnable = value;
                OnPropertyChanged();
            } 
        }
        private string selectedInterval;
        public string SelectedInterval
        {
            get 
            {
                return selectedInterval;
            }
            set
            {

                selectedInterval = value;

                MechanicSlots.Clear();
                MechanicSlots = slotService.GetMechanicSlotsReport(Mechanic.id, SelectedInterval);
                Series.Clear();
                Func<ChartPoint, string> PointLabel = chartPoint =>
              string.Format("{0:P}", chartPoint.Participation);
                foreach (string breakdown in MechanicSlots.Keys)
                {
                    Series.Add(
                    new PieSeries
                    {
                        Title = breakdown,
                        Values = new ChartValues<ObservableValue> { new ObservableValue(MechanicSlots[breakdown]) },
                        DataLabels = true,
                        //LabelPoint = PointLabel
                    });
                }
                OnPropertyChanged();
            }
        }
        private RegistrationDTO selectedRegistration;
        public RegistrationDTO SelectedRegistration
        { get
            {
                return selectedRegistration;
            }
            set
            {
                selectedRegistration = value;
               
                if(selectedRegistration.status == 4 || selectedRegistration.status == 3)
                {
                    Statuses.Clear();
                    Statuses.Add(registrationService.GetStatus(selectedRegistration.status));
                    BtnUpdateEnable = false;
                }
                else
                {
                    Statuses.Clear();
                    Statuses.AddRange(registrationService.GetStatuses());
                    BtnUpdateEnable = true;
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
            BtnUpdateEnable = false;
            Mechanic = mechanicService.GetMechanic(m_id);
            Registrations = new ObservableCollection<RegistrationDTO>(registrationService.GetMechanicRegistrations(Mechanic.id));
            Statuses = new ObservableCollection<StatusDTO>(registrationService.GetStatuses());
            Intervals = new List<string> { "Месяц", "Квартал", "Полгода", "Год", "Все время" };
            selectedInterval = Intervals.Last();
            MechanicSlots = slotService.GetMechanicSlotsReport(Mechanic.id, SelectedInterval);
            Series = new SeriesCollection();
            Func<ChartPoint, string> PointLabel = chartPoint =>
                          string.Format("{0:P}", chartPoint.Participation);
            foreach (string breakdown in MechanicSlots.Keys)
            {
                Series.Add(
                new PieSeries
                {
                    Title = breakdown,
                    Values = new ChartValues<ObservableValue> { new ObservableValue(MechanicSlots[breakdown]) },
                    DataLabels = true,
                    //LabelPoint = PointLabel
                });
            }

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
