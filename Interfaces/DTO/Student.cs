using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel;
using System.ComponentModel;

namespace Interfaces.DTO
{
    public class StudentDTO
    {
        public int id { get; set; }
        [DisplayName("ФИО студента")]

        public string full_name { get; set; }
        [DisplayName("Дата рождения")]
        public DateTime birthdate { get; set; }
        [DisplayName("Город")]
        public string cityName { get; set; }

        public int city { get; set; }
        [DisplayName("Курс")]
        public int course { get; set; }
        [DisplayName("Группа")]
        public int groupe { get; set; }
        [DisplayName("Год поступления")]
        public int admission_year { get; set; }
        public StudentDTO() { }
        public StudentDTO(Student s)
        {
            id = s.ID;
            full_name = s.FULL_NAME;
            birthdate = s.BIRTHDATE;
            city = s.CITY;
            cityName = s.City1.NAME;
            course = s.COURSE;
            groupe = s.GROUP;
            admission_year = s.ADMISSION_YEAR;
        }
    }
}
