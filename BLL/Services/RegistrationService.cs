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
            throw new NotImplementedException();
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
