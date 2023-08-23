using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MoviesApi.Data;
using MoviesApi.Generics;

namespace MoviesApi.Services
{
    public interface IGenreService : IGenericBase<Genre>
    {

        Task< IEnumerable<Genre>>GetAll();

         Task<bool> IsvalidGenre(byte id);




    }
}