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
    public class RegistrationDTO
    {
        public int id { get; set; }

        public int car_id { get; set; }

        public string car_name {  get; set; }

        public DateTime? reg_date {  get; set; }

        public string info { get; set; }

        public int status { get; set; }
       
        public string status_name { get; set; }
       
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
    }
}
