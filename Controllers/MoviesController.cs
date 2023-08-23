using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesApi.Data;
using MoviesApi.Dtos;
using MoviesApi.Services;

namespace MoviesApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class MoviesController : ControllerBase
    {
        

        private readonly IUnitOfWork _myunit;


        public MoviesController(IUnitOfWork myunit)
        {

            _myunit = myunit;

        }

        [HttpPost]
        public async Task<IActionResult> CreateMovie([FromForm]MoviesDtos dtos)
        {
            if(dtos.Poster == null)
              return BadRequest("poster Must be found");

            var isvalisid = await _myunit.genreService.IsvalidGenre(dtos.GenreId);

            if(!isvalisid)
              return BadRequest($"Not Found Genre with this id : {dtos.GenreId}");

               using var  DataStream = new MemoryStream();
               await dtos.Poster.CopyToAsync(DataStream);
            
          var movie = new Movie
          {
            Title = dtos.Title,
            Rate  = dtos.Rate,
            Year  = dtos.Year,
            StroreLine  = dtos.StroreLine,
            GenreId = dtos.GenreId,
            Poster = DataStream.ToArray()
          };
      
             
            await _myunit.moviesService.AddAsync(movie);

          return Ok(movie);

        }

        [HttpGet]
        public async Task<IActionResult> GetAllMovies()
        {
            var movies = await _myunit.moviesService.GetAll();
            
            return Ok(movies);

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            
             var movie = await _myunit.moviesService.GetByIdAsync(id);

             if(movie == null) return NotFound("Not found movie with this id");
            
               
             await _myunit.moviesService.DeleteAsync(movie);

             return Ok(movie);

        }


        [HttpPut("{id}")]
         public async Task<IActionResult> UpdateMovie(int id,[FromForm]MoviesDtos dto)
         {
             var movie = await _myunit.moviesService.GetByIdAsync(id);

             if(movie == null) return NotFound("Not found movie with this id");

             if(dto.Poster != null)
              {
                using var DataStream = new MemoryStream();
                await dto.Poster.CopyToAsync(DataStream);
                movie.Poster = DataStream.ToArray();
                
              }

           
               movie.Title = dto.Title;
               movie.Rate = dto.Rate;
               movie.Year = dto.Year;
               movie.StroreLine = dto.StroreLine;
               movie.GenreId = dto.GenreId;
            
            

            await  _myunit.moviesService.UpdateAsync(movie);


          return Ok(movie);


         }

         [HttpGet]
         [Route("api/[controller]/[action]")] 
         public async Task<IActionResult>Search(string movieName)
         { 
           
            var movie = await _myunit.moviesService.Search(movieName);

             if(movie is null || movieName == "")
              return BadRequest($"Not Found movie with this name \"{movieName}\"");


             return Ok(movie);
         }

        

    }
}