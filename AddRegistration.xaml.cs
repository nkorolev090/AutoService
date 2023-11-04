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

namespace AutoService
{
    /// <summary>
    /// Логика взаимодействия для AddRegistration.xaml
    /// </summary>
    public partial class AddRegistration : Window
    {
        ObservableCollection<gridItem> items;
        public AddRegistration()
        {
            
            InitializeComponent();
            items= new ObservableCollection<gridItem>();

            items.Add(new gridItem { time = "10:00", fullName = "Иванов И.И.", cost = 1000 });
            items.Add(new gridItem { time = "11:00", fullName = "Петров А.И.", cost = 1500 });
            items.Add(new gridItem { time = "12:30", fullName = "Иванов И.И.", cost = 1000 });
            itemsGrid.ItemsSource = items;
        }

        
    }

    public class gridItem
    {
        public string time { get; set; }
        public string fullName { get; set; }
        public int cost { get; set; }
    }
}
