using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Exceptions;
using DomainLayer.Models.IdentityModule;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ServicesAbstraction;
using Shared.DataTransferObjects.IdentityModuleDtos;

namespace Services
{
    public class AuthenticationService(UserManager<ApplicationUser> _userManager, IConfiguration _configuration) : IAuthenticationService
    {
        public async Task<UserDto> LoginAsync(LoginDto loginDto)
        {
            var User = await _userManager.FindByEmailAsync(loginDto.Email) ?? throw new UserNotFoundException(loginDto.Email);
            var IsPasswordValid = await _userManager.CheckPasswordAsync(user: User, password: loginDto.Password);
            if (IsPasswordValid)
            {
                return new UserDto()
                {
                    DisplayName = User.DisplayName,  
                    Email = User.Email,
                    Token = await CreateTokenAsync(User)
                };

            }
            else
            {
                throw new UnauthorizedException();
            }
        }

       
        public async Task<UserDto> RegisterAsync(RegisterDto registerDto)
        {
            var User = new ApplicationUser()
            {
                DisplayName = registerDto.DisplayName,
                Email = registerDto.Email,
                PhoneNumber = registerDto.PhoneNumber,
                UserName = registerDto.UserName,
            };
            var Result = await _userManager.CreateAsync(User,registerDto.Password);
            if (Result.Succeeded)
            {
                return new UserDto()
                {
                    DisplayName = User.DisplayName,
                    Email = User.Email,
                    Token  =await CreateTokenAsync(User)
                };
            }
            else
            {
                var Errors = Result.Errors.Select(E=>E.Description).ToList();
                throw new BadRequestException(errors: Errors);
            }
        }
        private async Task<string> CreateTokenAsync(ApplicationUser user)
        {
            var Claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Email, user.Email!),
                new Claim(ClaimTypes.Name,user.UserName!),
                new Claim(ClaimTypes.NameIdentifier,user.Id)

               
            };

            var Roles = await _userManager.GetRolesAsync(user);

            foreach (var role in Roles)
            {
                Claims.Add(new Claim(type: ClaimTypes.Role,value: role));
            }

            var SecretKey = _configuration.GetSection(key: "JWTOptions")[key:"SecretKey"];
            var Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));

            var Creds = new SigningCredentials(key: Key, algorithm: SecurityAlgorithms.HmacSha256);

            var Token = new JwtSecurityToken(
            
                issuer:_configuration[key:"JWTOptions:Issuer"],
                audience: _configuration[key:"JWTOptions:Audience"],
                claims: Claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials : Creds 
                );

            return new JwtSecurityTokenHandler().WriteToken(Token);

        }

    }
}
