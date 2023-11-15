using HUG.CRUD.Services;
using SmartHRM.Repository;
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
		public ResponseModel CreateRole(Insurance InsuranceCreate)
		{
			var Roles = _InsuranceRepository.GetAll()
							.Where(l => l.Name.Trim().ToLower() == InsuranceCreate.Name.Trim().ToLower())
							.FirstOrDefault();
			if (Roles != null)
			{
				return new ResponseModel(422, "Insurance already exists");
			}

			if (!_InsuranceRepository.Create(InsuranceCreate))
			{
				return new ResponseModel(500, "Something went wrong while saving");
			}

			return new ResponseModel(201, "Successfully created");
		}

		public ResponseModel DeleteRole(int RoleId)
		{
			if (!_InsuranceRepository.IsExists(RoleId)) return new ResponseModel(404, "Not found");
			var RoleToDelete = _InsuranceRepository.GetById(RoleId);
			if (!_InsuranceRepository.Delete(RoleToDelete))
			{
				return new ResponseModel(500, "Something went wrong when deleting Insurance");
			}
			return new ResponseModel(204, "");
		}

		public Insurance? GetRole(int RoleId)
		{
			if (!_InsuranceRepository.IsExists(RoleId)) return null;
			var Insurance = _InsuranceRepository.GetById(RoleId);
			return Insurance;
		}

		public IEnumerable<Insurance> GetRoles()
		{
			return _InsuranceRepository.GetAll();
		}

		public ResponseModel UpdateRole(int RoleId, Insurance updatedRole)
		{
			if (!_InsuranceRepository.IsExists(RoleId)) return new ResponseModel(404, "Not found");

			if (!_InsuranceRepository.Update(updatedRole))
			{
				return new ResponseModel(500, "Something went wrong updating Insurance");
			}
			return new ResponseModel(204, "");
		}


	}
}