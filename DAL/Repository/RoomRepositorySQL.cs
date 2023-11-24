using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interfaces.Repository;
using Interfaces.DTO;
using DomainModel;

namespace DAL.Repository
{
    public class RoomRepositorySQL : IRepository<Room>
    {
        ModelDB db;
        public RoomRepositorySQL(ModelDB db) {
           this.db = db;
        }

        public void Create(Room item)
        {
           db.Room.Add(item);
        }

        public void Delete(int id)
        {
            Room item = db.Room.Find(id);
            if(item != null)
                db.Room.Remove(item);
        }

        public Room GetItem(int id)
        {
           return db.Room.Find(id);
        }

        public List<Room> GetList()
        {
            return db.Room.ToList();
        }

        public void Update(Room item)
        {
            db.Entry(item).State = System.Data.Entity.EntityState.Modified;
        }
    }
}
