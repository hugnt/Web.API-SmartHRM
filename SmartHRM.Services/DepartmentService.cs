using HUG.CRUD.Services;
using SmartHRM.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHRM.Services
{
    public class DepartmentService
    {
        private readonly DepartmentRepository _DepartmentRepository;
        public DepartmentService(DepartmentRepository DepartmentRepository)
        {
            _DepartmentRepository = DepartmentRepository;
        }
        public ResponseModel CreateRole(Department DepartmentCreate)
        {
            var Roles = _DepartmentRepository.GetAll()
                            .Where(l => l.Name.Trim().ToLower() == DepartmentCreate.Name.Trim().ToLower())
                            .FirstOrDefault();
            if (Roles != null)
            {
                return new ResponseModel(422, "Department already exists");
            }

            if (!_DepartmentRepository.Create(DepartmentCreate))
            {
                return new ResponseModel(500, "Something went wrong while saving");
            }

            return new ResponseModel(201, "Successfully created");
        }

        public ResponseModel DeleteRole(int RoleId)
        {
            if (!_DepartmentRepository.IsExists(RoleId)) return new ResponseModel(404, "Not found");
            var RoleToDelete = _DepartmentRepository.GetById(RoleId);
            if (!_DepartmentRepository.Delete(RoleToDelete))
            {
                return new ResponseModel(500, "Something went wrong when deleting Department");
            }
            return new ResponseModel(204, "");
        }

        public Department? GetRole(int RoleId)
        {
            if (!_DepartmentRepository.IsExists(RoleId)) return null;
            var Department = _DepartmentRepository.GetById(RoleId);
            return Department;
        }

        public IEnumerable<Department> GetRoles()
        {
            return _DepartmentRepository.GetAll();
        }

        public ResponseModel UpdateRole(int RoleId, Department updatedRole)
        {
            if (!_DepartmentRepository.IsExists(RoleId)) return new ResponseModel(404, "Not found");

            if (!_DepartmentRepository.Update(updatedRole))
            {
                return new ResponseModel(500, "Something went wrong updating Department");
            }
            return new ResponseModel(204, "");
        }


    }
}