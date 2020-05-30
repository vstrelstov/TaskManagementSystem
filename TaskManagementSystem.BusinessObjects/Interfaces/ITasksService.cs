using System;
using System.Collections.Generic;
using TaskManagementSystem.BusinessObjects.DTO;

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
