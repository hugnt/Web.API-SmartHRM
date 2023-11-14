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
        public PositionService(PositionRepository positionRepository)
        {
            _positionRepository = positionRepository;
        }
        public ResponseModel CreatePosition(Position positionCreate)
        {
            var positions = _positionRepository.GetAll()
                            .Where(l =>
                                    l.Name.Trim().ToLower() == positionCreate.Name.Trim().ToLower())
                            .FirstOrDefault();
            if (positions != null)
            {
                return new ResponseModel(422, "Position already exists");
            }

            if (!_positionRepository.Create(positionCreate))
            {
                return new ResponseModel(500, "Something went wrong while saving");
            }

            return new ResponseModel(201, "Successfully created");
        }

        public ResponseModel DeletePosition(int positionId)
        {
            if (!_positionRepository.IsExists(positionId)) return new ResponseModel(404, "Not found");
            var positionToDelete = _positionRepository.GetById(positionId);
            if (!_positionRepository.Delete(positionToDelete))
            {
                return new ResponseModel(500, "Something went wrong when deleting position");
            }
            return new ResponseModel(204, "");
        }

        public Position? GetPosition(int positionId)
        {
            if (!_positionRepository.IsExists(positionId)) return null;
            var position = _positionRepository.GetById(positionId);
            return position;
        }

        public IEnumerable<Position> GetPositions()
        {
            return _positionRepository.GetAll();
        }

        public ResponseModel UpdatePosition(int positionId, Position updatedPosition)
        {
            if (!_positionRepository.IsExists(positionId)) return new ResponseModel(404, "Not found");
            if (!_positionRepository.Update(updatedPosition))
            {
                return new ResponseModel(500, "Something went wrong updating position");
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
            var res  = _positionRepository.Search(field, keyWords);
            if(res==null) return new List<Position>();
            return res;
        }
    }
}
