using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MoviesApi.Dtos;

namespace MoviesApi.Services
{
    public interface IAuthService
    {
        
        Task<AuthDto> RegisterAsync(RegisterDto registerDto);
        Task<AuthDto> LoggInAsync(LoggInDto loggInDto);
    }
}