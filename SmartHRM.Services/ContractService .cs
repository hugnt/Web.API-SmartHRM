using HUG.CRUD.Services;
using SmartHRM.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHRM.Services
{
	public class ContractService
	{
		private readonly ContractRepository _ContractRepository;
		public ContractService(ContractRepository ContractRepository)
		{
			_ContractRepository = ContractRepository;
		}
		public ResponseModel CreateRole(Contract ContractCreate)
		{
			var Roles = _ContractRepository.GetAll()
							.Where(l => l.EmployeeId == ContractCreate.EmployeeId)
							.FirstOrDefault();
			if (Roles != null)
			{
				return new ResponseModel(422, "Contract already exists");
			}

			if (!_ContractRepository.Create(ContractCreate))
			{
				return new ResponseModel(500, "Something went wrong while saving");
			}

			return new ResponseModel(201, "Successfully created");
		}

		public ResponseModel DeleteRole(int RoleId)
		{
			if (!_ContractRepository.IsExists(RoleId)) return new ResponseModel(404, "Not found");
			var RoleToDelete = _ContractRepository.GetById(RoleId);
			if (!_ContractRepository.Delete(RoleToDelete))
			{
				return new ResponseModel(500, "Something went wrong when deleting Contract");
			}
			return new ResponseModel(204, "");
		}

		public Contract? GetRole(int RoleId)
		{
			if (!_ContractRepository.IsExists(RoleId)) return null;
			var Contract = _ContractRepository.GetById(RoleId);
			return Contract;
		}

		public IEnumerable<Contract> GetRoles()
		{
			return _ContractRepository.GetAll();
		}

		public ResponseModel UpdateRole(int RoleId, Contract updatedRole)
		{
			if (!_ContractRepository.IsExists(RoleId)) return new ResponseModel(404, "Not found");

			if (!_ContractRepository.Update(updatedRole))
			{
				return new ResponseModel(500, "Something went wrong updating Contract");
			}
			return new ResponseModel(204, "");
		}


	}
}