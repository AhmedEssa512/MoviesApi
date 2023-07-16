using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using MoviesApi.Data;
using MoviesApi.Dtos;

namespace MoviesApi.Services
{
    public class UnitOfWork : IUnitOfWork
    {

          private readonly ApplicationDbContext _context;
          private readonly UserManager<ApplicationUser> _usermanager;
           private readonly   IOptions<JWT> _jwt;

        public UnitOfWork(ApplicationDbContext context,UserManager<ApplicationUser> usermanager,IOptions<JWT> jwt)
        {
            _context = context;
            _usermanager = usermanager;
            _jwt = jwt;
            genreService = new GenreService(_context);
            moviesService = new MovieService(_context);
            authService = new AuthService(_usermanager,_jwt);
        }
        public IGenreService genreService {get;private set;}

        public IMoviesService moviesService {get;private set;}
        public IAuthService authService {get;private set;}


        public int CommitChanges()
        {
          return  _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}