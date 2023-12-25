using DomainModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces.DTO
{
    public class RegistrationDTO : INotifyPropertyChanged
    {
        public int id { get; set; }

        public int car_id { get; set; }

        public string car_name {  get; set; }

        public DateTime? reg_date {  get; set; }

        private string _info;
        public string info 
        {
            get
            {
                return _info;
            }
            set
            {
                _info = value;
                OnPropertyChanged();
            }

        }


        public int status { get; set; }

        private string _status_name;
        public string status_name 
        {
            get
            {
                return _status_name;
            }
            set
            {
                _status_name = value;
                OnPropertyChanged();
            }
        }
       
        public int client_id {  get; set; }
        public int? review_id { get; set; }
        public int? reg_price { get; set; }

        public RegistrationDTO(Registration registration) {
            id = registration.id;
            car_id = registration.car_id;
            car_name = registration.Car.brand + " " + registration.Car.model;
            client_id = registration.Car.owner_id;
            info = registration.info;
            status = registration.status;
            status_name = registration.Status1.name;
            review_id = registration.review_id;
            reg_price = registration.reg_price;
            reg_date = registration.reg_date;
        }
       
        public RegistrationDTO()
        {
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
