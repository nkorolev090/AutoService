using BLL.Services;
using Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using AutoService.ViewModels;

namespace AutoService.View
{
    /// <summary>
    /// Логика взаимодействия для MechanicMenu.xaml
    /// </summary>
    public partial class MechanicMenu : Window
    {
 
        public MechanicMenu(IMechanicService mechanicService, IRegistrationService registrationService, ISlotService slotService, IClientService clientService)
        {
            InitializeComponent();
            DataContext = new MechanicMenuViewModel(mechanicService, registrationService, slotService, clientService);
        }
    }
}
