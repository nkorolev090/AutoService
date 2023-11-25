using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interfaces.DTO;

namespace AutoService.Models
{
    public class CarModel : INotifyPropertyChanged
    {
        private int id;
        private string model;
        private string brand;
        private string color;
        private int owner_id;
        private int mileage;
        private string br_mod;
        public int Id {
            get { return id; }
            set 
            {
                id = value;
                OnPropertyChanged("id"); 
            }
        }
        public string Model {
            get { return model; }
            set
            {
                model = value;
                OnPropertyChanged("model"); 
            }
        }

        public int Owner_id
        {
            get { return owner_id; }
            set
            {
                owner_id = value;
                OnPropertyChanged("owner_id");
            }
        }

        public string Color
        {
            get { return color; }
            set
            {
                color = value;
                OnPropertyChanged("color");
            }
        }

        public string Brand
        {
            get { return brand; }
            set
            {
                brand = value;
                OnPropertyChanged("brand");
            }
        }
        public string Br_mod
        {
            get { return br_mod; }
            set
            {
                br_mod = value;
                OnPropertyChanged("br_mod");
            }
        }

        public int Mileage
        {
            get { return mileage; }
            set
            {
                mileage = value;
                OnPropertyChanged("mileage");
            }
        }

        public CarModel(CarDTO car)
        {
            id = car.id;
            model = car.model;
            owner_id = car.owner_id;
            color = car.color;
            brand = car.brand;
            br_mod = car.brand + " " + car.model;
            mileage = car.mileage;

        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
