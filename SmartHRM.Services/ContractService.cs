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
    public class ContractServise 
    {
        private readonly ContractRepository _ContractRepository;
        public ContractServise(ContractRepository ContractRepository)
        {
            _ContractRepository = ContractRepository;
        }
        public ResponseModel CreateContract(Contract ContractCreate)
        {
            var Contracts = _ContractRepository.GetAll()
                            .Where(l => l.Content.Trim().ToLower() == ContractCreate.Content.Trim().ToLower())
                            .FirstOrDefault();
            if (Contracts != null)
            {
                return new ResponseModel(422, "Contract already exists");
            }

            if (!_ContractRepository.Create(ContractCreate))
            {
                return new ResponseModel(500, "Something went wrong while saving");
            }

            return new ResponseModel(201, "Successfully created");
        }

        public ResponseModel DeleteContract(int ContractId)
        {
            if (!_ContractRepository.IsExists(ContractId)) return new ResponseModel(404, "Not found");
            var ContractToDelete = _ContractRepository.GetById(ContractId);
            if (!_ContractRepository.Delete(ContractToDelete))
            {
                return new ResponseModel(500, "Something went wrong when deleting Contract");
            }
            return new ResponseModel(204, "");
        }

        public Contract? GetContract(int ContractId)
        {
            if (!_ContractRepository.IsExists(ContractId)) return null;
            var Contract = _ContractRepository.GetById(ContractId);
            return Contract;
        }

        public IEnumerable<Contract> GetContracts()
        {
            return _ContractRepository.GetAll();
        }

        public ResponseModel UpdateContract(int ContractId, Contract updatedContract)
        {
            if (!_ContractRepository.IsExists(ContractId)) return new ResponseModel(404, "Not found");
          
            if (!_ContractRepository.Update(updatedContract))
            {
                return new ResponseModel(500, "Something went wrong updating Contract");
            }
            return new ResponseModel(204, "");
        }

   
    }
}
