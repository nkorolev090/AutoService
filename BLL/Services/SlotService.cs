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
        public SlotDTO CreateSlot(SlotDTO slotDTO)
        {
            Slot slot = new Slot();
            slot.start_date = slotDTO.start_date;
            slot.start_time = slotDTO.start_time;
            slot.registration_id = slotDTO.registration_id;
            if(slot.registration_id != null)
            {
                slot.Registration = db.Registrations.GetItem((int)slot.registration_id);
            }
            slot.breakdown_id = slotDTO.breakdown_id;
            if(slot.breakdown_id != null)
            {
                slot.Breakdown = db.Breakdowns.GetItem((int) slot.breakdown_id);
            }
            slot.mechanic_id = slotDTO.mechanic_id;
          
                slot.Mechanic = db.Mechanics.GetItem((int)slot.mechanic_id);

            db.Slots.Create(slot);
            db.Save();
            return new SlotDTO(db.Slots.GetList().Last());
        }
        public List<SlotDTO> GetAllSlots()
        {
           return db.Slots.GetList().Where(i => i.registration_id == null).Select( i => new SlotDTO(i)).ToList();
        }

        public List<SlotDTO> GetSlotsByDate_Breakdown(DateTime startDate, int breakdown_id) 
        {
            
            List<int> mechanic_ids = db.Mechanic_Breakdowns.GetList().Where(i => i.breakdown_id == breakdown_id).Select(i=>i.mechanic_id).ToList();
            List<DateTime> dateTimes = db.Slots.GetList().Select(i => i.start_date).ToList();
            return db.Slots.GetList().Where(i => i.start_date == startDate && i.breakdown_id == null).Where(i=> mechanic_ids.Contains(i.mechanic_id)).Select(i => new SlotDTO(i)).ToList();
        }

        public List<SlotDTO> GetRegistrationSlots(int regId)
        {
            return db.Slots.GetList().Where(i => i.registration_id == regId).Select(i => new SlotDTO(i)).ToList();
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
            s.breakdown_id = slot.breakdown_id;
            s.Mechanic = db.Mechanics.GetItem(slot.mechanic_id);
            if(slot.breakdown_id != null) { 
                s.Breakdown = db.Breakdowns.GetItem((int)slot.breakdown_id);
            }
            s.registration_id = slot.registration_id;
            if(slot.registration_id != null)
            {
                s.Registration = db.Registrations.GetItem((int)slot.registration_id);
            }
            db.Save();
        }
    }
}
