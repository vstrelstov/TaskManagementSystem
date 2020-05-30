using System;
using System.Collections.Generic;

namespace TaskManagementSystem.Infrastructure.Interfaces
{
    public interface IRepository<T> where T: class
    {
        IEnumerable<T> GetAll();
        T GetById(Guid id);
        void Add(T item);
        void Update(T item);
        void Delete(Guid id);
    }
}
