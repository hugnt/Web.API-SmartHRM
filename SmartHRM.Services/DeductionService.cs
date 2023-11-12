using HUG.CRUD.Services;
using SmartHRM.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHRM.Services
{
    public class DeductionService 
    {
        private readonly DeductionRepository _DeductionRepository;
        public DeductionService(DeductionRepository DeductionRepository)
        {
            _DeductionRepository = DeductionRepository;
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

    }
}
