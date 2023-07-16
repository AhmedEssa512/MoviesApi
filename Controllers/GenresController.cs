using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MoviesApi.Data;
using Microsoft.EntityFrameworkCore;
using MoviesApi.Dtos;
using MoviesApi.Services;
using Microsoft.AspNetCore.Authorization;

namespace MoviesApi.Controllers
{
    
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class GenresController : ControllerBase
    {
        
        private readonly IUnitOfWork _MyUnit;


        public GenresController(IUnitOfWork MyUnit)
        {
            
            _MyUnit = MyUnit;
        }

        [HttpGet]

        public async Task<IActionResult> GetAll()
        {

             var genres = await _MyUnit.genreService.GetAll();

             return Ok(genres);
        }


        [HttpPost]
        public async Task<IActionResult> CreateGenre(between ob)
        {
            var genre = new Genre{Name = ob.Name};
        
             await _MyUnit.genreService.Create(genre);

            return Ok(genre);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGenre(int id,[FromForm]between ob)
        {
            var genre = await _MyUnit.genreService.GetById(id);

            if(genre == null)
             return NotFound($"Not Found Genres with id {id}");

            genre.Name = ob.Name;
            await _MyUnit.genreService.Update(genre);

            return Ok(genre);     



        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGenre(int id)
        {
           var genre = await _MyUnit.genreService.GetById(id);
           if(genre == null)
            return NotFound($"Not found Gnere with id {id} ");
            
          await _MyUnit.genreService.Delete(genre);

           return Ok(genre);  

        }
        

        

       




    }
}

//   
