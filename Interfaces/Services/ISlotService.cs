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
        List<SlotDTO> GetAllSlots();
        List<SlotDTO> GetSlotsByDate_Breakdown(DateTime startDate, int breakdown_id);

        void UpdateSlot(SlotDTO slot);
        SlotDTO GetSlot(int id);
    }
}
