using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel;
using Interfaces.Repository;

namespace DAL.Repository
{
    public class DbRepositorySQL : IDbRepository
    {
        private ModelDB db;
        private CheckInRepositorySQL checkInRepository;
        private CityRepositorySQL cityRepository;
        private ReportRepositorySQL reportRepository;
        private StudentRepositorySQL studentRepository;
        private RoomRepositorySQL roomRepository;

        public DbRepositorySQL() {
            db = new ModelDB();
        }

        public IRepository<Check_IN> Check_Ins 
        {
            get
            {
                if(checkInRepository == null)
                {
                    checkInRepository = new CheckInRepositorySQL(db);
                }
                return checkInRepository;
            }
        }

        public IRepository<City> Cities 
        {
            get
            {
                if(cityRepository == null)
                {
                    cityRepository = new CityRepositorySQL(db);
                }
                return cityRepository;
            }
        }

        public IRepository<Student> Students
        {
            get
            {
                if (studentRepository == null)
                {
                    studentRepository = new StudentRepositorySQL(db);
                }
                return studentRepository;
            }
        }

        public IReportsRepository Reports
        {
            get
            {
                if(reportRepository == null)
                {
                    reportRepository = new ReportRepositorySQL(db);
                }
                return reportRepository;
            }
        }

        public IRepository<Room> Rooms 
        {
            get
            {
                if (roomRepository == null)
                {
                    roomRepository = new RoomRepositorySQL(db);
                }
                return roomRepository;
            }
        }


        public int Save()
        {
            return db.SaveChanges();
        }
    }
}
