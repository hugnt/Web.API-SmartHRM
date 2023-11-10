using HUG.CRUD.Services;
using SmartHRM.Repository;
using SmartHRM.Repository.Models;
using SmartHRM.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHRM.Services
{
    public class DepartmentServise
    {
        private readonly DepartmentRepository _DepartmentRepository;
        private readonly EmployeeRepository _EmployeeRepository;
        public DepartmentServise(DepartmentRepository departmentRepository, EmployeeRepository employeeRepository)
        {
            _DepartmentRepository = departmentRepository;
            _EmployeeRepository = employeeRepository;
        }
        public ResponseModel CreateDepartment(Department DepartmentCreate)
        {
            var Departments = _DepartmentRepository.GetAll()
                            .Where(l => l.Name.Trim().ToLower() == DepartmentCreate.Name.Trim().ToLower())
                            .FirstOrDefault();
            if (Departments != null)
            {
                return new ResponseModel(422, "Department already exists");
            }

            if (!_DepartmentRepository.Create(DepartmentCreate))
            {
                return new ResponseModel(500, "Something went wrong while saving");
            }

            return new ResponseModel(201, "Successfully created");
        }

        public ResponseModel DeleteDepartment(int DepartmentId)
        {
            if (!_DepartmentRepository.IsExists(DepartmentId)) return new ResponseModel(404, "Not found");
            var DepartmentToDelete = _DepartmentRepository.GetById(DepartmentId);
            if (!_DepartmentRepository.Delete(DepartmentToDelete))
            {
                return new ResponseModel(500, "Something went wrong when deleting Department");
            }
            return new ResponseModel(204, "");
        }

        public Department? GetDepartment(int DepartmentId)
        {
            if (!_DepartmentRepository.IsExists(DepartmentId)) return null;
            var Department = _DepartmentRepository.GetById(DepartmentId);
            return Department;
        }
        
        
        public IEnumerable<DepartmentDto> GetDepartments()
        {
            var departmentQuerry = from D in _DepartmentRepository.GetAll()
                      join E in _EmployeeRepository.GetAll() on D.ManagerId equals E.Id
                      select new DepartmentDto
                      {
                          Id = D.Id,
                          Name = D.Name,
                          Description = D.Description,
                          Manager = E
                      };
            var dtos = departmentQuerry.ToList();
            return dtos;
        }

        public ResponseModel UpdateDepartment(int DepartmentId, Department updatedDepartment)
        {
            if (!_DepartmentRepository.IsExists(DepartmentId)) return new ResponseModel(404, "Not found");
          
            if (!_DepartmentRepository.Update(updatedDepartment))
            {
                return new ResponseModel(500, "Something went wrong updating Department");
            }
            return new ResponseModel(204, "");
        }

   
    }
}
