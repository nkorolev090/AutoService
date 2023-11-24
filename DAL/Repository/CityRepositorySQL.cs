using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel;
using Interfaces.Repository;

namespace DAL.Repository
{
    public class CityRepositorySQL : IRepository<City>
    {
        ModelDB db;
        public CityRepositorySQL(ModelDB modelDB) {
            db = modelDB;
        }
        public void Create(City item)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public City GetItem(int id)
        {
            return db.City.Find(id);
        }

        public List<City> GetList()
        {
            return db.City.ToList();
        }

        public void Update(City item)
        {
            throw new NotImplementedException();
        }
    }
}
