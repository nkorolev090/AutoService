using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel;

namespace Interfaces.DTO
{
    public class CityDTO
    {
        public int id { get; set; }
        public string name { get; set; }

        public CityDTO() { }
        public CityDTO(City c)
        {
            id = c.ID;
            name = c.NAME;
        }
    }
}
