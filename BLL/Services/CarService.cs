using Interfaces.DTO;
using Interfaces.Repository;
using Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel;

namespace BLL.Services
{
    public class CarService : ICarService
    {
        IDbRepository db;
        public CarService(IDbRepository db) { this.db = db; }

        public void CreateCarDTO(CarDTO p)
        {
            db.Cars.Create(new Car() { id = p.id, brand = p.brand, color = p.color, model = p.model, mileage = p.mileage, owner_id = p.owner_id, Client = db.Clients.GetItem(p.owner_id), Registrations = null });
            db.Save();
        }

        public void DeleteCarDTO(int id)
        {
            db.Cars.Delete(id);
        }

        public List<CarDTO> GetAllCarDTO()
        {
           return db.Cars.GetList().Select(i => new CarDTO(i)).ToList();
        }

        public CarDTO GetCarDTO(int id)
        {
            return new CarDTO(db.Cars.GetItem(id));
        }

        public void UpdateCarDTO(CarDTO p)
        {
            Car car = db.Cars.GetItem(p.id);
            car.model = p.model;
            car.brand = p.brand;
            car.color = p.color;
            car.mileage = p.mileage;
            car.owner_id = p.owner_id;
            car.Client = db.Clients.GetItem(p.owner_id);
            db.Save();
        }
    }
}
