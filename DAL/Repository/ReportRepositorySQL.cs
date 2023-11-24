using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interfaces.DTO;
using Interfaces.Repository;

namespace DAL.Repository
{
    public class ReportRepositorySQL : IReportsRepository
    {
        ModelDB db;
        public ReportRepositorySQL(ModelDB db) {  this.db = db; }
        public List<SPResult> ExecuteSP(int study_year, int housing)
        {
            SqlParameter param1 = new SqlParameter("@study_year", study_year);
            SqlParameter param2 = new SqlParameter("@housing", housing);
            
            var result = db.Database.SqlQuery<SPResult>("check_stored_procedure_lab @study_year, @housing", new object[] { param1, param2 }).ToList();
            return result;
        }

        public List<ReportData> Report1(int val)
        {
            var request = db.Check_IN
               .Join(db.Student, ph => ph.GRADEBOOK_NUMBER, m => m.ID, (ph, m) => new { ph.GRADEBOOK_NUMBER, ph.ROOM_NUMBER, ph.CHECK_IN_DATE, ph.Student.FULL_NAME, m.COURSE, m.GROUP })
               .Where(i => i.GRADEBOOK_NUMBER == val)
               .Select(i => new ReportData() { check_in_date = i.CHECK_IN_DATE, full_name = i.FULL_NAME, course = i.COURSE, group = i.GROUP })
               .ToList();
            return request;
        }
    }
}
