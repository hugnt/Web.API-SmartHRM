using HUG.CRUD.Services;
using SmartHRM.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHRM.Services
{
    public class TimeKeepingService 
    {
        private readonly TimeKeepingRepository _TimeKeepingRepository;
        public TimeKeepingService(TimeKeepingRepository TimeKeepingRepository)
        {
            _TimeKeepingRepository = TimeKeepingRepository;
        }
        public ResponseModel CreateTimeKeeping(TimeKeeping TimeKeepingCreate)
        {
            var TimeKeepings = _TimeKeepingRepository.GetAll()
                            .Where(l => l.EmployeeId == TimeKeepingCreate.EmployeeId)
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

        public TimeKeeping? GetTimeKeeping(int TimeKeepingId)
        {
            if (!_TimeKeepingRepository.IsExists(TimeKeepingId)) return null;
            var TimeKeeping = _TimeKeepingRepository.GetById(TimeKeepingId);
            return TimeKeeping;
        }

        public IEnumerable<TimeKeeping> GetTimeKeepings()
        {
            return _TimeKeepingRepository.GetAll();
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

    }
}
