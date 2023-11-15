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
        private readonly ContractRepository _contractRepository;
        public ContractServise(ContractRepository ContractRepository)
        {
            _contractRepository = ContractRepository;
        }
        public ResponseModel CreateContract(Contract ContractCreate)
        {
            var Contracts = _contractRepository.GetAll()
                            .Where(l => l.Content.Trim().ToLower() == ContractCreate.Content.Trim().ToLower())
                            .FirstOrDefault();
            if (Contracts != null)
            {
                return new ResponseModel(422, "Contract already exists");
            }

            if (!_contractRepository.Create(ContractCreate))
            {
                return new ResponseModel(500, "Something went wrong while saving");
            }

            return new ResponseModel(201, "Successfully created");
        }

        public ResponseModel DeleteContract(int ContractId)
        {
            if (!_contractRepository.IsExists(ContractId)) return new ResponseModel(404, "Not found");
            var ContractToDelete = _contractRepository.GetById(ContractId);
            if (!_contractRepository.Delete(ContractToDelete))
            {
                return new ResponseModel(500, "Something went wrong when deleting Contract");
            }
            return new ResponseModel(204, "");
        }

        public Contract? GetContract(int ContractId)
        {
            if (!_contractRepository.IsExists(ContractId)) return null;
            var Contract = _contractRepository.GetById(ContractId);
            return Contract;
        }

        public IEnumerable<Contract> GetContracts()
        {
            return _contractRepository.GetAll();
        }

        public ResponseModel UpdateContract(int ContractId, Contract updatedContract)
        {
            if (!_contractRepository.IsExists(ContractId)) return new ResponseModel(404, "Not found");
          
            if (!_contractRepository.Update(updatedContract))
            {
                return new ResponseModel(500, "Something went wrong updating Contract");
            }
            return new ResponseModel(204, "");
        }
        public ResponseModel UpdateDeleteStatus(int ContractId, bool status)
        {
            if (!_contractRepository.IsExists(ContractId)) return new ResponseModel(404, "Not found");
            var updatedContract = _contractRepository.GetById(ContractId);
            updatedContract.IsDeleted = status;
            if (!_contractRepository.Update(updatedContract))
            {
                return new ResponseModel(500, "Something went wrong when change delete status Contract");
            }
            return new ResponseModel(204, "");
        }

        public IEnumerable<Contract> Search(string field, string keyWords)
        {
            if (keyWords == "null") return _contractRepository.GetAll();
            var res = _contractRepository.Search(field, keyWords);
            if (res == null) return new List<Contract>();
            return res;
        }

    }
}
