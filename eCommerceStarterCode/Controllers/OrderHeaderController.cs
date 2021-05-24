using eCommerceStarterCode.Data;
using eCommerceStarterCode.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace eCommerceStarterCode.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderHeaderController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public OrderHeaderController(ApplicationDbContext context) 
        {
            _context = context;
        }

        // GET: api/<OrderHeaderController>
        [HttpGet, Authorize]
        public IActionResult Get()
        {
            var orderHeader = _context.OrderHeader;
            return Ok(orderHeader);
        }

        // GET api/<OrderHeaderController>/5
        [HttpGet("{id}"), Authorize]
        public IActionResult GetOrderById(int id)
        {
            try
            {
                var OrderId = _context.OrderHeader.Where(oh => oh.OrderId == id).SingleOrDefault();
                return Ok(OrderId);
            }
            catch
            {
                return NotFound();
            }
        }

        // POST api/<OrderHeaderController>
        [HttpPost, Authorize]
        public IActionResult Post([FromBody]OrderHeader value)
        {
            _context.OrderHeader.Add(value);
            _context.SaveChanges();
            return StatusCode(201, value);
        }

        // PUT api/<OrderHeaderController>/5
        [HttpPut("{id}"), Authorize]
        public IActionResult Put(int id, [FromBody]OrderHeader value)
        {
            var OrderId = _context.OrderHeader.Where(oh => oh.OrderId == id).SingleOrDefault();
            _context.OrderHeader.Update(value);
            _context.SaveChanges();
            return StatusCode(200, value);
        }

        // DELETE api/<OrderHeaderController>/5
        [HttpDelete("{id}"), Authorize]
        public IActionResult Delete(int id)
        {

            try
            {
                var orderHeader = _context.OrderHeader.Where(oh => oh.OrderId == id).SingleOrDefault();
                _context.Remove(orderHeader);
                
                // If cascade delete is not automatic ... add detail delete here.

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
