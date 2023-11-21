using HUG.CRUD.Services;
using SmartHRM.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task = SmartHRM.Repository.Task;

namespace SmartHRM.Services
{
	public class TaskService
	{
		private readonly TaskRepository _TaskRepository;
		public TaskService(TaskRepository TaskRepository)
		{
			_TaskRepository = TaskRepository;
		}
		public ResponseModel CreateTask(Task TaskCreate)
		{
			var Tasks = _TaskRepository.GetAll()
							.Where(l => l.Name.Trim().ToLower() == TaskCreate.Name.Trim().ToLower())
							.FirstOrDefault();
			if (Tasks != null)
			{
				return new ResponseModel(422, "Task already exists");
			}

			if (!_TaskRepository.Create(TaskCreate))
			{
				return new ResponseModel(500, "Something went wrong while saving");
			}

			return new ResponseModel(201, "Successfully created");
		}

		public ResponseModel DeleteTask(int TaskId)
		{
			if (!_TaskRepository.IsExists(TaskId)) return new ResponseModel(404, "Not found");
			var TaskToDelete = _TaskRepository.GetById(TaskId);
			if (!_TaskRepository.Delete(TaskToDelete))
			{
				return new ResponseModel(500, "Something went wrong when deleting Task");
			}
			return new ResponseModel(204, "");
		}

		public Task? GetTask(int TaskId)
		{
			if (!_TaskRepository.IsExists(TaskId)) return null;
			var Task = _TaskRepository.GetById(TaskId);
			return Task;
		}

		public IEnumerable<Task> GetTasks()
		{
			return _TaskRepository.GetAll();
		}

		public ResponseModel UpdateTask(int TaskId, Task updatedTask)
		{
			if (!_TaskRepository.IsExists(TaskId)) return new ResponseModel(404, "Not found");

			if (!_TaskRepository.Update(updatedTask))
			{
				return new ResponseModel(500, "Something went wrong updating Task");
			}
			return new ResponseModel(204, "");
		}

        public ResponseModel UpdateDeleteStatus(int TaskId, bool status)
        {
            if (!_TaskRepository.IsExists(TaskId)) return new ResponseModel(404, "Not found");
            var updatedTask = _TaskRepository.GetById(TaskId);
            updatedTask.IsDeleted = status;
            if (!_TaskRepository.Update(updatedTask))
            {
                return new ResponseModel(500, "Something went wrong when change delete status Task");
            }
            return new ResponseModel(204, "");
        }

        public IEnumerable<Task> Search(string field, string keyWords)
        {
            if (keyWords == "null") return _TaskRepository.GetAll();
            var res = _TaskRepository.Search(field, keyWords);
            if (res == null) return new List<Task>();
            return res;
        }

        //Get total record
        public int GetTotal()
        {
            return _TaskRepository.GetAll().Where(x => x.IsDeleted == false).Count();
        }

        //Get total Time /Task
        public double GetTotalTaskTime()
        {
            return _TaskRepository.GetTotalTaskTime();
        }

        //Statistic 
        public object GetStatisticTask()
        {
            var notStartedCount = _TaskRepository.GetAll().Where(x => x.Status == 0 && x.IsDeleted == false).Count();
            var progressingCount = _TaskRepository.GetAll().Where(x => x.Status == 1 && x.IsDeleted == false).Count();
            var finishedCount = _TaskRepository.GetAll().Where(x => x.Status == 2 && x.IsDeleted == false).Count();
            return new
            {
                NotStarted = notStartedCount,
                Progressing = progressingCount,
                Finished = finishedCount
            };
        }

        //Get top / list
        public IEnumerable<Task> GetTopRecently(int limit)
        {
            var res = _TaskRepository.GetAll().Where(x => x.IsDeleted == false).OrderBy(x => x.CreatedAt).Take(limit);
            return res;
        }
    }
}