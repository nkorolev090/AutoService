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

namespace AutoService
{
    /// <summary>
    /// Логика взаимодействия для AddRegistration.xaml
    /// </summary>
    public class GridItem
    {
        public GridItem() { }

        DateTime time;
        String masterName;
        int cost;
    }
    public partial class AddRegistration : Window
    {

        public AddRegistration()
        {
            InitializeComponent();
        }
    }
}
