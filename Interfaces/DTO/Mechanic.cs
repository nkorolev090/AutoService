﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel;

namespace Interfaces.DTO
{
    public class MechanicDTO
    {
        public int id { get; set; }

        public string name { get; set; }

        public string surname { get; set; }

        public string midname { get; set; }
        public string full_name { get; set; }
        public string tel_number { get; set; }
        public MechanicDTO(Mechanic mechanic) 
        { 
            id = mechanic.id;
            name = mechanic.name;
            surname = mechanic.surname;
            midname = mechanic.midname;
            tel_number = mechanic.tel_number;
            full_name = name + " " + midname + " " + surname;
        }
    }
}
