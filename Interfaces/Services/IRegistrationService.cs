using Interfaces.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces.Services
{
    public interface IRegistrationService
    {
        RegistrationDTO GetItem(int id);
        List<RegistrationDTO> GetClientRegistrations(int client_id);
        int CreateRegistration(RegistrationDTO registration);
        void UpdateRegistration(RegistrationDTO registration);
    }
}
