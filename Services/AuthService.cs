using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MoviesApi.Data;
using MoviesApi.Dtos;

namespace MoviesApi.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _usermanager;


          private readonly JWT _jwt;

          public AuthService(UserManager<ApplicationUser> usermanager,IOptions<JWT> jwt)
          {
            
            _usermanager = usermanager;
            _jwt = jwt.Value;
          }











        public async Task<AuthDto> LoggInAsync(LoggInDto loggInDto)
        {
             var authDto = new AuthDto();

           var user = await _usermanager.FindByEmailAsync(loggInDto.Email);
           if(user is null || !await _usermanager.CheckPasswordAsync(user,loggInDto.Password))
           {
                 authDto.Message = "Password or Email is incorrect";
                 return authDto;
           }
             var listRoles = await _usermanager.GetRolesAsync(user);
             var token = await CreateJwtToken(user);

             authDto.Roles =  listRoles.ToList();
             authDto.Email = user.Email;
             authDto.Username = user.UserName;
             authDto.IsAuthenticated = true;
             authDto.Token = new JwtSecurityTokenHandler().WriteToken(token);
             authDto.ExpiresOn = token.ValidTo;


            return authDto;

        }

        public async Task<AuthDto> RegisterAsync(RegisterDto registerDto)
        {
            if(await _usermanager.FindByEmailAsync(registerDto.Email) is not null)
                return new AuthDto{Message = "Email is already registered!"};

            if(await _usermanager.FindByNameAsync(registerDto.Username) is not null)
              return new AuthDto{Message = "Username is already registered!" };

              var user = new ApplicationUser{
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                Email = registerDto.Email,
                UserName = registerDto.Username

              };


              var result = await _usermanager.CreateAsync(user,registerDto.Password);

              if(!result.Succeeded)
              {
                    var errors = string.Empty;
                    foreach (var error in result.Errors)
                    {
                        errors += $"{error.Description},";
                    }

                return new AuthDto{Message = errors};

              }

              await _usermanager.AddToRoleAsync(user,"User");

                    var jwtSecurityToken = await CreateJwtToken(user);

            return new AuthDto
            {
                Email = user.Email,
                ExpiresOn = jwtSecurityToken.ValidTo,
                IsAuthenticated = true,
                Roles = new List<string> { "User" },
                Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                Username = user.UserName
            };


        }


         private async Task<JwtSecurityToken> CreateJwtToken(ApplicationUser user)
        {
            var userClaims = await _usermanager.GetClaimsAsync(user);
            var roles = await _usermanager.GetRolesAsync(user);
            var roleClaims = new List<Claim>();

            foreach (var role in roles)
                roleClaims.Add(new Claim("roles", role));

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid", user.Id)
            }
            .Union(userClaims)
            .Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                claims: claims,
                expires: DateTime.Now.AddDays(_jwt.DurationInDays),
                signingCredentials: signingCredentials );

            return jwtSecurityToken;
        }

    }
}