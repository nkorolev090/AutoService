﻿using Interfaces.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces.Services
{
    public interface ISlotService
    {
        SlotDTO CreateSlot(SlotDTO slot);
        List<SlotDTO> GetAllSlots();
        List<SlotDTO> GetSlotsByDate_Breakdown(DateTime startDate, int breakdown_id);
        List<SlotDTO> GetRegistrationSlots(int regId);
        Dictionary<string, int> GetCarSlots(int carId);
        void UpdateSlot(SlotDTO slot);
        SlotDTO GetSlot(int id);
    }
}
