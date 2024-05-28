using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GestiondeEventos.Context;
using GestiondeEventos.Models;
using Microsoft.AspNetCore.Cors;
using GestiondeEventos.Models.Dtos;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace GestiondeEventos.Controllers
{
    [EnableCors("ReglasCORS")]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly AppDbContext _context;
        private string clave;

        public UsersController(AppDbContext context, IConfiguration config)
        {
            _context = context;
            clave = config.GetValue<string>("ApiSettings:Secreta");
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: api/Users/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.id)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Users
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.id }, user);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.id == id);
        }

        [HttpPost]
        [Route("login")]
        public UserLoginTokenDTO Login(UserDTO userDto)
        {
            var user = _context.Users.FirstOrDefault(
                u => u.email.ToLower() == userDto.email.ToLower()
                && u.password == userDto.password);

            if(user == null)
            {
                return new UserLoginTokenDTO()
                {
                    Token = "",
                    User = null

                };
            }

            var tok = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(clave);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.name.ToString()),
                    new Claim(ClaimTypes.Email, user.email.ToString()),
                    new Claim(ClaimTypes.Role, user.role.ToString())

                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tok.CreateToken(tokenDescriptor);

            UserLoginTokenDTO uT = new UserLoginTokenDTO()
            { 
                Token = tok.WriteToken(token),
                User = user
            };

            return uT;
        }

    }
}
