using eCommerceStarterCode.Data;
using eCommerceStarterCode.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerceStarterCode.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetailController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public OrderDetailController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/<OrderDetailController>
        [HttpGet]
        public IActionResult Get()
        {
            var orderDetail = _context.OrderDetail;
            return Ok(orderDetail);
        }

        // GET api/<OrderDetailController>/order/5
        [HttpGet("order/{id}"), Authorize]
        public IActionResult GetOrderDetailById(int id)
        {
            try
            {
                var orderDetail = _context.OrderDetail.Where(oh => oh.OrderId == id);
                return Ok(orderDetail);
            }
            catch
            {
                return NotFound();
            }
        }

        // GET api/<OrderDetailController>/product/5   
        // temproarily removed Authorize from the [HttpGet(), Authorize]
        [HttpGet("product/{id}")]
        public IActionResult GetOrderDetailByProduct(int id)
        {
            try
            {
                var orderDetail = _context.OrderDetail.Where(oh => oh.ProductId == id);
                return Ok(orderDetail);
            }
            catch
            {
                return NotFound();
            }
        }

        // POST api/<OrderDetailController>
        [HttpPost, Authorize]
        public IActionResult Post([FromBody] OrderDetail value)
        {
            _context.OrderDetail.Add(value);
            _context.SaveChanges();
            return StatusCode(201, value);
        }

        // PUT api/<OrderDetailController>/5
        [HttpPut("{id}"), Authorize]
        public IActionResult Put(int id, [FromBody] OrderDetail value)
        {
            var OrderId = _context.OrderDetail.Where(oh => oh.OrderId == id).SingleOrDefault();
            _context.OrderDetail.Update(value);
            _context.SaveChanges();
            return StatusCode(200, value);
        }

        // DELETE api/<OrderDetailController>/5
        [HttpDelete("{id}"), Authorize]
        public IActionResult Delete(int id)
        {
            // May need to remove the SingleOrDefault method inorder to remove all products for an Order
            try
            {
                var orderDetails = _context.OrderDetail.Where(od => od.OrderId == id).SingleOrDefault();
                _context.Remove(orderDetails);
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
