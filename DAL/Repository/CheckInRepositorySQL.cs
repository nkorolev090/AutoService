using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel;
using Interfaces.Repository;

namespace DAL.Repository
{
    public class CheckInRepositorySQL : IRepository<Check_IN>
    {
        private ModelDB db;
        public CheckInRepositorySQL(ModelDB modelDB) {
            db = modelDB;

        }
        public void Create(Check_IN item)
        {
            db.Check_IN.Add(item);
        }

        public void Delete(int id)
        {
            Check_IN item = db.Check_IN.Find(id);
            if (item != null)
                db.Check_IN.Remove(item);
        }

        public Check_IN GetItem(int id)
        {
            return db.Check_IN.Find(id);
        }

        public List<Check_IN> GetList()
        {
           return db.Check_IN.ToList();
        }

        public void Update(Check_IN item)
        {
            db.Entry(item).State = System.Data.Entity.EntityState.Modified;
        }

    }
}
