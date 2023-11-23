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
    public class InsuranceService 
    {
        private readonly InsuranceRepository _insuranceRepository;
        private readonly EmployeeRepository _employeeRepository;
        private readonly InsuranceDetailsRepository _insuranceDetailsRepository;
        public InsuranceService(InsuranceRepository insuranceRepository, 
                                EmployeeRepository employeeRepository,
                                InsuranceDetailsRepository insuranceDetailsRepository)
        {
            _insuranceRepository = insuranceRepository;
            _employeeRepository = employeeRepository;
            _insuranceDetailsRepository = insuranceDetailsRepository;
        }
        public ResponseModel CreateInsurance(Insurance insuranceCreate)
        {
            var insurances = _insuranceRepository.GetAll()
                            .Where(l =>
                                    l.Name.Trim().ToLower() == insuranceCreate.Name.Trim().ToLower())
                            .FirstOrDefault();
            if (insurances != null)
            {
                return new ResponseModel(422, "Insurance already exists");
            }

            if (!_insuranceRepository.Create(insuranceCreate))
            {
                return new ResponseModel(500, "Something went wrong while saving");
            }

            return new ResponseModel(201, "Successfully created");
        }

        public ResponseModel DeleteInsurance(int insuranceId)
        {
            if (!_insuranceRepository.IsExists(insuranceId)) return new ResponseModel(404, "Not found");
            var insuranceToDelete = _insuranceRepository.GetById(insuranceId);
            if (!_insuranceRepository.Delete(insuranceToDelete))
            {
                return new ResponseModel(500, "Something went wrong when deleting insurance");
            }
            return new ResponseModel(204, "");
        }

        public Insurance? GetInsurance(int insuranceId)
        {
            if (!_insuranceRepository.IsExists(insuranceId)) return null;
            var insurance = _insuranceRepository.GetById(insuranceId);
            return insurance;
        }

        public IEnumerable<Insurance> GetInsurances()
        {
            return _insuranceRepository.GetAll();
        }

        public ResponseModel UpdateInsurance(int insuranceId, Insurance updatedInsurance)
        {
            if (!_insuranceRepository.IsExists(insuranceId)) return new ResponseModel(404, "Not found");
            if (!_insuranceRepository.Update(updatedInsurance))
            {
                return new ResponseModel(500, "Something went wrong updating insurance");
            }
            return new ResponseModel(204, "");
        }

        public ResponseModel UpdateDeleteStatus(int insuranceId, bool status)
        {
            if (!_insuranceRepository.IsExists(insuranceId)) return new ResponseModel(404, "Not found");
            var updatedInsurance = _insuranceRepository.GetById(insuranceId);
            updatedInsurance.IsDeleted = status;
            if (!_insuranceRepository.Update(updatedInsurance))
            {
                return new ResponseModel(500, "Something went wrong when change delete status insurance");
            }
            return new ResponseModel(204, "");
        }

        public IEnumerable<Insurance> Search(string field, string keyWords)
        {
            if (keyWords == "null") return _insuranceRepository.GetAll();
            var res  = _insuranceRepository.Search(field, keyWords);
            if(res==null) return new List<Insurance>();
            return res;
        }

        public IEnumerable<InsuranceDetailsDto> GetInsuranceDetailsALOLO()
        {
            var employeeInsuranceList = from e in _employeeRepository.GetAll()
                                        join ied in _insuranceDetailsRepository.GetAll() on e.Id equals ied.EmployeeId
                                        join i in _insuranceRepository.GetAll() on ied.InsuranceId equals i.Id
                                        group ied by e into employeeGroup
                                        select new InsuranceDetailsDto
                                        {
                                           
                                        };
            var resp = employeeInsuranceList.ToList();
            return resp;

        }


    }
}
