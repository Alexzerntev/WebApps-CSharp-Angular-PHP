using Nod.Dal.Interfaces;
using Nod.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Nod.Dal.Interfaces
{
    public interface IRepository<T> where T : IEntity
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> predicate);
        Task<IEnumerable<T>> FindLimitedAsync(Expression<Func<T, bool>> predicate, int size);
        Task<IEnumerable<T>> FindPaginatedAsync(Expression<Func<T, bool>> predicate, int pageSize, int startPage);
        Task<T> GetOneAsync(string id);
        Task<T> AddOneAsync(T item);
        Task<bool> RemoveOneAsync(string id);
        Task<bool> UpdateOneAsync(string id, T item);
        Task<bool> RemoveAllAsync();
    }
}
