using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel;

namespace Interfaces.DTO
{
    public class Check_InDTO
    {
        public int id { get; set; }
        [DisplayName("Номер зачетной книжки")]
        public int gradebook_number { get; set; }
        [DisplayName("ФИО студента")]
        public string nameS { get; set; }
        
        public int room_number{ get; set; }
        [DisplayName("№ комнаты")]
        public int room {  get; set; }
        [DisplayName("Дата заселения")]
        public DateTime check_in_date{ get; set; }

        public DateTime ouster_date { get; set; }

       public Check_InDTO() { }
        public Check_InDTO(Check_IN t) {
            id = t.ID;
            gradebook_number = t.GRADEBOOK_NUMBER;
            if (t.Student != null)
                nameS = t.Student.FULL_NAME;
            else
                nameS = " ";
            room_number = t.ROOM_NUMBER;
            room = t.Room.ROOM_NUMBER;
           
            check_in_date = t.CHECK_IN_DATE;
            ouster_date = t.OUSTER_DATE;
        }
    }
}
