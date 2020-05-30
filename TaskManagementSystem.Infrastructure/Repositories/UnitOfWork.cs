using System;
using TaskManagementSystem.Infrastructure.Contexts;
using TaskManagementSystem.Infrastructure.Entities;
using TaskManagementSystem.Infrastructure.Interfaces;

namespace TaskManagementSystem.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private TasksContext _context;
        private TasksRepository _tasksRepository;

        public UnitOfWork()
        {
            _context = TasksContextFactory.CreateContext();
        }

        public IRepository<ManagedTask> TaskRepository
        {
            get
            {
                if (_tasksRepository == null)
                    _tasksRepository = new TasksRepository(_context);
                return _tasksRepository;
            }
        }
        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
