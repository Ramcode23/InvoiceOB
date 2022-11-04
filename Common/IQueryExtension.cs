using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Common
{
   
    public interface IQueryExtension<T>
    {
          Task<DataCollection<T>> GetAllAsync(int page, int take, IEnumerable<int> entities = null);
    
          Task<T> GetAsync(int id);
          Task AddAsync(T entity);
          Task UpdateAsync(int id,T entity);
          Task DeleteAsync(int id,T entity);

    }

}