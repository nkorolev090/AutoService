using AutoService.ViewModels;
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

namespace AutoService.Views
{
    /// <summary>
    /// Логика взаимодействия для MainMenu.xaml
    /// </summary>
    public partial class MainMenu : Window
    {
        public MainMenu(ICarService carService, IClientService clientService, IRegistrationService registrationService, ISlotService slotService)
        {
            InitializeComponent();
            DataContext = new MainMenuViewModel(carService, clientService, registrationService, slotService);

        }


    }
}
