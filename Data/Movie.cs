using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesApi.Data
{
    public class Movie
    {
        public int Id { get; set; }

        [MaxLength(200)]
        public string Title { get; set; }

        public double Rate { get; set; }
        public int Year { get; set; }
        
        [MaxLength(2500)]
        public string StroreLine { get; set; }

        public byte[] Poster { get; set; }

        public byte GenreId { get; set; }
        public Genre Genre { get; set; }
    }
}