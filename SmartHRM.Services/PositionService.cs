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
    public class PositionService
    {
        private readonly PositionRepository _positionService;
        public PositionService(PositionRepository PositionRepository)
        {
            _positionService = PositionRepository;
        }
        public ResponseModel CreatePosition(Position PositionCreate)
        {
            var Positions = _positionService.GetAll()
                            .Where(l => l.Name.Trim().ToLower() == PositionCreate.Name.Trim().ToLower())
                            .FirstOrDefault();
            if (Positions != null)
            {
                return new ResponseModel(422, "Position already exists");
            }

            if (!_positionService.Create(PositionCreate))
            {
                return new ResponseModel(500, "Something went wrong while saving");
            }

            return new ResponseModel(201, "Successfully created");
        }

        public ResponseModel DeletePosition(int PositionId)
        {
            if (!_positionService.IsExists(PositionId)) return new ResponseModel(404, "Not found");
            var PositionToDelete = _positionService.GetById(PositionId);
            if (!_positionService.Delete(PositionToDelete))
            {
                return new ResponseModel(500, "Something went wrong when deleting Position");
            }
            return new ResponseModel(204, "");
        }

        public Position? GetPosition(int PositionId)
        {
            if (!_positionService.IsExists(PositionId)) return null;
            var position = _positionService.GetById(PositionId);
            return position;
        }

        public IEnumerable<Position> GetPositions()
        {
            return _positionService.GetAll();
        }

        public ResponseModel UpdatePosition(int PositionId, Position updatedPosition)
        {
            if (!_positionService.IsExists(PositionId)) return new ResponseModel(404, "Not found");
            if (!_positionService.Update(updatedPosition))
            {
                return new ResponseModel(500, "Something went wrong updating Position");
            }
            return new ResponseModel(204, "");
        }

        public Position GetPositionById(int PositionId)
        {
            throw new NotImplementedException();
        }

        public List<Position> GetPosition()
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
            if (!_positionService.IsExists(positionId)) return new ResponseModel(404, "Not found");
            var updatedPosition = _positionService.GetById(positionId);
            updatedPosition.IsDeleted = status;
            if (!_positionService.Update(updatedPosition))
            {
                return new ResponseModel(500, "Something went wrong when change delete status position");
            }
            return new ResponseModel(204, "");
        }

        public IEnumerable<Position> Search(string field, string keyWords)
        {
            if (keyWords == "null") return _positionService.GetAll();
            var res = _positionService.Search(field, keyWords);
            if (res == null) return new List<Position>();
            return res;
        }
    }
}
