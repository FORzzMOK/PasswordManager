using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using PasswordManager.Models;
using Microsoft.Extensions.Configuration;
using System.Text;

namespace TokenApp.Controllers
{
    public class AccountController : Controller
    {
        private List<User> people = new List<User>
        {
        new User { Login="admin@gmail.com", Password="12345", Role = Roles.Admin },
        new User { Login="qwerty@gmail.com", Password="55555", Role = Roles.User }
        };

        private readonly IConfiguration _configuration;

        public AccountController(IConfiguration config)
        {
            _configuration = config;
        }

        [HttpPost(nameof(Token))]
        public IActionResult Token(string login, string password)
        {
            if (login != null && password != null)
            {
                var user = people.SingleOrDefault(i => i.Login == login && i.Password == password);

                if (user != null)
                {
                    var claims = new[] {
                        new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim(ClaimsIdentity.DefaultNameClaimType, user.Login),
                        new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role.ToString())
                    };

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(
                        _configuration["Jwt:Issuer"],
                        _configuration["Jwt:Audience"],
                        claims,
                        expires: DateTime.UtcNow.AddMinutes(100),
                        signingCredentials: signIn);

                    return Ok($"Bearer {new JwtSecurityTokenHandler().WriteToken(token)}");
                }
                else
                {
                    return BadRequest("Invalid credentials");
                }
            }
            else
            {
                return BadRequest();
            }
        }
    }
}