using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interfaces.DTO;

namespace Interfaces.Services
{
    public interface IReportsService
    {
        List<SPResult> ExecSP(int study_year, int housing);
        List<ReportData> Report1(int val);
    }
}
