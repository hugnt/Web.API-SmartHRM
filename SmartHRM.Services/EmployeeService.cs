using HUG.CRUD.Services;
using SmartHRM.Repository;
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
        public EmployeeService(EmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
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

        public Employee? GetEmployee(int employeeId)
        {
            if (!_employeeRepository.IsExists(employeeId)) return null;
            var employee = _employeeRepository.GetById(employeeId);
            return employee;
        }

        public IEnumerable<Employee> GetEmployees()
        {
            return _employeeRepository.GetAll();
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

        //Get total record
        public int GetTotal()
        {
            return _employeeRepository.GetAll().Where(x => x.IsDeleted == false).Count();
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
            return res;
        }

    }
}
