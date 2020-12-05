using System;
using TaskManagementSystem.Infrastructure.Contexts;
using TaskManagementSystem.Infrastructure.Entities;
using TaskManagementSystem.Infrastructure.Interfaces;

namespace TaskManagementSystem.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private TasksContext _context;
        private readonly Lazy<TasksRepository> _tasksRepository;

        public UnitOfWork()
        {
            _context = TasksContextFactory.CreateContext();
            _tasksRepository = new Lazy<TasksRepository>(() => new TasksRepository(_context));
        }

        public IRepository<ManagedTask> TaskRepository => _tasksRepository.Value;

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
