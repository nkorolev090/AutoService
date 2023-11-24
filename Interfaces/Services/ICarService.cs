using Interfaces.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces.Services
{
    public interface ICarService
    {
        List<CarDTO> GetAllCarDTO();
        bool CreateCarDTO(ClientDTO p);
        void UpdateCarDTO(ClientDTO p);
        CarDTO GetCarDTO(int id);
        void DeleteCarDTO(int id);
    }
}
