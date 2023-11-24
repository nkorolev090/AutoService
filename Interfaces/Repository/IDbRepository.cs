using DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces.Repository
{
    public interface IDbRepository
    {
        IRepository<Check_IN> Check_Ins {  get; }
        IRepository<City> Cities { get; }
        IRepository<Student> Students { get; }
        IRepository<Room> Rooms { get; }
        IReportsRepository Reports { get; }
        int Save();
    }
}
