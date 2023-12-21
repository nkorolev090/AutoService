using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoService.Views;
using Interfaces.Services;
using AutoService.View;

namespace AutoService.Util
{
    enum Views
    {
        MainWindowView,
        MainMenuView,
        AddRegistrationView,
        MechanicMenu
    }
    internal static class ViewNavigator
    {
        private static ICarService carService;
        private static IClientService clientService;
        private static IRegistrationService registrationService;
        private static ISlotService slotService;
        private static IBreakdownService    breakdownService;
        private static IMechanicService mechanicService;

        private static Window currentView_;

        public static void SetServices(ICarService carS, IClientService clientS, IRegistrationService registrationS, ISlotService slotS, IBreakdownService breakdownS, IMechanicService mechanicS)
        {
            carService = carS;
            clientService = clientS;
            registrationService = registrationS;
            slotService = slotS;
            breakdownService = breakdownS;
            mechanicService = mechanicS;
        }
        private static Window currentView
        { 
            get => currentView_;
            set
            {
                currentView_ = value;
                currentView_.Show();
            }
        }
        public static void ApplicationStartNavigation()
        {
            currentView = new MainWindow();
        }
        public static void SwitchViewTo(Views destinationView)
        {
            Window previousView = currentView;
            switch (destinationView)
            {
                case Views.MainWindowView:
                    currentView = new MainWindow();
                    break;
                case Views.MainMenuView:
                    currentView = new MainMenu(carService, clientService, registrationService, slotService);
                    break;
                case Views.AddRegistrationView:
                    currentView = new AddRegistration(slotService, registrationService, breakdownService, carService, clientService);
                    break;
                case Views.MechanicMenu:
                    currentView = new MechanicMenu(mechanicService);
                    break;
            }
            previousView.Close();
        }

    }
}
