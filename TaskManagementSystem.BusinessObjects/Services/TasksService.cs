using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TaskManagementSystem.BusinessObjects.DTO;
using TaskManagementSystem.BusinessObjects.Interfaces;
using TaskManagementSystem.Infrastructure.Entities;
using TaskManagementSystem.Infrastructure.Interfaces;

namespace TaskManagementSystem.BusinessObjects.Services
{
    public class TasksService : ITasksService
    {
        private readonly IUnitOfWork _database;
        private readonly IMapper _mapper;

        public TasksService(IUnitOfWork unitOfWork)
        {
            _database = unitOfWork;
            _mapper = new MapperConfiguration(cfg => cfg.CreateMap<ManagedTask, ManagedTaskDto>()).CreateMapper();
        }

        public IEnumerable<ManagedTaskDto> GetTasksHierarchy()
        {
            return _mapper.Map<IEnumerable<ManagedTask>, IEnumerable<ManagedTaskDto>>(_database.TaskRepository.GetAll()
                .OrderBy(x => x.DateOfIssue).Where(x => x.ParentTask == null));
        }

        public ManagedTaskDto GetTaskById(Guid id)
        {
            return _mapper.Map<ManagedTask, ManagedTaskDto>(_database.TaskRepository.GetById(id));
        }

        private void ChangeTaskState(ManagedTaskDto task, ManagedTaskState originalState)
        {
            if (!Enum.IsDefined(typeof(ManagedTaskState), task.State))
                throw new InvalidOperationException("Заданного состояния не существует");

            if (task.State == ManagedTaskState.Paused && originalState != ManagedTaskState.Executing)
                throw new InvalidOperationException("Статус \"Приостановлена\" может быть присвоен только задачам со статусом \"Выполняется\"");

            if (task.State == ManagedTaskState.Completed)
            {
                if (originalState != ManagedTaskState.Executing || !SubTasksCanBeCompleted(task))
                    throw new InvalidOperationException("Статус \"Завершена\" может быть присвоен только задачам со статусом \"Выполняется\"," +
                                                        " подзадачи которых также имеют статус \"Выполняется\" или \"Завершена\"");
                task.DateOfCompletion = DateTime.Now;
                CompleteSubTasks(task);
            }
            
        }
        
        public void UpdateTask(ManagedTaskDto task)
        {
            var originalState = _database.TaskRepository.GetById(task.Id).State;
            if (originalState != task.State)
                ChangeTaskState(task, originalState);
            
            SaveTask(task);
        }

        public void AddTask(ManagedTaskDto task)
        {
            if (task == null)
                throw new ArgumentNullException();

            var newTask = _mapper.Map<ManagedTaskDto, ManagedTask>(task);
            newTask.Id = Guid.NewGuid();
            newTask.DateOfIssue = DateTime.Now;
            
            _database.TaskRepository.Add(newTask);
            _database.Save();
        }

        public void DeleteTask(Guid taskId)
        {
            _database.TaskRepository.Delete(taskId);
            _database.Save();
        }

        private void SaveTask(ManagedTaskDto task)
        {
            var taskToUpdate = _mapper.Map<ManagedTaskDto, ManagedTask>(task);

            _database.TaskRepository.Update(taskToUpdate);
            _database.Save();
        }

        private void SetTaskState(ManagedTaskDto task, ManagedTaskState newState)
        {
            task.State = newState;
            task.DateOfCompletion = DateTime.Now;
            SaveTask(task);
        }

        private bool SubTasksCanBeCompleted(ManagedTaskDto task)
        {
            foreach (var subTask in task.SubTasks)
            {
                if (!SubTasksCanBeCompleted(subTask))
                    return false;
            }

            return task.State == ManagedTaskState.Executing || task.State == ManagedTaskState.Completed;
        }

        private void CompleteSubTasks(ManagedTaskDto task)
        {
            if (task.SubTasks != null)
                foreach (var subTask in task.SubTasks)
                {
                    CompleteSubTasks(subTask);
                }
            SetTaskState(task, ManagedTaskState.Completed);
        }
    }
}
