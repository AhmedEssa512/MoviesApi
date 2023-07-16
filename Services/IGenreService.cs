using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MoviesApi.Data;

namespace MoviesApi.Services
{
    public interface IGenreService
    {

        Task< IEnumerable<Genre>>GetAll();
        Task<Genre>GetById(int id);

        Task<Genre> Create(Genre genre);

        Task<Genre> Delete(Genre genre);

        Task<Genre> Update(Genre genre);

         Task<bool> IsvalidGenre(byte id);




    }
}