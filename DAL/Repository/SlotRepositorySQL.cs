using DomainModel;
using Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public class SlotRepositorySQL : IRepository<Slot>
    {
        ModelAutoService db;
        public SlotRepositorySQL(ModelAutoService db) { this.db = db; }
        public void Create(Slot item)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Slot GetItem(int id)
        {
            throw new NotImplementedException();
        }

        public List<Slot> GetList()
        {
            throw new NotImplementedException();
        }

        public void Update(Slot item)
        {
            throw new NotImplementedException();
        }
    }
}
