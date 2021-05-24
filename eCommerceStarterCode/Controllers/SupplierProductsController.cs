using eCommerceStarterCode.Data;
using eCommerceStarterCode.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace eCommerceStarterCode.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierProductsController : ControllerBase
    {
        public ApplicationDbContext _context;
        public SupplierProductsController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet, Authorize]
        public IActionResult GetSupplierProducts()
        {
            var supplierProducts = _context.SupplierProducts;

            if (supplierProducts == null)
            {
                return NotFound();
            }
            return Ok(supplierProducts);
        }

        [HttpPost, Authorize]
        public IActionResult PostSupplierProducts([FromBody] SupplierProduct value)
        {
            _context.SupplierProducts.Add(value);
            _context.SaveChanges();
            return StatusCode(201, value);
        }

        [HttpDelete("{userid}/{productid}"), Authorize]
        public IActionResult DeleteSupplierProduct(int productid)
        {
            var userId = User.FindFirstValue("id");
            var user = _context.Users.Find(userId);
            if (user == null)
            {
                return NotFound("User not found");
            }
            
            try
            {
                var compoundKey = new { userId, productid };
                var selectedObject = _context.SupplierProducts.Where(p => ( p.UserId == userId && p.ProductId == productid )).SingleOrDefault();
                _context.Remove(selectedObject);
                _context.SaveChanges();
                return Ok("product removed");
            }
            catch
            {
                return NotFound("no product found");
            }
        }
    }
}
