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
        IRepository<Client> Clients {  get; }
        IRepository<Car> Cars { get; }
        IRepository<Slot> Slots { get; }
        IRepository<Registration> Registrations { get; }
        int Save();
    }
}
