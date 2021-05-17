﻿using eCommerceStarterCode.Data;
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
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        public ApplicationDbContext _context;
        public CategoryController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet("user"), Authorize]
        public IActionResult GetCurrentUser()
        {
            var username = User.FindFirstValue(ClaimTypes.Name);
            var user = _context.Users.Where(u => u.UserName == username).SingleOrDefault();
            if (user == null)
            {
                return Ok("user does not exist");
            }
            return Ok(user);
        }
    }
}
