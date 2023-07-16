using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MoviesApi.Data;

namespace MoviesApi.Services
{
    public interface IUnitOfWork:IDisposable
    {
        IGenreService genreService {get;}
        IMoviesService moviesService  {get;}
        IAuthService authService  {get;}


        int CommitChanges();

    }
}