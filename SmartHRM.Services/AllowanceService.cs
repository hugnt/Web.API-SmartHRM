using HUG.CRUD.Services;
using SmartHRM.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHRM.Services
{
    public class AllowanceService
    {
        private readonly AllowanceRepository _AllowanceRepository;
        public AllowanceService(AllowanceRepository AllowanceRepository)
        {
            _AllowanceRepository = AllowanceRepository;
        }
        public ResponseModel CreateAllowance(Allowance AllowanceCreate)
        {
            var Allowances = _AllowanceRepository.GetAll()
                            .Where(l => l.Name.Trim().ToLower() == AllowanceCreate.Name.Trim().ToLower())
                            .FirstOrDefault();
            if (Allowances != null)
            {
                return new ResponseModel(422, "Allowance already exists");
            }

            if (!_AllowanceRepository.Create(AllowanceCreate))
            {
                return new ResponseModel(500, "Something went wrong while saving");
            }

            return new ResponseModel(201, "Successfully created");
        }

        public ResponseModel DeleteAllowance(int AllowanceId)
        {
            if (!_AllowanceRepository.IsExists(AllowanceId)) return new ResponseModel(404, "Not found");
            var AllowanceToDelete = _AllowanceRepository.GetById(AllowanceId);
            if (!_AllowanceRepository.Delete(AllowanceToDelete))
            {
                return new ResponseModel(500, "Something went wrong when deleting Allowance");
            }
            return new ResponseModel(204, "");
        }

        public Allowance? GetAllowance(int AllowanceId)
        {
            if (!_AllowanceRepository.IsExists(AllowanceId)) return null;
            var Allowance = _AllowanceRepository.GetById(AllowanceId);
            return Allowance;
        }

        public IEnumerable<Allowance> GetAllowances()
        {
            return _AllowanceRepository.GetAll();
        }

        public ResponseModel UpdateAllowance(int AllowanceId, Allowance updatedAllowance)
        {
            if (!_AllowanceRepository.IsExists(AllowanceId)) return new ResponseModel(404, "Not found");
            if (!_AllowanceRepository.Update(updatedAllowance))
            {
                return new ResponseModel(500, "Something went wrong updating Allowance");
            }
            return new ResponseModel(204, "");
        }
        public ResponseModel UpdateDeleteStatus(int BonusId, bool status)
        {
            if (!_AllowanceRepository.IsExists(BonusId)) return new ResponseModel(404, "Not found");
            var updatedBonus = _AllowanceRepository.GetById(BonusId);
            updatedBonus.IsDeleted = status;
            if (!_AllowanceRepository.Update(updatedBonus))
            {
                return new ResponseModel(500, "Something went wrong when change delete status Allowance");
            }
            return new ResponseModel(204, "");
        }

        public IEnumerable<Allowance> Search(string field, string keyWords)
        {
            if (keyWords == "null") return _AllowanceRepository.GetAll();
            var res = _AllowanceRepository.Search(field, keyWords);
            if (res == null) return new List<Allowance>();
            return res;
        }
    }
}
