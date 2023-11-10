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
    public class InsuranceService
    {
        private readonly InsuranceRepository _InsuranceRepository;
        public InsuranceService(InsuranceRepository InsuranceRepository)
        {
            _InsuranceRepository = InsuranceRepository;
        }
        public ResponseModel CreateInsurance(Insurance InsuranceCreate)
        {
            var Insurances = _InsuranceRepository.GetAll()
                            .Where(l => l.Name.Trim().ToLower() == InsuranceCreate.Name.Trim().ToLower())
                            .FirstOrDefault();
            if (Insurances != null)
            {
                return new ResponseModel(422, "Insurance already exists");
            }

            if (!_InsuranceRepository.Create(InsuranceCreate))
            {
                return new ResponseModel(500, "Something went wrong while saving");
            }

            return new ResponseModel(201, "Successfully created");
        }

        public ResponseModel DeleteInsurance(int InsuranceId)
        {
            if (!_InsuranceRepository.IsExists(InsuranceId)) return new ResponseModel(404, "Not found");
            var InsuranceToDelete = _InsuranceRepository.GetById(InsuranceId);
            if (!_InsuranceRepository.Delete(InsuranceToDelete))
            {
                return new ResponseModel(500, "Something went wrong when deleting Insurance");
            }
            return new ResponseModel(204, "");
        }

        public Insurance? GetInsurance(int InsuranceId)
        {
            if (!_InsuranceRepository.IsExists(InsuranceId)) return null;
            var Insurance = _InsuranceRepository.GetById(InsuranceId);
            return Insurance;
        }

        public IEnumerable<Insurance> GetInsurances()
        {
            return _InsuranceRepository.GetAll();
        }

        public ResponseModel UpdateInsurance(int InsuranceId, Insurance updatedInsurance)
        {
            if (!_InsuranceRepository.IsExists(InsuranceId)) return new ResponseModel(404, "Not found");
            if (!_InsuranceRepository.Update(updatedInsurance))
            {
                return new ResponseModel(500, "Something went wrong updating Insurance");
            }
            return new ResponseModel(204, "");
        }

        public Insurance GetInsuranceById(int InsuranceId)
        {
            throw new NotImplementedException();
        }

        public List<Insurance> GetInsurances()
        {
            throw new NotImplementedException();
        }
        public Task<ResponseModel> ValidateInsurance(Insurance Insurance)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseModel> ValidateNameAndPassword(Insurance Insurance)
        {
            throw new NotImplementedException();
        }
    }
}
