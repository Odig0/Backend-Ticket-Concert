using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using TicketConcerts.Data;
using TicketConcerts.Models;
using TicketConcerts.Models.Entities;

namespace TicketConcerts.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;

        public UsersController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        [HttpGet]
        public IActionResult GetAllUser()
        {
            var allUsers = dbContext.Users.ToList();

            return Ok(allUsers);
        }

        [HttpGet]
        [Route("{id:int}")]
        public IActionResult GetUser(int id)
        {
            var userById = dbContext.Users.Find(id);

            if (userById is null) {
                return NotFound("No se encontro el usuario");
            }

            return Ok(userById);
        }
        [HttpPost]
        public IActionResult AddUser(AddUserDto addUserDto)
        {

            var userEntity = new User()
            {
                Email = addUserDto.Email,
                Name = addUserDto.Name,
                Password = addUserDto.Password,
                CreatedAt = DateTime.UtcNow,
            };

            dbContext.Users.Add(userEntity);
            dbContext.SaveChanges();
            
            return Ok(userEntity);
        }
        [HttpPut]
        [Route("{id:guid}")]
        public IActionResult UpdateUser(int id, UpdateUserDto updateUserDto)
        {
            var user = dbContext.Users.Find(id);
            if (user is null)
            {
                return NotFound("No se encontro al usuario");
            }

            user.Name = updateUserDto.Name;
            user.Email = updateUserDto.Email;
            user.Password = updateUserDto.Password;

            dbContext.SaveChanges();

            return Ok(user);
        }
        [HttpDelete]
        public IActionResult DeleteUser(int id)
        {
            var userEntity = dbContext.Users.Find(id);
            if (userEntity is null)
            {
                return NotFound("No se encontro el usuaruio");
            }
            dbContext.Users.Remove(userEntity); dbContext.SaveChanges();
            dbContext.SaveChanges();
            return Ok("Usuario eliminado con exito");
        }

    }
}
