using CRUDEFcore.Model;
using CRUDEFcore.Model.Entity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CRUDEFcore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CCountController : ControllerBase
    {
        private readonly CtegoriesContext _tegoriesContext;
        private readonly IConfiguration _configuration;

        public CCountController(CtegoriesContext tegoriesContext, IConfiguration configuration)
        {
            _tegoriesContext = tegoriesContext;
            _configuration = configuration;
        }

        [HttpPost]
        public IActionResult Registers(UserModel user) {

            if (ModelState.IsValid)
            {
                User users = new User()
                {

                    Email = user.Email,
                    Password = user.Password,
                    RoleId = 2


                };

                
                _tegoriesContext.users.Add(users);
                _tegoriesContext.SaveChanges();

                return Ok();
            }
            return BadRequest();
        }

        [HttpPut]
        public IActionResult authentiction(LoginRequest users)
        {
            if (ModelState.IsValid)
            {
                if (!string.IsNullOrWhiteSpace(users.Email) && !string.IsNullOrWhiteSpace(users.Password))
                {
                    bool isuthenticted = _tegoriesContext.users.Any(
                        u => u.Email.Equals(users.Email) &&
                        u.Password.Equals(users.Password)
                        );
                    string roles = "";
                    if (isuthenticted)
                    {

                        roles = (from user in _tegoriesContext.users
                                 join role in _tegoriesContext.roles
                                 on user.RoleId equals role.Id
                                 where user.Email == user.Email
                                 select role.RoleName)?.FirstOrDefault();

                        var tocen = CreateToken(users.Email, roles);
                        return Ok(tocen);

                    }

                    return Unauthorized();

                }
            }
            return BadRequest();
        }

        private string CreateToken(string email, string roleName)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            string secretKey = _configuration["Api"];
            byte[] key = Encoding.ASCII.GetBytes(secretKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, email),
                     new Claim(ClaimTypes.Role, roleName)
                }),

                Expires = DateTime.UtcNow.AddMinutes(10),

                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),   
                    SecurityAlgorithms.HmacSha256Signature
                )
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
