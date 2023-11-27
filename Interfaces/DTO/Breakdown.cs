using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel;

namespace Interfaces.DTO
{
    public class BreakdownDTO
    {
        public int id { get; set; }

        public string title { get; set; }

        public string info { get; set; }

        public int price { get; set; }

        public int warranty { get; set; }

        public BreakdownDTO( Breakdown b) 
        {
            id = b.id;
            title = b.title;
            info = b.info;
            price = b.price;
            warranty = b.warranty;
        }
    }
}
