using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace MoviesApi.Data
{
    public class ApplicationUser:IdentityUser
    {
        [MaxLength(50),Required]
        public string FirstName { get; set; }

        [MaxLength(50),Required]
        public string LastName { get; set; }
   
    }
}