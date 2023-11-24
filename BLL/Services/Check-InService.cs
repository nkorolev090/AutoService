using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Interfaces.DTO;
using Interfaces.Services;
using Interfaces.Repository;
using DomainModel;

namespace BLL.Services
{
    public class Check_InService : ICheckInService
    {
        IDbRepository db;
        public Check_InService(IDbRepository db) {
            this.db = db;
        }

        public List<Check_InDTO> GetAllCheck_In() {
        return db.Check_Ins.GetList().Select(i => new Check_InDTO(i)).ToList();
        }

        public bool CreateCheck_In(Check_InDTO p)
        {
            var room = db.Rooms.GetList()
                .Where(b => b.ROOM_NUMBER == p.room)
                .FirstOrDefault();
            if (room.NUMBER_OF_FREE_SEATS != 0)
            {
                db.Check_Ins.Create(new Check_IN() { GRADEBOOK_NUMBER = p.gradebook_number, ROOM_NUMBER = room.ID, CHECK_IN_DATE = p.check_in_date, OUSTER_DATE = p.ouster_date, Student = db.Students.GetItem(p.gradebook_number) });
               room.NUMBER_OF_FREE_SEATS -= 1;
                db.Save();
                return true;
            }
            else
            {
                return false;
            }
        }

        public void UpdateCheck_In(Check_InDTO p)
        {
            var room = db.Rooms.GetList()
                .Where(b => b.ROOM_NUMBER == p.room)
                .FirstOrDefault();
            Check_IN ph = db.Check_Ins.GetItem(p.id);
            ph.GRADEBOOK_NUMBER = p.gradebook_number;
            ph.ROOM_NUMBER = room.ID;
            ph.CHECK_IN_DATE = p.check_in_date;
            ph.OUSTER_DATE = p.ouster_date;
            db.Save();
        }

        public void DeleteCheck_In(int id)
        {
            var check_in = db.Check_Ins.GetItem(id);
            var room = db.Rooms.GetList()
                .Where(b => b.ID == check_in.ROOM_NUMBER)
                .FirstOrDefault();
            room.NUMBER_OF_FREE_SEATS++;
            db.Check_Ins.Delete(id);
            db.Save();
        }

        public Check_InDTO GetCheck_In(int id)
        {

          //Check_IN ch = db.Check_Ins.GetItem(id);
          //  Check_InDTO check_InDTO = new Check_InDTO();
          //  check_InDTO.id = ch.ID;
          //  check_InDTO.gradebook_number = ch.GRADEBOOK_NUMBER;
          //  check_InDTO.room_number = ch.ROOM_NUMBER;
          //  check_InDTO.room = ch.Room.ROOM_NUMBER;
          //  check_InDTO.check_in_date = ch.CHECK_IN_DATE;
          //  check_InDTO.ouster_date = ch.OUSTER_DATE;
          //  return check_InDTO;

            return this.GetAllCheck_In()
                .Where(i=>i.id == id)
                .FirstOrDefault();
        }
    }
}
