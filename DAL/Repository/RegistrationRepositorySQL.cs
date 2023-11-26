using DomainModel;
using Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public class RegistrationRepositorySQL : IRepository<Registration>
    {
        ModelAutoService db;
        public RegistrationRepositorySQL(ModelAutoService db) { this.db = db; }
        public void Create(Registration item)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Registration GetItem(int id)
        {
            throw new NotImplementedException();
        }

        public List<Registration> GetList()
        {
            throw new NotImplementedException();
        }

        public void Update(Registration item)
        {
            throw new NotImplementedException();
        }
    }
}
