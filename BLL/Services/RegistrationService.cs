using DomainModel;
using Interfaces.DTO;
using Interfaces.Repository;
using Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class RegistrationService : IRegistrationService
    {
        IDbRepository db;
        public RegistrationService(IDbRepository db) { this.db = db; }
        public int CreateRegistration(RegistrationDTO registration)
        {
            Registration reg = new Registration();
            reg.car_id = registration.car_id;
            reg.reg_price = registration.reg_price;
            reg.info = registration.info;
            reg.review_id = registration.review_id;
            reg.status = registration.status;
            reg.Car = db.Cars.GetItem(registration.car_id);
            db.Registrations.Create(reg);
            //reg.Repair_Review;
            //reg.Status1
            db.Save();

            return db.Registrations.GetList().Last().id;
            
        }
        public RegistrationDTO GetItem(int id)
        {
            return new RegistrationDTO(db.Registrations.GetItem(id));
        }

        public List<RegistrationDTO> GetClientRegistrations(int client_id)
        {
            return db.Registrations.GetList().Where(i => i.Car.owner_id == client_id).ToList().Select(i => new RegistrationDTO(i)).ToList();
        }

        public void UpdateRegistration(RegistrationDTO registration)
        {
            Registration reg = db.Registrations.GetItem(registration.id);
            reg.reg_price = registration.reg_price;
            reg.info = registration.info;
            reg.car_id = registration.car_id;
            reg.Car = db.Cars.GetItem(registration.car_id);
            reg.Slots = db.Slots.GetList().Where(i => i.registration_id == registration.id).ToList();
            reg.status = registration.status;
            reg.review_id = registration.review_id;
            //reg.Repair_Review
            //reg.Status1
            db.Save();

        }
    }
}
