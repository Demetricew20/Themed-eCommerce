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
        [HttpGet("allUsers"), Authorize]
        public IActionResult GetAllUsers()
        {
            var users = _context.Users.ToList();
            if (users == null)
            {
                return NotFound();
            }
            return Ok(users);
        }

        [HttpGet("{id}/get"), Authorize]
        public IActionResult GetUserById(string id)
        {
            try
            {
                var userId = _context.Users.Where(u => u.Id == id).SingleOrDefault();
                return Ok(userId);
            }
            catch
            {
                return NotFound();
            }
            
        }

        [HttpDelete("{id}/delete"), Authorize]
        public IActionResult DeleteUser(string id)
        {
            
            try
            {
                var user = _context.Users.Where(u => u.Id == id).SingleOrDefault();
                _context.Remove(user);
                _context.SaveChanges();
                return Ok();
            }
            catch
            {
                return NotFound();
            }
        
        }


        [HttpPost("{id}/setRole"), Authorize]
        public IActionResult GetRoleStatus(string id)
        {
            var userId = _context.Users.Where(u => u.Id == id).SingleOrDefault();
            bool isSupplier = _context.Users.Where(u => u.Id == id).Select(u => u.IsSupplier).SingleOrDefault();
            var user = _context.Users.Find(userId);
            if (user == null)
            {
                return NotFound();
            }
            else
            {
                if (!isSupplier)
                {
                    UserRole newUserRole = new UserRole()
                    {
                        UserId = _context.Users.Where(u => u.Id == id).Select(u => u.Id).SingleOrDefault(),
                        RoleId = 1
                    };
                    _context.UserRoles.Add(newUserRole);
                    _context.SaveChanges();
                    return Ok(newUserRole.RoleId);

                }
                else if (isSupplier)
                {
                    UserRole newUserRole = new UserRole()
                    {
                        UserId = _context.Users.Where(u => u.Id == id).Select(u => u.Id).SingleOrDefault(),
                        RoleId = 2
                    };
                    _context.UserRoles.Add(newUserRole);
                    _context.SaveChanges();
                    return Ok(newUserRole.RoleId);

                }

            }
            return NotFound("Status not set");
                
        }
    }
}
