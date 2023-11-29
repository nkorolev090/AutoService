using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using Interfaces.Services;

namespace AutoService.Views
{
    /// <summary>
    /// Логика взаимодействия для AddRegistration.xaml
    /// </summary>
    public partial class AddRegistration : Window
    {
        
        public AddRegistration(ISlotService slotService, IRegistrationService registrationService, IBreakdownService breakdownService, ICarService carService)
        {
             InitializeComponent();
            
            DataContext = new AddRegistrationViewModel(slotService, registrationService, breakdownService, carService);
           
           
        }

        
    }

    public class gridItem
    {
        public string time { get; set; }
        public string fullName { get; set; }
        public int cost { get; set; }
    }
}
