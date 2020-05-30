using System;
using System.Collections.Generic;
using System.Text;
using TaskManagementSystem.Infrastructure.Entities;

namespace TaskManagementSystem.Infrastructure.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<ManagedTask> TaskRepository { get; }
        void Save();
    }
}
