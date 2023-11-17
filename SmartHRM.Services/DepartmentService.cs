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
    public class DepartmentService
    {
        private readonly DepartmentRepository _departmentRepository;
        private readonly EmployeeRepository _EmployeeRepository;
        public DepartmentService(DepartmentRepository departmentRepository, EmployeeRepository employeeRepository)
        {
            _departmentRepository = departmentRepository;
            _EmployeeRepository = employeeRepository;
        }
        public ResponseModel CreateDepartment(Department DepartmentCreate)
        {
            var Departments = _departmentRepository.GetAll()
                            .Where(l => l.Name.Trim().ToLower() == DepartmentCreate.Name.Trim().ToLower())
                            .FirstOrDefault();
            if (Departments != null)
            {
                return new ResponseModel(422, "Department already exists");
            }

            if (!_departmentRepository.Create(DepartmentCreate))
            {
                return new ResponseModel(500, "Something went wrong while saving");
            }

            return new ResponseModel(201, "Successfully created");
        }

        public ResponseModel DeleteDepartment(int DepartmentId)
        {
            if (!_departmentRepository.IsExists(DepartmentId)) return new ResponseModel(404, "Not found");
            var DepartmentToDelete = _departmentRepository.GetById(DepartmentId);
            if (!_departmentRepository.Delete(DepartmentToDelete))
            {
                return new ResponseModel(500, "Something went wrong when deleting Department");
            }
            return new ResponseModel(204, "");
        }

        public Department? GetDepartment(int DepartmentId)
        {
            if (!_departmentRepository.IsExists(DepartmentId)) return null;
            var Department = _departmentRepository.GetById(DepartmentId);
            return Department;
        }
        
        
        public IEnumerable<DepartmentDto> GetDepartments()
        {
            var departmentQuerry = from D in _departmentRepository.GetAll()
                      join E in _EmployeeRepository.GetAll() on D.ManagerId equals E.Id
                      select new DepartmentDto
                      {
                          Id = D.Id,
                          Name = D.Name,
                          Description = D.Description,
                          Manager = E,
                          IsDeleted = D.IsDeleted,
                      };
            var dtos = departmentQuerry.ToList();
            return dtos;
        }

        public ResponseModel UpdateDepartment(int DepartmentId, Department updatedDepartment)
        {
            if (!_departmentRepository.IsExists(DepartmentId)) return new ResponseModel(404, "Not found");
          
            if (!_departmentRepository.Update(updatedDepartment))
            {
                return new ResponseModel(500, "Something went wrong updating Department");
            }
            return new ResponseModel(204, "");
        }
        public ResponseModel UpdateDeleteStatus(int departmentId, bool status)
        {
            if (!_departmentRepository.IsExists(departmentId)) return new ResponseModel(404, "Not found");
            var updatedPosition = _departmentRepository.GetById(departmentId);
            updatedPosition.IsDeleted = status;
            if (!_departmentRepository.Update(updatedPosition))
            {
                return new ResponseModel(500, "Something went wrong when change delete status Department");
            }
            return new ResponseModel(204, "");
        }

        public IEnumerable<DepartmentDto> Search(string field, string keyWords)
        {
            if (keyWords == "null") return GetDepartments();
            var res = _departmentRepository.Search(field, keyWords);
            var resDto = new List<DepartmentDto>();
            foreach ( var D in res)
            {
                resDto.Add(new DepartmentDto()
                {

                    Id = D.Id,
                    Name = D.Name,
                    Description = D.Description,
                    Manager = _EmployeeRepository.GetById(D.ManagerId),
                    IsDeleted = D.IsDeleted
                });
            }
            if (resDto == null) return new List<DepartmentDto>();
            return resDto;
        }

    }
}
