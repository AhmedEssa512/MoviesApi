using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MoviesApi.Dtos;
using MoviesApi.Services;

namespace MoviesApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {

                 private readonly IUnitOfWork _unitofwork;

        public AuthController(IUnitOfWork unitofwork)
        {
            _unitofwork = unitofwork;
           
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody]RegisterDto registerDto)
        {
            if (registerDto is null)
            {
                throw new ArgumentNullException(nameof(registerDto));
            }

            if (!ModelState.IsValid)
               return BadRequest(ModelState);

            var result = await _unitofwork.authService.RegisterAsync(registerDto);
            if(!result.IsAuthenticated)
               return BadRequest(result.Message);


            return Ok(result);
        }


        [HttpPost("loggIn")]
        public async Task<IActionResult> LoggIn([FromBody]LoggInDto loggInDto)
        {
            if(!ModelState.IsValid)
             return BadRequest(ModelState);

             var result = await _unitofwork.authService.LoggInAsync(loggInDto);

             if(!result.IsAuthenticated)
                return BadRequest(result.Message);

                return Ok(result);
        }
    
        
    
    }
}