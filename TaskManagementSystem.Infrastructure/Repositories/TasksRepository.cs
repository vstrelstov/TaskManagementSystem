using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TaskManagementSystem.Infrastructure.Contexts;
using TaskManagementSystem.Infrastructure.Entities;
using TaskManagementSystem.Infrastructure.Interfaces;

namespace TaskManagementSystem.Infrastructure.Repositories
{
    public class TasksRepository : IRepository<ManagedTask>
    {
        private readonly TasksContext _context;

        public TasksRepository(TasksContext context)
        {
            _context = context;
        }

        public IEnumerable<ManagedTask> GetAll()
        {
            return _context.Tasks.Include(x => x.SubTasks);
        }

        public ManagedTask GetById(Guid id)
        {
            return GetAll().FirstOrDefault(x => x.Id == id);
        }

        public void Add(ManagedTask item)
        {
            if (item.ParentTask == null)
                _context.Add(item);
            else
                AddSubTask(item);
        }

        public void Update(ManagedTask item)
        {
            var originalTask = _context.Tasks.FirstOrDefault(x => x.Id == item.Id);
            if (originalTask != null)
                _context.Entry(originalTask).CurrentValues.SetValues(item);
        }
        
        public void Delete(Guid id)
        {
            ManagedTask taskToDelete = _context.Tasks.Find(id);
            if (taskToDelete != null)
            {
                var parent = _context.Tasks.AsEnumerable().FirstOrDefault(x => x.SubTasks.Contains(taskToDelete)); // TODO: Понять причину падения
                if (parent != null)
                {
                    foreach (var subTask in _context.Tasks.Where(x => x.ParentTask.Id == id))
                        subTask.ParentTask = parent;
                }
                _context.Tasks.Remove(taskToDelete);
            }
        }

        private void AddSubTask(ManagedTask item)
        {
            var parent = GetById(item.ParentTask.Id);
            if (parent.SubTasks == null)
                parent.SubTasks = new List<ManagedTask>();
            parent.SubTasks.Add(item);
            Update(parent);
        }
    }
}
