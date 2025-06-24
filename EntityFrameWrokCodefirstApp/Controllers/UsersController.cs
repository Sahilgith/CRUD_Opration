using System.Runtime.CompilerServices;
using EntityFrameWrokCodefirstApp.Data;
using EntityFrameWrokCodefirstApp.DTO;
using EntityFrameWrokCodefirstApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace EntityFrameWrokCodefirstApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly AppDbContext _context;
        public UsersController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("GetUsers")]

        public async Task<ActionResult<List<UserDto>>> GetUser() {
            var users = await _context.Users
                .Select(u => new UserDto
                {
                    Id = u.Id,
                    Name = u.Name,
                    ContactNo = u.ContactNo,
                }).ToListAsync();

            return Ok(users); 
        }

        [HttpGet]
        [Route("GetUser")]

        public async Task<ActionResult<UserDto>> GetUser(int id) { 
            var user = await _context.Users.FindAsync(id);
            if (user == null) return NotFound();

            return new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                ContactNo = user.ContactNo,
            };
        }

        [HttpPost]
        [Route("AddUser")]
        public async Task<ActionResult<UserDto>> CreateUser(CreateUserDto dto) {

            var user = new Users
            {
                Name = dto.Name,
                ContactNo = dto.ContactNo,
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
             return CreatedAtAction(nameof (CreateUser), new {id = user.Id} , new UserDto{ 
                 Id = user.Id,  
                 Name   = user.Name, 
                 ContactNo = user.ContactNo,    

            });
        }

        [HttpPut]
        [Route("UpdateUser")]
        public async Task<IActionResult> UpdateUser(int id, CreateUserDto dto) {
            var user = await _context.Users.FindAsync(id);
            if(user == null) return NotFound(); 
            
            user.Name = dto.Name;
            user.ContactNo = dto.ContactNo;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete]
        [Route("DeleteUser")]

        public async Task<IActionResult> DeleteUser(int id) {
            var user = await _context.Users.FindAsync(id);
            if(user == null) return NotFound(); 

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
             return NoContent();    
        }


        


    }
}
