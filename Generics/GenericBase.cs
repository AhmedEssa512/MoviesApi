using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MoviesApi.Data;

namespace MoviesApi.Generics
{
    public class GenericBase<T> : IGenericBase<T> where T : class
    {
        private readonly ApplicationDbContext _context;

        public GenericBase(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<T>  AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

         public async Task<T> DeleteAsync(T entity)
        {
             _context.Set<T>().Remove(entity);
             await _context.SaveChangesAsync();
             return entity;
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<T> UpdateAsync(T entity)
        {
            
             _context.Set<T>().Update(entity);
             await _context.SaveChangesAsync();

             return entity;

        }
    }
}