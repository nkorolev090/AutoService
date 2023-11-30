using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interfaces.DTO;
using Interfaces.Repository;
using Interfaces.Services;
using DomainModel;

namespace BLL.Services
{
    public class ClientService : IClientService
    {
        IDbRepository db;
        public ClientService(IDbRepository db) { this.db = db; }
        public bool CreateClientDTO(ClientDTO p)
        {
            throw new NotImplementedException();
        }

        public void DeleteClientDTO(int id)
        {
            throw new NotImplementedException();
        }

        public List<ClientDTO> GetAllClientDTO()
        {
            throw new NotImplementedException();
        }

        public ClientDTO GetClientDTO(int id)
        {
            return new ClientDTO(db.Clients.GetItem(id));
        }

        public void UpdateClientDTO(ClientDTO p)
        {
            throw new NotImplementedException();
        }

        public int GetClientDiscount(int id)
        {
            
            return db.Clients.GetItem(id).Discount.sale;
        }
    }
}
