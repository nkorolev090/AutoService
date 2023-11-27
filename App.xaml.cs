using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using AutoService.Util;
using Interfaces.Services;
using Ninject;

namespace AutoService
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void ApplicationStartUp(object sender, StartupEventArgs e)
        {
            var kernel = new StandardKernel(new NinjectRegistration(), new ReposModule("ModelAutoService"));

            
            ICarService carService = kernel.Get<ICarService>();
            IClientService clientService = kernel.Get<IClientService>();
            ISlotService slotService = kernel.Get<ISlotService>();
            IRegistrationService registrationService = kernel.Get<IRegistrationService>();  
            ViewNavigator.SetServices(carService, clientService, registrationService, slotService);
            ViewNavigator.ApplicationStartNavigation();
        }
    }
}
