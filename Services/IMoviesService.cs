using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MoviesApi.Data;
using MoviesApi.Generics;

namespace MoviesApi.Services
{
    public interface IMoviesService : IGenericBase<Movie>
    {
       Task< IEnumerable<Movie>> GetAll(byte genreId = 0);

        Task<Movie>Search(string movieName);

    }
}