﻿using System;
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
        private ModelAutoService db;
       private ClientRepositorySQL clientRepository;
        private CarRepositorySQL carRepository;
        private SlotRepositorySQL slotRepository;
        private RegistrationRepositorySQL registrationRepository;

        public DbRepositorySQL() {
            db = new ModelAutoService();
        }


        public IRepository<Client> Clients 
        {
            get
            {
                if (clientRepository == null)
                {
                    clientRepository = new ClientRepositorySQL(db);
                }
                return clientRepository;
            }
        }

        

        public IRepository<Car> Cars
        {
            get
            {
                if (carRepository == null)
                {
                    carRepository = new CarRepositorySQL(db);
                }
                return carRepository;
            }
        }

        public IRepository<Slot> Slots
        {
            get
            {
                if (slotRepository == null)
                {
                    slotRepository = new SlotRepositorySQL(db);
                }
                return slotRepository;
            }
        }

        public IRepository<Registration> Registrations
        {
            get
            {
                if (registrationRepository == null)
                {
                    registrationRepository = new RegistrationRepositorySQL(db);
                }
                return registrationRepository;
            }
        }
        public int Save()
        {
            return db.SaveChanges();
        }
    }
}
