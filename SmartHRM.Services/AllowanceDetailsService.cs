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
    public class AllowanceDetailsService 
    {
        private readonly AllowanceRepository _allowanceRepository;
        private readonly EmployeeRepository _employeeRepository;
        private readonly AllowanceDetailsRepository _allowanceDetailsRepository;
        public AllowanceDetailsService(AllowanceRepository allowanceRepository,
                                EmployeeRepository employeeRepository,
                                AllowanceDetailsRepository allowanceDetailsRepository)
        {
            _allowanceRepository = allowanceRepository;
            _employeeRepository = employeeRepository;
            _allowanceDetailsRepository = allowanceDetailsRepository;
        }
        public ResponseModel CreateAllowanceDetails(AllowanceDetails allowanceDetailsCreate)
        {
            var allowanceDetailss = _allowanceDetailsRepository.IsExists(allowanceDetailsCreate);
            if (allowanceDetailss)
            {
                return new ResponseModel(422, "AllowanceDetails already exists");
            }

            if (!_allowanceDetailsRepository.Create(allowanceDetailsCreate))
            {
                return new ResponseModel(500, "Something went wrong while saving");
            }

            return new ResponseModel(201, "Successfully created");
        }

        public ResponseModel DeleteAllowanceDetails(int allowanceDetailsId)
        {
            if (!_allowanceDetailsRepository.IsExists(allowanceDetailsId)) return new ResponseModel(404, "Not found");
            var allowanceDetailsToDelete = _allowanceDetailsRepository.GetById(allowanceDetailsId);
            if (!_allowanceDetailsRepository.Delete(allowanceDetailsToDelete))
            {
                return new ResponseModel(500, "Something went wrong when deleting allowanceDetails");
            }
            return new ResponseModel(204, "");
        }

      
        public ResponseModel UpdateAllowanceDetails(int allowanceDetailsId, AllowanceDetails updatedAllowanceDetails)
        {
            if (!_allowanceDetailsRepository.IsExists(allowanceDetailsId)) return new ResponseModel(404, "Not found");
            if (!_allowanceDetailsRepository.Update(updatedAllowanceDetails))
            {
                return new ResponseModel(500, "Something went wrong updating allowanceDetails");
            }
            return new ResponseModel(204, "");
        }

        public ResponseModel UpdateDeleteStatus(int allowanceDetailsId, bool status)
        {
            if (!_allowanceDetailsRepository.IsExists(allowanceDetailsId)) return new ResponseModel(404, "Not found");
            var updatedAllowanceDetails = _allowanceDetailsRepository.GetById(allowanceDetailsId);
            updatedAllowanceDetails.IsDeleted = status;
            if (!_allowanceDetailsRepository.Update(updatedAllowanceDetails))
            {
                return new ResponseModel(500, "Something went wrong when change delete status allowanceDetails");
            }
            return new ResponseModel(204, "");
        }

       
    }
}
