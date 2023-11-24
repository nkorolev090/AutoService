using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel;
using Interfaces.Repository;

namespace DAL.Repository
{
    public class StudentRepositorySQL : IRepository<Student>
    {
        ModelDB db;
        public StudentRepositorySQL(ModelDB db) { this.db = db; }

        public void Create(Student item)
        {
            db.Student.Add(item);
        }

        public void Delete(int id)
        {
            Student item = db.Student.Find(id);
            if(item != null)
                db.Student.Remove(item);    
        }

        public Student GetItem(int id)
        {
            return db.Student.Find(id);
        }

        public List<Student> GetList()
        {
            return db.Student.ToList();
        }

        public void Update(Student item)
        {
            db.Entry(item).State = System.Data.Entity.EntityState.Modified;
        }
    }
}
