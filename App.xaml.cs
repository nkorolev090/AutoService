using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using AutoService.Util;

namespace AutoService
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void ApplicationStartUp(object sender, StartupEventArgs e)
        {
            ViewNavigator.ApplicationStartNavigation();
        }
    }
}
