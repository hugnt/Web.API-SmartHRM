using HUG.CRUD.Services;
using SmartHRM.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHRM.Services
{
    public class BonusService
    {
        private readonly BonusRepository _BonusRepository;
        public BonusService(BonusRepository BonusRepository)
        {
            _BonusRepository = BonusRepository;
        }
        public ResponseModel CreateBonus(Bonus BonusCreate)
        {
            var Bonuss = _BonusRepository.GetAll()
                            .Where(l => l.Name.Trim().ToLower() == BonusCreate.Name.Trim().ToLower())
                            .FirstOrDefault();
            if (Bonuss != null)
            {
                return new ResponseModel(422, "Bonus already exists");
            }

            if (!_BonusRepository.Create(BonusCreate))
            {
                return new ResponseModel(500, "Something went wrong while saving");
            }

            return new ResponseModel(201, "Successfully created");
        }

        public ResponseModel DeleteBonus(int BonusId)
        {
            if (!_BonusRepository.IsExists(BonusId)) return new ResponseModel(404, "Not found");
            var BonusToDelete = _BonusRepository.GetById(BonusId);
            if (!_BonusRepository.Delete(BonusToDelete))
            {
                return new ResponseModel(500, "Something went wrong when deleting Bonus");
            }
            return new ResponseModel(204, "");
        }

        public Bonus? GetBonus(int BonusId)
        {
            if (!_BonusRepository.IsExists(BonusId)) return null;
            var Bonus = _BonusRepository.GetById(BonusId);
            return Bonus;
        }

        public IEnumerable<Bonus> GetBonuss()
        {
            return _BonusRepository.GetAll();
        }

        public ResponseModel UpdateBonus(int BonusId, Bonus updatedBonus)
        {
            if (!_BonusRepository.IsExists(BonusId)) return new ResponseModel(404, "Not found");
            if (!_BonusRepository.Update(updatedBonus))
            {
                return new ResponseModel(500, "Something went wrong updating Bonus");
            }
            return new ResponseModel(204, "");
        }

        public ResponseModel UpdateDeleteStatus(int BonusId, bool status)
        {
            if (!_BonusRepository.IsExists(BonusId)) return new ResponseModel(404, "Not found");
            var updatedBonus = _BonusRepository.GetById(BonusId);
            updatedBonus.IsDeleted = status;
            if (!_BonusRepository.Update(updatedBonus))
            {
                return new ResponseModel(500, "Something went wrong when change delete status Bonus");
            }
            return new ResponseModel(204, "");
        }

        public IEnumerable<Bonus> Search(string field, string keyWords)
        {
            if (keyWords == "null") return _BonusRepository.GetAll();
            var res = _BonusRepository.Search(field, keyWords);
            if (res == null) return new List<Bonus>();
            return res;
        }
    }
}
