using HUG.CRUD.Services;
using SmartHRM.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHRM.Services
{
    public class RoleService 
    {
        private readonly RoleRepository _RoleRepository;
        public RoleService(RoleRepository RoleRepository)
        {
            _RoleRepository = RoleRepository;
        }
        public ResponseModel CreateRole(Role RoleCreate)
        {
            var Roles = _RoleRepository.GetAll()
                            .Where(l =>
                                    l.Name.Trim().ToLower() == RoleCreate.Name.Trim().ToLower())
                            .FirstOrDefault();
            if (Roles != null)
            {
                return new ResponseModel(422, "Role already exists");
            }

            if (!_RoleRepository.Create(RoleCreate))
            {
                return new ResponseModel(500, "Something went wrong while saving");
            }

            return new ResponseModel(201, "Successfully created");
        }

        public ResponseModel DeleteRole(int RoleId)
        {
            if (!_RoleRepository.IsExists(RoleId)) return new ResponseModel(404, "Not found");
            var RoleToDelete = _RoleRepository.GetById(RoleId);
            if (!_RoleRepository.Delete(RoleToDelete))
            {
                return new ResponseModel(500, "Something went wrong when deleting Role");
            }
            return new ResponseModel(204, "");
        }

        public Role? GetRole(int RoleId)
        {
            if (!_RoleRepository.IsExists(RoleId)) return null;
            var Role = _RoleRepository.GetById(RoleId);
            return Role;
        }

        public IEnumerable<Role> GetRoles()
        {
            return _RoleRepository.GetAll();
        }

        public ResponseModel UpdateRole(int RoleId, Role updatedRole)
        {
            if (!_RoleRepository.IsExists(RoleId)) return new ResponseModel(404, "Not found");
            if (!_RoleRepository.Update(updatedRole))
            {
                return new ResponseModel(500, "Something went wrong updating Role");
            }
            return new ResponseModel(204, "");
        }

        public ResponseModel UpdateDeleteStatus(int RoleId, bool status)
        {
            if (!_RoleRepository.IsExists(RoleId)) return new ResponseModel(404, "Not found");
            var updatedRole = _RoleRepository.GetById(RoleId);
            updatedRole.IsDeleted = status;
            if (!_RoleRepository.Update(updatedRole))
            {
                return new ResponseModel(500, "Something went wrong when change delete status Role");
            }
            return new ResponseModel(204, "");
        }

        public IEnumerable<Role> Search(string field, string keyWords)
        {
            if (keyWords == "null") return _RoleRepository.GetAll();
            var res  = _RoleRepository.Search(field, keyWords);
            if(res==null) return new List<Role>();
            return res;
        }

        public int GetTotal()
        {
            return _RoleRepository.GetAll().Where(x => x.IsDeleted == false).Count();
        }
    }
}
