using HUG.CRUD.Services;
using SmartHRM.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace SmartHRM.Services
{
    public class DeductionService
    {
        private readonly DeductionRepository _DeductionRepository;
        public readonly EmployeeRepository _EmployeeRepository;
        public readonly DeductionDetailsRepository _DeductionDetailsRepository;
        public DeductionService(DeductionRepository DeductionRepository, EmployeeRepository EmployeeRepository, DeductionDetailsRepository DeductionDetails)
        {
            _DeductionRepository = DeductionRepository;
            _EmployeeRepository = EmployeeRepository;
            _DeductionDetailsRepository = DeductionDetails;
        }
        public ResponseModel CreateDeduction(Deduction DeductionCreate)
        {
            var Deductions = _DeductionRepository.GetAll()
                            .Where(l => l.Name.Trim().ToLower() == DeductionCreate.Name.Trim().ToLower())
                            .FirstOrDefault();
            if (Deductions != null)
            {
                return new ResponseModel(422, "Deduction already exists");
            }

            if (!_DeductionRepository.Create(DeductionCreate))
            {
                return new ResponseModel(500, "Something went wrong while saving");
            }

            return new ResponseModel(201, "Successfully created");
        }

        public ResponseModel DeleteDeduction(int DeductionId)
        {
            if (!_DeductionRepository.IsExists(DeductionId)) return new ResponseModel(404, "Not found");
            var DeductionToDelete = _DeductionRepository.GetById(DeductionId);
            if (!_DeductionRepository.Delete(DeductionToDelete))
            {
                return new ResponseModel(500, "Something went wrong when deleting Deduction");
            }
            return new ResponseModel(204, "");
        }

        public Deduction? GetDeduction(int DeductionId)
        {
            if (!_DeductionRepository.IsExists(DeductionId)) return null;
            var Deduction = _DeductionRepository.GetById(DeductionId);
            return Deduction;
        }

        public IEnumerable<Deduction> GetDeductions()
        {
            return _DeductionRepository.GetAll();
        }

        public ResponseModel UpdateDeduction(int DeductionId, Deduction updatedDeduction)
        {
            if (!_DeductionRepository.IsExists(DeductionId)) return new ResponseModel(404, "Not found");
            if (!_DeductionRepository.Update(updatedDeduction))
            {
                return new ResponseModel(500, "Something went wrong updating Deduction");
            }
            return new ResponseModel(204, "");
        }

        public ResponseModel UpdateDeleteStatus(int DeductionId, bool status)
        {
            if (!_DeductionRepository.IsExists(DeductionId)) return new ResponseModel(404, "Not found");
            var updatedDeduction = _DeductionRepository.GetById(DeductionId);
            updatedDeduction.IsDeleted = status;
            if (!_DeductionRepository.Update(updatedDeduction))
            {
                return new ResponseModel(500, "Something went wrong when change delete status Deduction");
            }
            return new ResponseModel(204, "");
        }

        public IEnumerable<Deduction> Search(string field, string keyWords)
        {
            if (keyWords == "null") return _DeductionRepository.GetAll();
            var res = _DeductionRepository.Search(field, keyWords);
            if (res == null) return new List<Deduction>();
            return res;
        }
        public int GetInsuranceTotal()
        {
            return _DeductionRepository.GetAll().Where(x => x.IsDeleted == false).Count();
        }

        //Statistic 
        public object GetStatisticUsedInsurance()
        {
            var ActiveCount = _DeductionRepository.GetAll().Where(x => x.IsActive == true && x.IsDeleted == false).Count();
            var InactiveCount = _DeductionRepository.GetAll().Where(x => x.IsActive == false && x.IsDeleted == false).Count();
            return new
            {
                Active = ActiveCount,
                Inactive = InactiveCount
            };
        }

        //Get top / list
        public IEnumerable<Deduction> GetTopID(int limit)
        {
            var res = _DeductionRepository.GetAll().Where(x => x.IsDeleted == false).OrderByDescending(x => x.Id).Take(limit);
            return res;
        }
        public int GetAmountDeduction()
        {
            return _DeductionRepository.GetAll().Where(x=>x.IsDeleted == false).Count();
        }
        public object GetTotalDeduction(int month)
        {
            var resQuery = (from D in _DeductionRepository.GetAll()
                            join D2 in _DeductionDetailsRepository.GetAll()
                            on D.Id equals D2.DeductionId
                            where D2.StartAt.Month == month
                            select D.Amount).Sum();
            decimal? res = resQuery;

            var totalDeduction = _DeductionRepository.GetAll()
                .Join(_DeductionDetailsRepository.GetAll(),
                D => D.Id, D2 => D2.DeductionId,
                (D, D2) => new { Deduction = D, Detail = D2 })
                .Where(x => x.Detail.StartAt.Month == month)
                .Sum(x => x.Deduction.Amount);

            return resQuery;
        }
        public decimal? GetTotalAmountDeduction()
        {
            decimal? res = 0;
            foreach(var item in _DeductionRepository.GetAll())
            {
                res += item.Amount;
            }
            return res;
        }
        public object GetTopDeduction(int limit)
        {
            var res = _DeductionRepository.GetAll().Where(x=>
            x.IsDeleted == false).OrderByDescending(x=>x.Amount).Take(limit);
            return res;
        }
        //Số lượng Deduction
        public int GetTotal()
        {
            return _DeductionRepository.GetAll().Where(x => x.IsDeleted == false).Count();
        }

        //Statistic 
        public decimal? GetStatisticMonth()
        {
            var totalAmount = _DeductionRepository.GetAll()
        .Join(
        _DeductionDetailsRepository.GetAll(),
        Deduction => Deduction.Id,
        detail => detail.DeductionId,
        (Deduction, detail) => new { Deduction = Deduction, Detail = detail }
        )
        .Where(x => x.Detail.StartAt.Month == 11)
        .Sum(x => x.Deduction.Amount);
            return totalAmount;
        }

        //Get top / list
        public IEnumerable<Deduction> GetTopDeductionHighest(int limit)
        {
            var res = _DeductionRepository.GetAll().Where(x => x.IsDeleted == false).OrderByDescending(x => x.Amount).Take(limit);
            return res;
        }
    }
}
