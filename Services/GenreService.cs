using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MoviesApi.Data;

namespace MoviesApi.Services
{
    public class GenreService : IGenreService
    {
          private readonly ApplicationDbContext _context;

        public GenreService(ApplicationDbContext context)
        {
            _context = context;
        }








        public async Task<Genre> Create(Genre genre)
        {
            await _context.Genres.AddAsync(genre);
            await _context.SaveChangesAsync();

            return genre;
        }

        public async Task<Genre> Delete(Genre genre)
        {
            _context.Genres.Remove(genre);
            await _context.SaveChangesAsync();

            return genre;
        }

        public async Task<IEnumerable<Genre>> GetAll()
        {
            return   await _context.Genres.OrderBy(g => g.Name).ToListAsync();   
        }

        public async Task<Genre> GetById(int id)
        {
            return await _context.Genres.SingleOrDefaultAsync(g => g.Id == id);
        }

        public Task<bool> IsvalidGenre(byte id)
        {
            return   _context.Genres.AnyAsync(g => g.Id == id);
        }

        public async Task<Genre> Update(Genre genre)
        {
            _context.Genres.Update(genre);
            await _context.SaveChangesAsync();
            return genre;
        }
    }
}