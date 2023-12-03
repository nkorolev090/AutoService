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
        public void CreateRegistration(RegistrationDTO registration)
        {
            Registration reg = new Registration();
            reg.car_id = registration.car_id;
            reg.reg_price = registration.reg_price;
            reg.info = registration.info;
            reg.review_id = registration.review_id;
            reg.Car = db.Cars.GetItem(registration.car_id);
            db.Save();
            //reg.Repair_Review;
        }

        public List<RegistrationDTO> GetClientRegistrations(int client_id)
        {
            throw new NotImplementedException();
        }

        public void UpdateRegistration(RegistrationDTO registration)
        {
            throw new NotImplementedException();
        }
    }
}
