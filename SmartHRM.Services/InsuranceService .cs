using HUG.CRUD.Services;
using SmartHRM.Repository;
using SmartHRM.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHRM.Services
{
    public class IsuranceServise
    {
        private readonly IsuranceRepository _IsuranceRepository;
        public IsuranceServise(IsuranceRepository IsuranceRepository)
        {
            _IsuranceRepository = IsuranceRepository;
        }
        public ResponseModel CreateRole(Isurance IsuranceCreate)
        {
            var Roles = _IsuranceRepository.GetAll()
                            .Where(l => l.Name.Trim().ToLower() == IsuranceCreate.Name.Trim().ToLower())
                            .FirstOrDefault();
            if (Roles != null)
            {
                return new ResponseModel(422, "Isurance already exists");
            }

            if (!_IsuranceRepository.Create(IsuranceCreate))
            {
                return new ResponseModel(500, "Something went wrong while saving");
            }

            return new ResponseModel(201, "Successfully created");
        }

        public ResponseModel DeleteRole(int RoleId)
        {
            if (!_IsuranceRepository.IsExists(RoleId)) return new ResponseModel(404, "Not found");
            var RoleToDelete = _IsuranceRepository.GetById(RoleId);
            if (!_IsuranceRepository.Delete(RoleToDelete))
            {
                return new ResponseModel(500, "Something went wrong when deleting Isurance");
            }
            return new ResponseModel(204, "");
        }

        public Isurance? GetRole(int RoleId)
        {
            if (!_IsuranceRepository.IsExists(RoleId)) return null;
            var Isurance = _IsuranceRepository.GetById(RoleId);
            return Isurance;
        }

        public IEnumerable<Isurance> GetRoles()
        {
            return _IsuranceRepository.GetAll();
        }

        public ResponseModel UpdateRole(int RoleId, Isurance updatedRole)
        {
            if (!_IsuranceRepository.IsExists(RoleId)) return new ResponseModel(404, "Not found");
          
            if (!_IsuranceRepository.Update(updatedRole))
            {
                return new ResponseModel(500, "Something went wrong updating Isurance");
            }
            return new ResponseModel(204, "");
        }

   
    }
}
