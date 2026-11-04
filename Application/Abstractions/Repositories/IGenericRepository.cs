using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Abstractions.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T?> GetByIdAsync(Guid id);
        Task AddAsync(T entity);
        void Update(T entity);
        void Remove(T entity);
    }
}
