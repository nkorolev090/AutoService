using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel;

namespace Interfaces.DTO
{
    public class CarDTO
    {
        public int id { get; set; }
        public string model { get; set; }

        public int owner_id { get; set; }

        public string color { get; set; }

        public string brand { get; set; }
        public string br_mod { get; set; }

        public int mileage { get; set; }

        public CarDTO(Car car) { 
            id = car.id;
            model = car.model;
            owner_id = car.owner_id;
            brand = car.brand;
            br_mod = car.brand + " " + car.model;
            mileage = car.mileage;

        }
    }
}
