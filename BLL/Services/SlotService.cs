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
    public class SlotService : ISlotService
    {
        IDbRepository db;
        public SlotService(IDbRepository db) { this.db = db; }
        public List<SlotDTO> GetAllSlots()
        {
           return db.Slots.GetList().Where(i => i.registration_id == null).Select( i => new SlotDTO(i)).ToList();
        }

        public SlotDTO GetSlot(int id)
        {
            return new SlotDTO(db.Slots.GetItem(id));
        }

        public void UpdateSlot(SlotDTO slot)
        {
            Slot s = db.Slots.GetItem(slot.id);
            s.start_time = slot.start_time;
            s.start_date = slot.start_date;
            s.finish_date = slot.finish_date;
            s.finish_time = slot.finish_time;
            s.mechanic_id = slot.mechanic_id;
            s.Mechanic = db.Mechanics.GetItem(slot.mechanic_id);
            if(slot.breakdown_id != null) { 
                s.Breakdown = db.Breakdowns.GetItem((int)slot.breakdown_id);
            }
        }
    }
}
