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
    public class SalaryService 
    {
        private readonly EmployeeRepository _employeeRepository;
        private readonly PositionRepository _positionRepository;
        private readonly DepartmentRepository _departmentRepository;
        private readonly DeductionRepository _deductionRepository;
        private readonly AllowanceRepository _allowanceRepository;
        private readonly BonusRepository _bonusRepository;
        private readonly AllowanceDetailsRepository _allowanceDetailsRepository;
        private readonly BonusDetailsRepository _bonusDetailsRepository;
        private readonly DeductionDetailsRepository _deductionDetailsRepository;
        public SalaryService(EmployeeRepository employeeRepository
                               , DepartmentRepository departmentRepository
                               , PositionRepository positionRepository
                               , DeductionRepository deductionRepository
                               , AllowanceRepository allowanceRepository
                               , BonusRepository bonusRepository
                               , AllowanceDetailsRepository allowanceDetailsRepository
                               , BonusDetailsRepository bonusDetailsRepository
                               , DeductionDetailsRepository deductionDetailsRepository)
        {
            _employeeRepository = employeeRepository;

            _positionRepository = positionRepository;
            _departmentRepository = departmentRepository;

            _bonusRepository = bonusRepository;
            _allowanceRepository = allowanceRepository;
            _deductionRepository = deductionRepository;

            _bonusDetailsRepository = bonusDetailsRepository;
            _allowanceDetailsRepository = allowanceDetailsRepository;
            _deductionDetailsRepository = deductionDetailsRepository;
        }
        //GET SALARY
        public List<EmployeeSalaryDto> GetAllSalary()
        {
            //Tiền lương tháng = Lương cơ bản + ((Phụ cấp nếu có)/ngày công chuẩn của tháng)* số ngày làm việc thực tế
            //Mức lương hưởng = Mức lương cơ sở x Hệ số lương hiện hưởng
            var employeeAllowance = from ad in _allowanceDetailsRepository.GetAll()
                                    join a in _allowanceRepository.GetAll() on ad.AllowanceId equals a.Id
                                    group new { a, ad.Id } by new {EmployeeId = ad.EmployeeId, MonthYear = ad.StartAt.ToString("MM/yyyy")} into grpAd
                                    select new
                                    {
                                        EmployeeId = grpAd.Key.EmployeeId,
                                        MonthYear = grpAd.Key.MonthYear,
                                        AllowanceDetailsId = grpAd.Select(g => g.Id).ToList(),
                                        Allowance = grpAd.Select(g => g.a).ToList()
                                    };
            var employeeBonus = from ad in _bonusDetailsRepository.GetAll()
                                join a in _bonusRepository.GetAll() on ad.BonusId equals a.Id
                                group new { a, ad.Id } by new { EmployeeId = ad.EmployeeId, MonthYear = ad.StartAt.ToString("MM/yyyy") } into grpAd
                                select new
                                {
                                    EmployeeId = grpAd.Key.EmployeeId,
                                    MonthYear = grpAd.Key.MonthYear,
                                    BonusDetailsId = grpAd.Select(g => g.Id).ToList(),
                                    Bonus = grpAd.Select(g => g.a).ToList()
                                };
            var employeeDeduction = from ad in _deductionDetailsRepository.GetAll()
                                join a in _deductionRepository.GetAll() on ad.DeductionId equals a.Id
                                    group new { a, ad.Id } by new { EmployeeId = ad.EmployeeId, MonthYear = ad.StartAt.ToString("MM/yyyy") } into grpAd
                                select new
                                {
                                    EmployeeId = grpAd.Key.EmployeeId,
                                    MonthYear = grpAd.Key.MonthYear,
                                    DeductionDetailsId = grpAd.Select(g => g.Id).ToList(),
                                    Deduction = grpAd.Select(g => g.a).ToList()
                                };
            var check = employeeDeduction.ToList();
            var lstEmployeeSalary = new List<EmployeeSalaryDto>();
            foreach (var es in employeeAllowance)
            {
                var selectedEmployee = _employeeRepository.GetById(es.EmployeeId);
                var employeeSalary = new EmployeeSalaryDto()
                {
                    Id = selectedEmployee.Id,
                    FullName = selectedEmployee.FullName,
                    BasicSalary = selectedEmployee.BasicSalary,
                    CoefficientSalary = selectedEmployee.CoefficientSalary
                };
                employeeSalary.AllowanceDetailsId = es.AllowanceDetailsId;
                employeeSalary.Allowances = es.Allowance;
                employeeSalary.MonthYear = es.MonthYear;
                lstEmployeeSalary.Add(employeeSalary);
            }
            foreach (var es in employeeBonus)
            {
                int flag = 0;
                var selectedEmployee = _employeeRepository.GetById(es.EmployeeId);
                var employeeSalary = new EmployeeSalaryDto()
                {
                    Id = selectedEmployee.Id,
                    FullName = selectedEmployee.FullName,
                    BasicSalary = selectedEmployee.BasicSalary,
                    CoefficientSalary = selectedEmployee.CoefficientSalary
                };

                for (int i = 0; i < lstEmployeeSalary.Count; i++)
                {
                    if (lstEmployeeSalary[i].Id == es.EmployeeId&&lstEmployeeSalary[i].MonthYear == es.MonthYear)
                    {
                        lstEmployeeSalary[i].Bonuss = es.Bonus;
                        lstEmployeeSalary[i].BonusDetailsId = es.BonusDetailsId;
                        flag = 1;
                    }
                }
                
                if (flag == 1) continue;
                employeeSalary.Bonuss = es.Bonus;
                employeeSalary.MonthYear = es.MonthYear;
                employeeSalary.BonusDetailsId = es.BonusDetailsId;
                lstEmployeeSalary.Add(employeeSalary);
            }

            foreach (var es in employeeDeduction)
            {
                int flag = 0;
                var selectedEmployee = _employeeRepository.GetById(es.EmployeeId);
                var employeeSalary = new EmployeeSalaryDto()
                {
                    Id = selectedEmployee.Id,
                    FullName = selectedEmployee.FullName,
                    BasicSalary = selectedEmployee.BasicSalary,
                    CoefficientSalary = selectedEmployee.CoefficientSalary
                };

                for (int i = 0; i < lstEmployeeSalary.Count; i++)
                {
                    if (lstEmployeeSalary[i].Id == es.EmployeeId&&lstEmployeeSalary[i].MonthYear == es.MonthYear)
                    {
                        lstEmployeeSalary[i].Deductions = es.Deduction;
                        lstEmployeeSalary[i].DeductionDetailsId = es.DeductionDetailsId;
                        flag = 1;
                    }
                }
                
                if (flag == 1) continue;
                employeeSalary.DeductionDetailsId = es.DeductionDetailsId;
                employeeSalary.Deductions = es.Deduction;
                employeeSalary.MonthYear = es.MonthYear;
                lstEmployeeSalary.Add(employeeSalary);
            }

            return lstEmployeeSalary;
        }

        public EmployeeSalaryDto GetSalary(int employeeId, string monthYear)
        {
            var selectedSalary = GetAllSalary().FirstOrDefault(x => x.Id == employeeId && x.MonthYear == monthYear);
            return selectedSalary;
        }

        //SALARY FUNCTION
        public ResponseModel AddOrUpdateSalary(SalaryDto salaryDto)
        {
            var selectedEmployee = _employeeRepository.GetById(salaryDto.EmployeeId);
            //Update Employee
            if (selectedEmployee == null)
            {
                return new ResponseModel(400, "Not found");
            }
            selectedEmployee.BasicSalary = salaryDto.BasicSalary;
            selectedEmployee.CoefficientSalary = salaryDto.CoefficientSalary;
            if (!_employeeRepository.Update(selectedEmployee))
            {
                return new ResponseModel(500, "Something went wrong updating Employee");
            }
            //Delete if need
            var currentSalary = GetSalary(selectedEmployee.Id, salaryDto.MonthYear.ToString("MM/yyyy"));
            if (currentSalary!=null&&currentSalary.AllowanceDetailsId != null)
            {
                var lstNewAlowanceDetails = salaryDto.Allowances.Select(x => x.Id);
                var lstRemoveAlowanceDetails = currentSalary.AllowanceDetailsId.Except(lstNewAlowanceDetails);
                foreach (var detailsId in lstRemoveAlowanceDetails)
                {
                    var deletedDetails = DeleteAllowanceDetails(detailsId);
                    if (deletedDetails.Status != 204) return deletedDetails;
                }

            }

            if (currentSalary != null && currentSalary != null && currentSalary.BonusDetailsId != null)
            {
                var lstNewBonusDetails = salaryDto.Bonus.Select(x => x.Id);
                var lstRemoveBonusDetails = currentSalary.BonusDetailsId.Except(lstNewBonusDetails);
                foreach (var detailsId in lstRemoveBonusDetails)
                {
                    var deletedDetails = DeleteBonusDetails(detailsId);
                    if (deletedDetails.Status != 204) return deletedDetails;
                }

            }

            if (currentSalary != null && currentSalary.DeductionDetailsId != null)
            {
                var lstNewDeductionDetails = salaryDto.Deduction.Select(x => x.Id);
                var lstRemoveDeductionDetails = currentSalary.DeductionDetailsId.Except(lstNewDeductionDetails);
                foreach (var detailsId in lstRemoveDeductionDetails)
                {
                    var deletedDetails = DeleteDeductionDetails(detailsId);
                    if (deletedDetails.Status != 204) return deletedDetails;
                }
            }

            //Update Details
            foreach (var s in salaryDto.Allowances)
            {
                if(s.Id == 0)
                {
                    var addedDetails = CreateAllowanceDetails(s);
                    if (addedDetails.Status != 201) return addedDetails;
                }
                if (!_allowanceDetailsRepository.Update(s))
                {
                    return new ResponseModel(500, "Something went wrong updating Allowances");
                }
            }
            foreach (var s in salaryDto.Bonus)
            {
                if (s.Id == 0)
                {
                    var addedDetails = CreateBonusDetails(s);
                    if (addedDetails.Status != 201) return addedDetails;
                }
                if (!_bonusDetailsRepository.Update(s))
                {
                    return new ResponseModel(500, "Something went wrong updating Bonus");
                }
            }
            foreach (var s in salaryDto.Deduction)
            {
                if (s.Id == 0)
                {
                    var addedDetails = CreateDeductionDetails(s);
                    if (addedDetails.Status != 201) return addedDetails;
                }
                if (!_deductionDetailsRepository.Update(s))
                {
                    return new ResponseModel(500, "Something went wrong updating Deduction");
                }
            }
            return new ResponseModel(204, "");
        }

        public ResponseModel DeleteSalary(int employeeId, string monthYear)
        {
            var selectedSalary = GetAllSalary().FirstOrDefault(x => x.Id == employeeId && x.MonthYear == monthYear);
            if(selectedSalary==null) return new ResponseModel(404, "Not found");
            if (selectedSalary.AllowanceDetailsId != null)
            {
                foreach (var detailsId in selectedSalary.AllowanceDetailsId)
                {
                    var deletedDetails = DeleteAllowanceDetails(detailsId);
                    if (deletedDetails.Status != 204) return deletedDetails;
                }
            }
            if (selectedSalary.BonusDetailsId != null)
            {
                foreach (var detailsId in selectedSalary.BonusDetailsId)
                {
                    var deletedDetails = DeleteBonusDetails(detailsId);
                    if (deletedDetails.Status != 204) return deletedDetails;
                }
            }
            if (selectedSalary.DeductionDetailsId != null)
            {
                foreach (var detailsId in selectedSalary.DeductionDetailsId)
                {
                    var deletedDetails = DeleteDeductionDetails(detailsId);
                    if (deletedDetails.Status != 204) return deletedDetails;
                }
            }
            return new ResponseModel(204, "");
        }

        //DETAILS FUNCTION
        public ResponseModel CreateAllowanceDetails(AllowanceDetails allowanceDetails)
        {
            var details = _allowanceDetailsRepository.IsExists(allowanceDetails);
            if (details)
            {
                return new ResponseModel(422, "AllowanceDetails already exists");
            }

            if (!_allowanceDetailsRepository.Create(allowanceDetails))
            {
                return new ResponseModel(500, "Something went wrong while saving");
            }

            return new ResponseModel(201, "Successfully created");
        }

        public ResponseModel CreateBonusDetails(BonusDetails bonusDetails)
        {
            var details = _bonusDetailsRepository.IsExists(bonusDetails);
            if (details)
            {
                return new ResponseModel(422, "BonusDetails already exists");
            }

            if (!_bonusDetailsRepository.Create(bonusDetails))
            {
                return new ResponseModel(500, "Something went wrong while saving");
            }

            return new ResponseModel(201, "Successfully created");
        }

        public ResponseModel CreateDeductionDetails(DeductionDetails deductionDetails)
        {
            var details = _deductionDetailsRepository.IsExists(deductionDetails);
            if (details)
            {
                return new ResponseModel(422, "DeductionDetails already exists");
            }

            if (!_deductionDetailsRepository.Create(deductionDetails))
            {
                return new ResponseModel(500, "Something went wrong while saving");
            }

            return new ResponseModel(201, "Successfully created");
        }


        public ResponseModel DeleteAllowanceDetails(int AllowanceId)
        {
            if (!_allowanceDetailsRepository.IsExists(AllowanceId)) return new ResponseModel(404, "Not found");
            var AllowanceToDelete = _allowanceDetailsRepository.GetById(AllowanceId);
            if (!_allowanceDetailsRepository.Delete(AllowanceToDelete))
            {
                return new ResponseModel(500, "Something went wrong when deleting Allowance");
            }
            return new ResponseModel(204, "");
        }

        public ResponseModel DeleteBonusDetails(int BonusId)
        {
            if (!_bonusDetailsRepository.IsExists(BonusId)) return new ResponseModel(404, "Not found");
            var BonusToDelete = _bonusDetailsRepository.GetById(BonusId);
            if (!_bonusDetailsRepository.Delete(BonusToDelete))
            {
                return new ResponseModel(500, "Something went wrong when deleting Bonus");
            }
            return new ResponseModel(204, "");
        }

        public ResponseModel DeleteDeductionDetails(int DeductionId)
        {
            if (!_deductionDetailsRepository.IsExists(DeductionId)) return new ResponseModel(404, "Not found");
            var DeductionToDelete = _deductionDetailsRepository.GetById(DeductionId);
            if (!_deductionDetailsRepository.Delete(DeductionToDelete))
            {
                return new ResponseModel(500, "Something went wrong when deleting Deduction");
            }
            return new ResponseModel(204, "");
        }


    }
}
