using System;
using System.Collections.Generic;
using System.Text;
using TaskManagementSystem.BusinessObjects.DTO;
using TaskManagementSystem.Infrastructure.Entities;

namespace TaskManagementSystem.BusinessObjects.Interfaces
{
    public interface ITasksService
    {
        IEnumerable<ManagedTaskDto> GetTasksHierarchy();
        ManagedTaskDto GetTaskById(Guid id);
        void UpdateTask(ManagedTaskDto task);
        void AddTask(ManagedTaskDto task);
        void DeleteTask(Guid taskId);
    }
}
