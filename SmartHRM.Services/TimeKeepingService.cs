using HUG.CRUD.Services;
using SmartHRM.Repository;
using SmartHRM.Services.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.WebSockets;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace SmartHRM.Services
{
    public class TimeKeepingService 
    {
        private readonly TimeKeepingRepository _TimeKeepingRepository;
        private readonly EmployeeRepository _EmployeeRepository;
        public TimeOnly startTime = new TimeOnly(7, 0, 0);
        public TimeOnly endTime = new TimeOnly(17, 0, 0);
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

        public int getWeek(DateTime? _date)
        {
            var date = _TimeKeepingRepository.GetAll().FirstOrDefault(x => x.TimeAttendance == _date);

            CultureInfo culture = CultureInfo.CurrentCulture;
            CalendarWeekRule weekRule = culture.DateTimeFormat.CalendarWeekRule;
            DayOfWeek firstWeekDay = culture.DateTimeFormat.FirstDayOfWeek;
            Calendar calendar = culture.Calendar;

            int week = calendar.GetWeekOfYear(date.TimeAttendance, weekRule, firstWeekDay);
            return week;
        }
        
        public TimeOnly getTime(DateTime _date)
        {
            TimeOnly _time = TimeOnly.FromDateTime(_date);
            
            return _time;
        }

        //number of employee work from 7am to 5pm by week
        public object GetNumberOnTimeEmployee(int _date)
        { 
            return GetTimeKeepings().Where(x => 
            getTime(x.TimeAttendance) <= startTime 
                && x.IsDeleted == false
                && getWeek(x.TimeAttendance) == _date).Count();

        }

        //list ontime employee by week
        public object GetListOnTime(int _date)
        {
            DateTime day = new DateTime(2023,1,1);
            var mon = GetTimeKeepings().Where(x=>
                x.IsDeleted == false
                && getTime(x.TimeAttendance) <= startTime
                && x.TimeAttendance.Date == day.AddDays((_date-1)*7+1)
            ).Count();
            var tue = GetTimeKeepings().Where(x =>
                x.IsDeleted == false
                && getTime(x.TimeAttendance) <= startTime
                && x.TimeAttendance.Date == day.Date.AddDays((_date - 1) * 7 + 2)
            ).Count();
            var wed = GetTimeKeepings().Where(x =>
                x.IsDeleted == false
                && getTime(x.TimeAttendance) <= startTime
                && x.TimeAttendance.Date == day.Date.AddDays((_date - 1) * 7 + 3)
            ).Count();
            var thu = GetTimeKeepings().Where(x =>
                 x.IsDeleted == false
                && getTime(x.TimeAttendance) <= startTime
                && x.TimeAttendance.Date == day.Date.AddDays((_date - 1) * 7 + 4)
            ).Count();
            var fri = GetTimeKeepings().Where(x =>
                x.IsDeleted == false
                && getTime(x.TimeAttendance) <= startTime
                && x.TimeAttendance.Date == day.Date.AddDays((_date - 1) * 7 + 5)
            ).Count();
            var sat = GetTimeKeepings().Where(x =>
                x.IsDeleted == false
                && getTime(x.TimeAttendance) <= startTime
                && x.TimeAttendance.Date == day.Date.AddDays((_date - 1) * 7 + 6)
            ).Count();
            var sun = GetTimeKeepings().Where(x =>
                x.IsDeleted == false
                && getTime(x.TimeAttendance) <= startTime
                && x.TimeAttendance.Date == day.Date.AddDays((_date - 1) * 7 + 7)
            ).Count();
            return new
            {
                Monday = mon,
                Tuesday = tue,
                Wednesday = wed,
                Thursday = thu,
                Friday = fri,
                Saturday = sat,
                Sunday = sun
            };
        }

        //top late
        public object GetTopLateTimeEmployee(int limit)
        {
            var res = GetTimeKeepings().Where(x=>
            x.IsDeleted == false &&
            getTime(x.TimeAttendance) >= startTime).
            OrderByDescending(x=>getTime(x.TimeAttendance)).Take(limit);
            return res;
        }
        //top usually late
        public object GetListUsuallyLate(int limit)
        {
            var resQuery = from E in _EmployeeRepository.GetAll()
                           join T in _TimeKeepingRepository.GetAll()
                           on E.Id equals T.EmployeeId
                           where getTime(T.TimeAttendance) >= startTime && T.IsDeleted == false
                           group E by E.FullName into eGroup
                           where eGroup.Count() > 1 
                           select new
                           {
                               EmployeeName = eGroup.Key,
                               Times = eGroup.Count(),
                               
                           };
            var res = resQuery.ToList();
            return res.OrderByDescending(x=>x.Times).Take(limit);
        }

        //number of employee late by id by week
        public int GetNumberLate(int id, int _date)
        {
            int res = GetTimeKeepings().Where(x=>
            x.IsDeleted==false 
            && x.Id == id
            && getWeek(x.TimeAttendance) ==_date
            && getTime(x.TimeAttendance) >= startTime).Count();
            return res; 
        }

        //number of employee haven't work
        public int GetNumberEmployeeNoWork(int _date)
        {
            var resQuery = from E in _EmployeeRepository.GetAll()
                           join T in _TimeKeepingRepository.GetAll()
                           on new { Id = E.Id, Time = _date }
                           equals new {Id = T.EmployeeId, Time = getWeek(T.TimeAttendance) }    
                           into timeKeepings
                           from subTimeKeeping in timeKeepings.DefaultIfEmpty()
                                where subTimeKeeping == null 
                                select new {
                Name = E.FullName, isDelete = E.IsDeleted
            };
            int res = resQuery.Count();
            return res;
        }
    }
}
