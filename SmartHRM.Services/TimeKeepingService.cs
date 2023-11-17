using HUG.CRUD.Services;
using SmartHRM.Repository;
using SmartHRM.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace SmartHRM.Services
{
    public class TimeKeepingService 
    {
        private readonly TimeKeepingRepository _TimeKeepingRepository;
        private readonly EmployeeRepository _EmployeeRepository;
        public TimeKeepingService(TimeKeepingRepository TimeKeepingRepository, EmployeeRepository EmployeeRepository)
        {
            _TimeKeepingRepository = TimeKeepingRepository;
            _EmployeeRepository = EmployeeRepository;
        }
        public ResponseModel CreateTimeKeeping(TimeKeeping TimeKeepingCreate)
        {
            var TimeKeepings = _TimeKeepingRepository.GetAll()
                            .Where(l => l.EmployeeId == TimeKeepingCreate.EmployeeId 
                            && l.TimeAttendance.ToShortDateString() == TimeKeepingCreate.TimeAttendance.ToShortDateString())
                            .FirstOrDefault();
            if (TimeKeepings != null)
            {
                return new ResponseModel(422, "TimeKeeping already exists");
            }

            if (!_TimeKeepingRepository.Create(TimeKeepingCreate))
            {
                return new ResponseModel(500, "Something went wrong while saving");
            }

            return new ResponseModel(201, "Successfully created");
        }

        public ResponseModel DeleteTimeKeeping(int TimeKeepingId)
        {
            if (!_TimeKeepingRepository.IsExists(TimeKeepingId)) return new ResponseModel(404, "Not found");
            var TimeKeepingToDelete = _TimeKeepingRepository.GetById(TimeKeepingId);
            if (!_TimeKeepingRepository.Delete(TimeKeepingToDelete))
            {
                return new ResponseModel(500, "Something went wrong when deleting TimeKeeping");
            }
            return new ResponseModel(204, "");
        }

        public TimeKeeperEmployee? GetTimeKeeping(int TkesId)
        {
            if (!_TimeKeepingRepository.IsExists(TkesId)) return null;
            var res = GetTimeKeepings().FirstOrDefault(x => x.Id == TkesId);
            return res;
        }

        public IEnumerable<TimeKeeperEmployee> GetTimeKeepings()
        {
            var tkQuerry = from T in _TimeKeepingRepository.GetAll()
                           join E in _EmployeeRepository.GetAll() on T.EmployeeId equals E.Id
                           select new TimeKeeperEmployee{
                               Id = T.Id,
                               TimeAttendance = T.TimeAttendance,
                               Note = T.Note,
                               EmployeeDetail = E,
                               IsDeleted = T.IsDeleted,
            };
            var tkes = tkQuerry.ToList();
            return tkes;
        }

        public ResponseModel UpdateTimeKeeping(int TimeKeepingId, TimeKeeping updatedTimeKeeping)
        {
            if (!_TimeKeepingRepository.IsExists(TimeKeepingId)) return new ResponseModel(404, "Not found");
            if (!_TimeKeepingRepository.Update(updatedTimeKeeping))
            {
                return new ResponseModel(500, "Something went wrong updating TimeKeeping");
            }
            return new ResponseModel(204, "");
        }
        public ResponseModel UpdateDeleteStatus(int TimeKeepingId, bool status)
        {
            if (!_TimeKeepingRepository.IsExists(TimeKeepingId)) return new ResponseModel(404, "Not found");
            var updatedTimeKeeping = _TimeKeepingRepository.GetById(TimeKeepingId);
            updatedTimeKeeping.IsDeleted = status;
            if (!_TimeKeepingRepository.Update(updatedTimeKeeping))
            {
                return new ResponseModel(500, "Something went wrong when change delete status TimeKeeping");
            }
            return new ResponseModel(204, "");
        }

        public IEnumerable<TimeKeeperEmployee> Search(string field, string keyWords)
        {
            if (keyWords == "null") return GetTimeKeepings();
            var res = _TimeKeepingRepository.Search(field, keyWords);
            var resTkes = new List<TimeKeeperEmployee>();
            foreach ( var T in res)
            {
                resTkes.Add(new TimeKeeperEmployee()
                {
                    Id = T.Id,
                    TimeAttendance = T.TimeAttendance,
                    Note = T.Note,
                    EmployeeDetail = _EmployeeRepository.GetById(T.EmployeeId),
                    IsDeleted = T.IsDeleted,
                });
            }

            if (resTkes == null) return new List<TimeKeeperEmployee>();
            return resTkes;
        }

    }
}
