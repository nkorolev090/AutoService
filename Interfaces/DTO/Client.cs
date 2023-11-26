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

        public string discount_points { get; set; }

        public string surname { get; set; }

        public string midname { get; set; }

        public ClientDTO(Client client) {
            id = client.id;
            name = client.name;
            discount_id = client.discount_id;
            discount_name = client.Discount.name;
            discount_points = client.discount_points;
            surname = client.surname;
            midname = client.midname;
            full_name = name + " " + midname + " " + surname;
        }
    }
}
