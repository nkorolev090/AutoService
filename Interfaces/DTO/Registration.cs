using DomainModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces.DTO
{
    public class RegistrationDTO
    {
        public int id { get; set; }

        public int car_id { get; set; }

        public string car_name {  get; set; }

        public string start_date {  get; set; }
        public string start_time {  get; set; }

        public string info { get; set; }

        public int status { get; set; }
        public string status_name { get; set; }

        public int? review_id { get; set; }
        public int? reg_price { get; set; }

        public RegistrationDTO(Registration registration) {
            id = registration.id;
            car_id = registration.car_id;
            car_name = registration.Car.brand + " " + registration.Car.model;
            info = registration.info;
            status = registration.status;
            status_name = registration.Status1.name;
            review_id = registration.review_id;
            reg_price = registration.reg_price;
            DateTime minDate = new DateTime(3000, 1, 1);
            TimeSpan minTime = new TimeSpan();
            foreach(Slot slot in registration.Slots)
            {
                if(slot.start_date < minDate)
                {
                    minDate = slot.start_date;
                    minTime = slot.start_time;
                }
            }
            start_date = minDate.ToShortDateString();
            start_time = minTime.ToString();
        }

        public RegistrationDTO()
        {
        }
    }
}
