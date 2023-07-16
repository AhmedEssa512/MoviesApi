using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesApi.Dtos
{
    public class MoviesDtos
    {
         [MaxLength(200)]
        public string Title { get; set; }

        public double Rate { get; set; }
        public int Year { get; set; }
        
        [MaxLength(2500)]
        public string StroreLine { get; set; }

        public IFormFile Poster { get; set; }

        public byte GenreId { get; set; }
    }
}