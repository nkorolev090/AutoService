using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces.DTO
{
   
        public class SPResult
        {
            public int id { get; set; }
            public int gradebook_number { get; set; }
            public string full_name { get; set; }
            public int room_number { get; set; }
            public DateTime check_in_date { get; set; }
            public DateTime ouster_date { get; set; }
        }

        public class ReportData
        {
            public DateTime check_in_date { get; set; }
            public string full_name { get; set; }
            public int course { get; set; }
            public int group { get; set; }
        }
 
}
