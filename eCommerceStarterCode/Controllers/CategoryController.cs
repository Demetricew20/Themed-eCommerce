﻿using eCommerceStarterCode.Data;
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
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        public ApplicationDbContext _context;
        public CategoryController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet, Authorize]
        public IActionResult GetCategories()
        {
            var catergories = _context.CategoryClass;
            
            if (catergories == null)
            {
                return NotFound();
            }
            return Ok(catergories);
        }

        [HttpPost, Authorize]
        public IActionResult Post([FromBody] CategoryTable value)
        {
            _context.CategoryClass.Add(value);
            _context.SaveChanges();
            return StatusCode(201, value);
        }
    }

}
