using DomainModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces.DTO
{
    public class ClientDTO
    {
        public int id { get; set; }
        public string full_name {  get; set; }

        public string name { get; set; }

        public int discount_id { get; set; }

        public string discount_name { get; set; }

        public int discount_points { get; set; }

        public string surname { get; set; }

        public string midname { get; set; }
        public string tel_number { get; set; }
        public DateTime birth_date { get; set; }
        public string birth_short { get; set; }

        public ClientDTO(Client client) {
            id = client.id;
            name = client.name;
            discount_id = client.discount_id;
            discount_name = client.Discount.name;
            discount_points = client.discount_points;
            surname = client.surname;
            midname = client.midname;
            tel_number = client.tel_number;
            birth_date = client.birth_date;
            birth_short = birth_date.ToShortDateString();
            full_name = name + " " + midname + " " + surname;
        }
    }
}
