using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MoviesApi.Data;

namespace MoviesApi.Services
{
    public class MovieService : IMoviesService
    {
           private readonly ApplicationDbContext _context;

        public MovieService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Movie> Create(Movie movie)
        {
            await _context.AddAsync(movie);
          await  _context.SaveChangesAsync();     

             return movie;

        }

        public async Task<Movie> Delete(Movie movie)
        {
             _context.Remove(movie);
             await _context.SaveChangesAsync();

             return movie;
        }

        public async Task<IEnumerable<Movie>> GetAll(byte genreId = 0)
        {
             return await _context.Movies.Where(g => g.GenreId == genreId || genreId == 0)
                                          .OrderByDescending(g => g.Rate)
                                          .Include(g => g.Genre)
                                          .ToListAsync();
        }

        public async Task<Movie> GetById(int id)
        {
            return await _context.Movies.Include(p => p.Genre).SingleOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Movie> Update(Movie movie)
        {
             _context.Movies.Update(movie);
            await _context.SaveChangesAsync();
            return movie;
        }

       public async Task<Movie> Search(string movieName)
       {
           return await _context.Movies.Include(g => g.Genre).SingleOrDefaultAsync(m => m.Title == movieName);

       }
    }
}