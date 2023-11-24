﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interfaces.DTO;

namespace Interfaces.Services
{
    public interface IStudentService
    {
        List<StudentDTO> GetAllStudents();
        void CreateStudent(StudentDTO p);
        void UpdateStudent(StudentDTO p);
        void DeleteStudent(int id);
        List<CityDTO> GetCityList();

        StudentDTO GetStudent(int id);

    }
}
