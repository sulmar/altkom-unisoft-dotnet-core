using Altkom.UniSoft.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Alktom.UniSoft.IServices
{
    public interface IEntityService<T>
        where T : Base
    {
        ICollection<T> Get();
        T Get(int id);
        void Add(T entity);
        void Update(T entity);
        void Remove(int id);
    }

    public interface IEntityServiceAsync<T>
       where T : Base
    {
        Task<ICollection<T>> GetAsync();
        Task<T> GetAsync(int id);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task RemoveAsync(int id);
    }

}

