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
    [Route("api/ShoppingCart")]
    [ApiController]
    public class ShoppingCartController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ShoppingCartController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet("{id}/cart"), Authorize]
        public IActionResult GetCurrentUserCart(string id)
        {
            var userCart = _context.ShoppingCarts.Where(u => u.UserId == id).Select(u => new { u.ProductId, u.Quantity });
            return Ok(userCart);
        }

        [HttpPost("{id}/postCart"), Authorize]

        public IActionResult Post([FromBody] ShoppingCart value)
        {
            _context.ShoppingCarts.Add(value);
            _context.SaveChanges();
            return StatusCode(201, value);
        }

        [HttpDelete("{id}/{productId}/delete"), Authorize ]
        public IActionResult Delete(string id, int productId)
        {

            var item = _context.ShoppingCarts.Where(u => (u.UserId == id && u.ProductId == productId)).SingleOrDefault();
            _context.ShoppingCarts.Remove(item);
            _context.SaveChanges();
            return Ok();
        }

        [HttpPut("{id}/{productId}/edit"), Authorize]

        public IActionResult Put(string id, int productId, [FromBody]ShoppingCart value)
        {
            var item = _context.ShoppingCarts.Where(u => (u.UserId == id && u.ProductId == productId)).SingleOrDefault();
            _context.ShoppingCarts.Remove(item);
            _context.SaveChanges();
            _context.ShoppingCarts.Add(value);
            return Ok(value);

        }



        
    }
}
