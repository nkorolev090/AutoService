using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoService.Views;

namespace AutoService.Util
{
    enum Views
    {
        MainWindowView,
        MainMenuView,
        AddRegistrationView
    }
    internal static class ViewNavigator
    {
        
        private static Window currentView_;
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
                    currentView = new MainMenu();
                    break;
                case Views.AddRegistrationView:
                    currentView = new AddRegistration();
                    break;
            }
            previousView.Close();
        }

    }
}
