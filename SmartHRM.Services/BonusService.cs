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
        public readonly EmployeeRepository _EmployeeRepository;
        public readonly BonusDetailsRepository _BonusDetailsRepository;
        public BonusService(BonusRepository BonusRepository, EmployeeRepository EmployeeRepository, BonusDetailsRepository BonusDetails)
        {
            _BonusRepository = BonusRepository;
            _EmployeeRepository = EmployeeRepository;
            _BonusDetailsRepository = BonusDetails;
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
        public int GetInsuranceTotal()
        {
            return _BonusRepository.GetAll().Where(x => x.IsDeleted == false).Count();
        }

        //Statistic 
        public object GetStatisticUsedInsurance()
        {
            var ActiveCount = _BonusRepository.GetAll().Where(x => x.IsActive == true && x.IsDeleted == false).Count();
            var InactiveCount = _BonusRepository.GetAll().Where(x => x.IsActive == false && x.IsDeleted == false).Count();
            return new
            {
                Active = ActiveCount,
                Inactive = InactiveCount
            };
        }

        //Get top / list
        public IEnumerable<Bonus> GetTopID(int limit)
        {
            var res = _BonusRepository.GetAll().Where(x => x.IsDeleted == false).OrderByDescending(x => x.Id).Take(limit);
            return res;
        }
        //Số lượng Bonus
        public int GetTotal()
        {
            return _BonusRepository.GetAll().Where(x => x.IsDeleted == false).Count();
        }

        //Statistic 
        public decimal? GetStatisticMonth()
        {
            var totalAmount = _BonusRepository.GetAll()
        .Join(
        _BonusDetailsRepository.GetAll(),
        Bonus => Bonus.Id,
        detail => detail.BonusId,
        (Bonus, detail) => new { Bonus = Bonus, Detail = detail }
        )
        .Where(x => x.Detail.StartAt.Month == 11)
        .Sum(x => x.Bonus.Amount);
            return totalAmount;
        }

        //Get top / list
        public IEnumerable<Bonus> GetTopBonusHighest(int limit)
        {
            var res = _BonusRepository.GetAll().Where(x => x.IsDeleted == false).OrderByDescending(x => x.Amount).Take(limit);
            return res;
        }
    }
}
