using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MoviesApi.Data;
using MoviesApi.Generics;

namespace MoviesApi.Services
{
    public class GenreService : GenericBase<Genre>, IGenreService
    {
          private readonly DbSet<Genre> _genre;

        public GenreService(ApplicationDbContext context):base(context)
        {
            _genre = context.Set<Genre>();
        }

       

        public async Task<IEnumerable<Genre>> GetAll()
        {
            return   await _genre.OrderBy(g => g.Name).ToListAsync();   
        }



        public Task<bool> IsvalidGenre(byte id)
        {
            return   _genre.AnyAsync(g => g.Id == id);
        }

        
    }
}