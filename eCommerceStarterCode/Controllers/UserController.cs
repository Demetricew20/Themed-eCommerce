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
        [HttpGet("get-user"), Authorize]
        public IActionResult GetCurrentUser()
        {
            var userId = User.FindFirstValue("id");
            var user = _context.Users.Find(userId);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }
        [HttpPost("set-role")]
        //Add Parameters once front end is built
        public IActionResult GetRoleStatus()
        {
            // Hard coded userId for testing
            string userId = "75777217-c7ad-40f2-bf1c-92cdd2b0e0a0";
            bool isSupplier = _context.Users.Where(u => u.Id == userId).Select(u => u.IsSupplier).SingleOrDefault();
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
                        UserId = _context.Users.Where(u => u.Id == userId).Select(u => u.Id).SingleOrDefault(),
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
                        UserId = _context.Users.Where(u => u.Id == userId).Select(u => u.Id).SingleOrDefault(),
                        RoleId = 2
                    };
                    _context.UserRoles.Add(newUserRole);
                    _context.SaveChanges();
                    return Ok(newUserRole.RoleId);

                }

            }
            return NotFound("Staus not set");
                
        }
    }
}
