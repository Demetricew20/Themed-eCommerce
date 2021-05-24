using eCommerceStarterCode.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using eCommerceStarterCode.Models;

namespace eCommerceStarterCode.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public ProductsController(ApplicationDbContext context)
        {
            _context = context;
        }
        // <baseurl>/api/products
        [HttpGet]
        public IActionResult Get()
        {
            var product = _context.Products.ToList();
            return Ok(product);
        }

        [HttpGet("{productId}")]
        public IActionResult GetById(int productId)
        {
            var product = _context.Products.Where(p => p.ProductId == productId).SingleOrDefault();
            return Ok(product);
        }

        // POST /api/product
        [HttpPost, Authorize]
        public IActionResult Post([FromBody]Product value)
        {
            _context.Products.Add(value);
            _context.SaveChanges();
            return StatusCode(201, value);
        }
    }
}
