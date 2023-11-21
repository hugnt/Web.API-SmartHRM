using HUG.CRUD.Services;
using SmartHRM.Repository;
using SmartHRM.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHRM.Services
{
    public class EmployeeService
    {
        private readonly EmployeeRepository _employeeRepository;
        private readonly PositionRepository _positionRepository;
        private readonly DepartmentRepository _departmentRepository;
        public EmployeeService(EmployeeRepository employeeRepository
                               ,DepartmentRepository departmentRepository
                               ,PositionRepository positionRepository)
        {
            _employeeRepository = employeeRepository;
            _positionRepository = positionRepository;
            _departmentRepository = departmentRepository;
        }
        public ResponseModel CreateEmployee(Employee employeeCreate)
        {
            var employees = _employeeRepository.GetAll()
                            .Where(l =>
                                    l.FullName.Trim().ToLower() == employeeCreate.FullName.Trim().ToLower()
                                    && l.IdentificationCard.Trim().ToLower() == employeeCreate.IdentificationCard.Trim().ToLower())
                            .FirstOrDefault();
            if (employees != null)
            {
                return new ResponseModel(422, "Employee already exists");
            }

            if (!_employeeRepository.Create(employeeCreate))
            {
                return new ResponseModel(500, "Something went wrong while saving");
            }

            return new ResponseModel(201, "Successfully created");
        }

        public ResponseModel DeleteEmployee(int employeeId)
        {
            if (!_employeeRepository.IsExists(employeeId)) return new ResponseModel(404, "Not found");
            var employeeToDelete = _employeeRepository.GetById(employeeId);
            if (!_employeeRepository.Delete(employeeToDelete))
            {
                return new ResponseModel(500, "Something went wrong when deleting employee");
            }
            return new ResponseModel(204, "");
        }

        public EmployeeDto? GetEmployee(int employeeId)
        {
            if (!_employeeRepository.IsExists(employeeId)) return null;
            var employee = GetEmployees().FirstOrDefault(x=>x.Id == employeeId);
            return employee;
        }

        public IEnumerable<EmployeeDto> GetEmployees()
        {
            var employeeQuerry = from e in _employeeRepository.GetAll()
                                 join p in _positionRepository.GetAll() on e.PositionId equals p.Id
                                 join d in _departmentRepository.GetAll() on e.DepartmentId equals d.Id
                                 select new EmployeeDto
                                 {
                                     Id = e.Id,
                                     FullName = e.FullName,
                                     PhoneNumber = e.PhoneNumber,
                                     Email = e.Email,
                                     IdentificationCard = e.IdentificationCard,
                                     Dob = e.Dob,
                                     Gender = e.Gender,
                                     Address = e.Address,
                                     Avatar = e.Avatar,
                                     PositionId = e.PositionId,
                                     Position = p.Name,
                                     DepartmentId = e.DepartmentId,
                                     Department = d.Name,
                                     IsDeleted = e.IsDeleted
                                 };
            return employeeQuerry.ToList();
        }

        public ResponseModel UpdateEmployee(int employeeId, Employee updatedEmployee)
        {
            if (!_employeeRepository.IsExists(employeeId)) return new ResponseModel(404, "Not found");
            if (!_employeeRepository.Update(updatedEmployee))
            {
                return new ResponseModel(500, "Something went wrong updating employee");
            }
            return new ResponseModel(204, "");
        }
      
        public ResponseModel UpdateDeleteStatus(int employeeId, bool status)
        {
            if (!_employeeRepository.IsExists(employeeId)) return new ResponseModel(404, "Not found");
            var updatedEmployee = _employeeRepository.GetById(employeeId);
            updatedEmployee.IsDeleted = status;
            if (!_employeeRepository.Update(updatedEmployee))
            {
                return new ResponseModel(500, "Something went wrong when change delete status employee");
            }
            return new ResponseModel(204, "");
        }

        

        //Get total record
        public int GetTotal()
        {
            return _employeeRepository.GetAll().Where(x=>x.IsDeleted==false).Count();
        }

        //Statistic 
        public object GetStatisticMaleFemale()
        {
            var maleCount = _employeeRepository.GetAll().Where(x => x.Gender == true && x.IsDeleted == false).Count();
            var femaleCount = _employeeRepository.GetAll().Where(x => x.Gender == false && x.IsDeleted == false).Count();
            return new
            {
                Male = maleCount,
                Female = femaleCount
            };
        }

        //Get top / list
        public IEnumerable<Employee> GetTopYoungest(int limit)
        {
            var res = _employeeRepository.GetAll().Where(x => x.IsDeleted == false).OrderByDescending(x => x.Dob).Take(limit);
            return res ;
        }
    }
}