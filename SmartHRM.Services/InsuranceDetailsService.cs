using HUG.CRUD.Services;
using SmartHRM.Repository;
using SmartHRM.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHRM.Services
{
    public class InsuranceDetailsService 
    {
        private readonly InsuranceRepository _insuranceRepository;
        private readonly EmployeeRepository _employeeRepository;
        private readonly InsuranceDetailsRepository _insuranceDetailsRepository;
        public InsuranceDetailsService(InsuranceRepository insuranceRepository,
                                EmployeeRepository employeeRepository,
                                InsuranceDetailsRepository insuranceDetailsRepository)
        {
            _insuranceRepository = insuranceRepository;
            _employeeRepository = employeeRepository;
            _insuranceDetailsRepository = insuranceDetailsRepository;
        }
        public ResponseModel CreateInsuranceDetails(InsuranceDetails insuranceDetailsCreate)
        {
            var insuranceDetailss = _insuranceDetailsRepository.IsExists(insuranceDetailsCreate);
            if (insuranceDetailss)
            {
                return new ResponseModel(422, "InsuranceDetails already exists");
            }

            if (!_insuranceDetailsRepository.Create(insuranceDetailsCreate))
            {
                return new ResponseModel(500, "Something went wrong while saving");
            }

            return new ResponseModel(201, "Successfully created");
        }

        public ResponseModel DeleteInsuranceDetails(int insuranceDetailsId)
        {
            if (!_insuranceDetailsRepository.IsExists(insuranceDetailsId)) return new ResponseModel(404, "Not found");
            var insuranceDetailsToDelete = _insuranceDetailsRepository.GetById(insuranceDetailsId);
            if (!_insuranceDetailsRepository.Delete(insuranceDetailsToDelete))
            {
                return new ResponseModel(500, "Something went wrong when deleting insuranceDetails");
            }
            return new ResponseModel(204, "");
        }

        public InsuranceDetails? GetInsuranceDetails(int insuranceDetailsId)
        {
            if (!_insuranceDetailsRepository.IsExists(insuranceDetailsId)) return null;
            var insuranceDetails = GetInsuranceDetailss().FirstOrDefault(x=> x.Id == insuranceDetailsId);
            return insuranceDetails;
        }

        public IEnumerable<InsuranceDetailsDto> GetInsuranceDetailss()
        {
            var queryInsuranceDetails = from e in _employeeRepository.GetAll()
                                        join ei in _insuranceDetailsRepository.GetAll() on e.Id equals ei.EmployeeId
                                        join i in _insuranceRepository.GetAll() on ei.InsuranceId equals i.Id
                                        select new InsuranceDetailsDto
                                        {
                                            Id = ei.Id,
                                            FullName = e.FullName,
                                            InsuranceName = i.Name,
                                            InsuranceAmount = i.Amount,
                                            InsuranceId = ei.InsuranceId,
                                            EmployeeId = ei.EmployeeId,
                                            ProvideAt = ei.ProvideAt,
                                            ExpriredAt = ei.ExpriredAt,
                                            ProviderAddress = ei.ProviderAddress,
                                            IsDeleted = ei.IsDeleted
                                        };
            return queryInsuranceDetails.ToList();
        }

        public ResponseModel UpdateInsuranceDetails(int insuranceDetailsId, InsuranceDetails updatedInsuranceDetails)
        {
            if (!_insuranceDetailsRepository.IsExists(insuranceDetailsId)) return new ResponseModel(404, "Not found");
            if (!_insuranceDetailsRepository.Update(updatedInsuranceDetails))
            {
                return new ResponseModel(500, "Something went wrong updating insuranceDetails");
            }
            return new ResponseModel(204, "");
        }

        public ResponseModel UpdateDeleteStatus(int insuranceDetailsId, bool status)
        {
            if (!_insuranceDetailsRepository.IsExists(insuranceDetailsId)) return new ResponseModel(404, "Not found");
            var updatedInsuranceDetails = _insuranceDetailsRepository.GetById(insuranceDetailsId);
            updatedInsuranceDetails.IsDeleted = status;
            if (!_insuranceDetailsRepository.Update(updatedInsuranceDetails))
            {
                return new ResponseModel(500, "Something went wrong when change delete status insuranceDetails");
            }
            return new ResponseModel(204, "");
        }

        public IEnumerable<InsuranceDetails> Search(string field, string keyWords)
        {
            if (keyWords == "null") return GetInsuranceDetailss();
            keyWords = keyWords.ToLower();
            var list = (from e in GetInsuranceDetailss()
                            where (e.GetType().GetProperty(field)?.GetValue(e)?.ToString()?.ToLower().Contains(keyWords)).GetValueOrDefault()
                            select e).ToList();
            if (list.Count() > 0)
            {
                return list;
            }

            return new List<InsuranceDetails>();
        }
    }
}
