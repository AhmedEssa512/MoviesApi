using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MoviesApi.Data;
using MoviesApi.Generics;

namespace MoviesApi.Services
{
    public class MovieService : GenericBase<Movie>,IMoviesService
    {
           private readonly DbSet<Movie> _movie;

        public MovieService(ApplicationDbContext context):base(context)
        {
            _movie = context.Set<Movie>();
        }

        



        public async Task<IEnumerable<Movie>> GetAll(byte genreId = 0)
        {
             return await _movie.Where(g => g.GenreId == genreId || genreId == 0)
                                          .OrderByDescending(g => g.Rate)
                                          .Include(g => g.Genre)
                                          .ToListAsync();
        }

        
       public async Task<Movie> Search(string movieName)
       {
           return await _movie.Include(g => g.Genre).SingleOrDefaultAsync(m => m.Title == movieName);

       }
    }
}