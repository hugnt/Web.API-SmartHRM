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
    public class TaskDetailsService 
    {
        private readonly TaskRepository _TaskRepository;
        private readonly EmployeeRepository _employeeRepository;
        private readonly TaskDetailsRepository _TaskDetailsRepository;
        public TaskDetailsService(TaskRepository TaskRepository,
                                EmployeeRepository employeeRepository,
                                TaskDetailsRepository TaskDetailsRepository)
        {
            _TaskRepository = TaskRepository;
            _employeeRepository = employeeRepository;
            _TaskDetailsRepository = TaskDetailsRepository;
        }
        public ResponseModel CreateTaskDetails(TaskDetails TaskDetailsCreate)
        {
            var TaskDetailss = _TaskDetailsRepository.IsExists(TaskDetailsCreate);
            if (TaskDetailss)
            {
                return new ResponseModel(422, "TaskDetails already exists");
            }

            if (!_TaskDetailsRepository.Create(TaskDetailsCreate))
            {
                return new ResponseModel(500, "Something went wrong while saving");
            }

            return new ResponseModel(201, "Successfully created");
        }

        public ResponseModel DeleteTaskDetails(int TaskDetailsId)
        {
            if (!_TaskDetailsRepository.IsExists(TaskDetailsId)) return new ResponseModel(404, "Not found");
            var TaskDetailsToDelete = _TaskDetailsRepository.GetById(TaskDetailsId);
            if (!_TaskDetailsRepository.Delete(TaskDetailsToDelete))
            {
                return new ResponseModel(500, "Something went wrong when deleting TaskDetails");
            }
            return new ResponseModel(204, "");
        }

        public TaskDetails? GetTaskDetails(int TaskDetailsId)
        {
            if (!_TaskDetailsRepository.IsExists(TaskDetailsId)) return null;
            var TaskDetails = GetTaskDetailss().FirstOrDefault(x=> x.Id == TaskDetailsId);
            return TaskDetails;
        }

        public IEnumerable<TaskDetailsDto> GetTaskDetailss()
        {
            var queryTaskDetails = from e in _employeeRepository.GetAll()
                                        join ti in _TaskDetailsRepository.GetAll() on e.Id equals ti.AssigneeId
                                        join t in _TaskRepository.GetAll() on ti.TaskId equals t.Id
                                        select new TaskDetailsDto
                                        {
                                            Id = ti.Id,
                                            AssigneeId = ti.AssigneeId,
                                            AssignerId = ti.AssignerId,
                                            AssigneeName = _employeeRepository.GetById(ti.AssigneeId).FullName,
                                            AssignerName = _employeeRepository.GetById(ti.AssignerId).FullName,
                                            TaskName = t.Name,
                                            TaskId = ti.Id,
                                            Content = ti.Content,
                                            Description = ti.Description,
                                            Status = ti.Status,
                                            IsDeleted = ti.IsDeleted
                                        };
            return queryTaskDetails.ToList();
        }

        public ResponseModel UpdateTaskDetails(int TaskDetailsId, TaskDetails updatedTaskDetails)
        {
            if (!_TaskDetailsRepository.IsExists(TaskDetailsId)) return new ResponseModel(404, "Not found");
            if (!_TaskDetailsRepository.Update(updatedTaskDetails))
            {
                return new ResponseModel(500, "Something went wrong updating TaskDetails");
            }
            return new ResponseModel(204, "");
        }

        public ResponseModel UpdateDeleteStatus(int TaskDetailsId, bool status)
        {
            if (!_TaskDetailsRepository.IsExists(TaskDetailsId)) return new ResponseModel(404, "Not found");
            var updatedTaskDetails = _TaskDetailsRepository.GetById(TaskDetailsId);
            updatedTaskDetails.IsDeleted = status;
            if (!_TaskDetailsRepository.Update(updatedTaskDetails))
            {
                return new ResponseModel(500, "Something went wrong when change delete status TaskDetails");
            }
            return new ResponseModel(204, "");
        }

        public IEnumerable<TaskDetails> Search(string field, string keyWords)
        {
            if (keyWords == "null") return GetTaskDetailss();
            keyWords = keyWords.ToLower();  
            var list = (from e in GetTaskDetailss()
                            where (e.GetType().GetProperty(field)?.GetValue(e)?.ToString()?.ToLower().Contains(keyWords)).GetValueOrDefault()
                            select e).ToList();
            if (list.Count() > 0)
            {
                return list;
            }
            return new List<TaskDetails>();
        }
    }
}
