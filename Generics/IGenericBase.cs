using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesApi.Generics
{
    public interface IGenericBase<T> where T : class
    {
         Task<T> AddAsync(T entity);
         Task<T> DeleteAsync(T entity);
         Task<T> UpdateAsync(T entity);
         Task<T> GetByIdAsync(int id);


    }
}