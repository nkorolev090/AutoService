using Interfaces.DTO;
using Interfaces.Services;
using Interfaces.Repository;
using DomainModel;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class StudentService : IStudentService
    {
        IDbRepository db;
        public StudentService(IDbRepository db) {
           this.db = db;
        }
        public List<StudentDTO> GetAllStudents() {
        return db.Students.GetList().Select(i => new StudentDTO(i)).ToList();
        }

        public void CreateStudent(StudentDTO p)
        {
            db.Students.Create(new Student() { FULL_NAME = p.full_name, BIRTHDATE = p.birthdate, ADMISSION_YEAR = p.admission_year, CITY = p.city, COURSE = p.course, GROUP = p.groupe });
            db.Save();
            
        }

        public void UpdateStudent(StudentDTO p)
        {
            Student ph = db.Students.GetItem(p.id);
            ph.FULL_NAME = p.full_name;
            ph.BIRTHDATE = p.birthdate;
            ph.ADMISSION_YEAR = p.admission_year;
            ph.CITY = p.city;
            ph.COURSE = p.course;
            ph.GROUP = p.groupe;
            db.Save();
        }

        public void DeleteStudent(int id)
        {
            db.Students.Delete(id);
            db.Save();
        }


        //public bool Save() {
        //    if (db.SaveChanges() > 0) return true;
        //    return false;
        //}

        public List<CityDTO> GetCityList()
        {
            return db.Cities.GetList().Select(i => new CityDTO(i)).ToList();
        }

        public StudentDTO GetStudent(int id)
        {
            //Student s = db.Students.GetItem(id);
            //StudentDTO sd = new StudentDTO();
            //sd.id = s.ID;
            //sd.full_name = s.FULL_NAME;
            //sd.birthdate = s.BIRTHDATE;
            //sd.course = s.COURSE;
            //sd.groupe = s.GROUP;
            //sd.city = s.CITY;
            //sd.cityName = s.City1.NAME;
            //return sd;
            return this.GetAllStudents()
                .Where(i => i.id == id)
                .FirstOrDefault();
        }
    }
}
