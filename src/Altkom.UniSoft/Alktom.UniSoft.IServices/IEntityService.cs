using Altkom.UniSoft.Models;
using System.Collections.Generic;

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
}

