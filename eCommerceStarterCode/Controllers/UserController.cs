using eCommerceStarterCode.Data;
using eCommerceStarterCode.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace eCommerceStarterCode.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public UserController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet("allUsers")]
        public IActionResult GetAllUsers()
        {
            var users = _context.Users.ToList();
            if (users == null)
            {
                return NotFound();
            }
            return Ok(users);
        }

        [HttpGet(), Authorize]
        public IActionResult GetUserById()
        {
            var userId = User.FindFirstValue("id");
            var user = _context.Users.Find(userId);
            if (user == null)
            {
                return NotFound("User not found");
            }

            try
            {
                var userInfo = _context.Users.Where(u => u.Id == userId).SingleOrDefault();
                return Ok(userInfo);
            }
            catch
            {
                return NotFound();
            }
            
        }

        [HttpPut(), Authorize]
        public IActionResult EditUser([FromBody]User value)
        {
            var userId = User.FindFirstValue("id");
            var user = _context.Users.Find(userId);
            if (user == null)
            {
                return NotFound("User not found");
            }

            var userInfo = _context.Users.Where(u => u.Id == userId).SingleOrDefault();
            _context.Users.Update(value);
            _context.SaveChanges();
            return Ok(user);
        }

        [HttpDelete("{userId}"), Authorize]
        public IActionResult DeleteUser(string userId)
        {

            try
            {
                var userInfo = _context.Users.Where(u => u.UserName == userId).SingleOrDefault();
                _context.Remove(userInfo);
                _context.SaveChanges();
                return Ok();
            }
            catch
            {
                return NotFound();
            }
        
        }
    }
}
