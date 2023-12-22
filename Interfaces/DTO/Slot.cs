using DomainModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces.DTO
{
    public class SlotDTO
    {
        public int id { get; set; }

        public int? breakdown_id { get; set; }
        public string breakdown_name { get; set; } 
        public int breakdown_warranty { get; set; }
        public int cost {  get; set; }

        public int mechanic_id { get; set; }

        public string mechanic_name {  get; set; }

        public TimeSpan start_time { get; set; }

        public DateTime start_date { get; set; }

        public TimeSpan finish_time { get; set; }

        public DateTime finish_date { get; set; }

        public int? registration_id { get; set; }

        public SlotDTO(Slot slot)
        {
            this.id = slot.id;
            this.breakdown_id = slot.breakdown_id;
            if( slot.breakdown_id != null )
            {
                this.breakdown_name = slot.Breakdown.title;
                this.breakdown_warranty = slot.Breakdown.warranty;
                this.cost = slot.Breakdown.price;
            }
            
            this.mechanic_id = slot.mechanic_id;
            this.mechanic_name = slot.Mechanic.surname + " " + slot.Mechanic.name[0] + ". " + slot.Mechanic.midname[0] + ".";
            this.start_time = slot.start_time;
            this.start_date = slot.start_date;
            this.finish_time = slot.finish_time;
            this.finish_date = slot.finish_date;
            this.registration_id = slot.registration_id;
        }
        public SlotDTO() { }
    }
}
