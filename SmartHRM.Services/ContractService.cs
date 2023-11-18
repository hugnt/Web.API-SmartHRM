using HUG.CRUD.Services;
using SmartHRM.Repository;
using SmartHRM.Repository.Models;
using SmartHRM.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHRM.Services
{
    public class ContractService 
    {
        private readonly ContractRepository _contractRepository;
        private readonly EmployeeRepository _employeeRepository;
        public ContractService(ContractRepository contractRepository, EmployeeRepository employeeRepository)
        {
            _contractRepository = contractRepository;
            _employeeRepository = employeeRepository;
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

        public ContractDto? GetContract(int ContractId)
        {
            if (!_contractRepository.IsExists(ContractId)) return null;
            var res = GetContracts().FirstOrDefault(x => x.Id == ContractId);
            return res;
        }

        public IEnumerable<ContractDto> GetContracts()
        {
            var contractQuerry = from C in _contractRepository.GetAll()
                                 join E in _employeeRepository.GetAll() on C.EmployeeId equals E.Id
                                 select new ContractDto
                                 {
                                     Id = C.Id,
                                     Content = C.Content,
                                     SignedAt = C.SignedAt,
                                     ExpriedAt = C.ExpriedAt,
                                     Image = C.Image,
                                     Employees = E,
                                     IsDeleted = C.IsDeleted,
                                 };
            var cdto = contractQuerry.ToList();
            return cdto;
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

        public IEnumerable<ContractDto> Search(string field, string keyWords)
        {
            if (keyWords == "null") return GetContracts();
            var res = _contractRepository.Search(field, keyWords);
            var rescdto = new List<ContractDto>();
            foreach (var C in res)
            {
                rescdto.Add(new ContractDto()
                {

                    Id = C.Id,
                    Content = C.Content,
                    SignedAt = C.SignedAt,
                    ExpriedAt = C.ExpriedAt,
                    Image = C.Image,
                    Employees = _employeeRepository.GetById(C.EmployeeId),
                    IsDeleted = C.IsDeleted,
                });
            }
            if (rescdto == null) return new List<ContractDto>();
            return rescdto;
        }

    }
}
