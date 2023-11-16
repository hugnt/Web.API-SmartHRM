using HUG.CRUD.Services;
using SmartHRM.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHRM.Services
{
    public class PositionService
    {
        private readonly PositionRepository _PositionRepository;
        public PositionService(PositionRepository PositionRepository)
        {
            _PositionRepository = PositionRepository;
        }
        public ResponseModel CreatePosition(Position PositionCreate)
        {
            var Positions = _PositionRepository.GetAll()
                            .Where(l => l.Name.Trim().ToLower() == PositionCreate.Name.Trim().ToLower())
                            .FirstOrDefault();
            if (Positions != null)
            {
                return new ResponseModel(422, "Position already exists");
            }

            if (!_PositionRepository.Create(PositionCreate))
            {
                return new ResponseModel(500, "Something went wrong while saving");
            }

            return new ResponseModel(201, "Successfully created");
        }

        public ResponseModel DeletePosition(int PositionId)
        {
            if (!_PositionRepository.IsExists(PositionId)) return new ResponseModel(404, "Not found");
            var PositionToDelete = _PositionRepository.GetById(PositionId);
            if (!_PositionRepository.Delete(PositionToDelete))
            {
                return new ResponseModel(500, "Something went wrong when deleting Position");
            }
            return new ResponseModel(204, "");
        }

        public Position? GetPosition(int PositionId)
        {
            if (!_PositionRepository.IsExists(PositionId)) return null;
            var Position = _PositionRepository.GetById(PositionId);
            return Position;
        }

        public IEnumerable<Position> GetPositions()
        {
            return _PositionRepository.GetAll();
        }

        public ResponseModel UpdatePosition(int PositionId, Position updatedPosition)
        {
            if (!_PositionRepository.IsExists(PositionId)) return new ResponseModel(404, "Not found");
            if (!_PositionRepository.Update(updatedPosition))
            {
                return new ResponseModel(500, "Something went wrong updating Position");
            }
            return new ResponseModel(204, "");
        }

        public Role GetRoleById(int roleId)
        {
            throw new NotImplementedException();
        }

        public List<Role> GetRoles()
        {
            throw new NotImplementedException();
        }
        public Task<ResponseModel> ValidatePosition(Position Position)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseModel> ValidateNameAndPassword(Position Position)
        {
            throw new NotImplementedException();
        }
        public ResponseModel UpdateDeleteStatus(int positionId, bool status)
        {
            if (!_PositionRepository.IsExists(positionId)) return new ResponseModel(404, "Not found");
            var updatedPosition = _PositionRepository.GetById(positionId);
            updatedPosition.IsDeleted = status;
            if (!_PositionRepository.Update(updatedPosition))
            {
                return new ResponseModel(500, "Something went wrong when change delete status position");
            }
            return new ResponseModel(204, "");
        }

        public IEnumerable<Position> Search(string field, string keyWords)
        {
            if (keyWords == "null") return _PositionRepository.GetAll();
            var res = _PositionRepository.Search(field, keyWords);
            if (res == null) return new List<Position>();
            return res;
        }
    }
}
