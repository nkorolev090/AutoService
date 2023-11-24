using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interfaces.DTO;

namespace Interfaces.Services
{
    public interface ICheckInService
    {
        List<Check_InDTO> GetAllCheck_In();
        bool CreateCheck_In(Check_InDTO p);
        void UpdateCheck_In(Check_InDTO p);
        Check_InDTO GetCheck_In(int id);
        void DeleteCheck_In(int id);
        


    }
}
