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
        private readonly PositionRepository _positionRepository;
        public PositionService(PositionRepository PositionRepository)
        {
            _positionRepository = PositionRepository;
        }
        public ResponseModel CreatePosition(Position PositionCreate)
        {
            var Positions = _positionRepository.GetAll()
                                .Where(l => l.Name.Trim().ToLower() == PositionCreate.Name.Trim().ToLower())
                                .FirstOrDefault();
            if (Positions != null)
            {
                return new ResponseModel(422, "Position already exists");
            }

            if (!_positionRepository.Create(PositionCreate))
            {
                return new ResponseModel(500, "Something went wrong while saving");
            }

            return new ResponseModel(201, "Successfully created");
        }

        public ResponseModel DeletePosition(int PositionId)
        {
            if (!_positionRepository.IsExists(PositionId)) return new ResponseModel(404, "Not found");
            var PositionToDelete = _positionRepository.GetById(PositionId);
            if (!_positionRepository.Delete(PositionToDelete))
            {
                return new ResponseModel(500, "Something went wrong when deleting Position");
            }
            return new ResponseModel(204, "");
        }

        public Position? GetPosition(int PositionId)
        {
            if (!_positionRepository.IsExists(PositionId)) return null;
            var Position = _positionRepository.GetById(PositionId);
            return Position;
        }

        public IEnumerable<Position> GetPositions()
        {
            return _positionRepository.GetAll();
        }

        public ResponseModel UpdatePosition(int PositionId, Position updatedPosition)
        {
            if (!_positionRepository.IsExists(PositionId)) return new ResponseModel(404, "Not found");
            if (!_positionRepository.Update(updatedPosition))
            {
                return new ResponseModel(500, "Something went wrong updating Position");
            }
            return new ResponseModel(204, "");
        }
        public ResponseModel UpdateDeleteStatus(int positionId, bool status)
        {
            if (!_positionRepository.IsExists(positionId)) return new ResponseModel(404, "Not found");
            var updatedPosition = _positionRepository.GetById(positionId);
            updatedPosition.IsDeleted = status;
            if (!_positionRepository.Update(updatedPosition))
            {
                return new ResponseModel(500, "Something went wrong when change delete status position");
            }
            return new ResponseModel(204, "");
        }

        public IEnumerable<Position> Search(string field, string keyWords)
        {
            if (keyWords == "null") return _positionRepository.GetAll();
            var res = _positionRepository.Search(field, keyWords);
            if (res == null) return new List<Position>();
            return res;
        }
    }
}
