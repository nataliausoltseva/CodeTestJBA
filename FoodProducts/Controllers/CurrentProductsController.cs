using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FoodProducts.Models;

namespace FoodProducts.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrentProductsController : ControllerBase
    {
        private readonly FoodProductsContext _context;

        public CurrentProductsController(FoodProductsContext context)
        {
            _context = context;
        }

        // GET: api/CurrentProducts
        [HttpGet]
        public async Task<ActionResult> GetCurrentProducts()
        {
            return Ok(await _context.CurrentProducts.ToListAsync());
        }

        // GET: api/CurrentProducts/5
        [HttpGet("{id}")]
        public async Task<ActionResult> GetCurrentProducts(int id)
        {
            var currentProducts = await _context.CurrentProducts.FindAsync(id);

            if (currentProducts == null)
            {
                return NotFound();
            }

            return Ok(currentProducts);
        }

        // POST: api/CurrentProducts
        [HttpPost]
        public async Task<ActionResult> PostCurrentProducts(CurrentProducts currentProducts)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            _context.CurrentProducts.Add(currentProducts);
            await _context.SaveChangesAsync();

            return Ok(currentProducts);
        }

        // DELETE: api/CurrentProducts/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCurrentProducts(int id)
        {
            var currentProducts = await _context.CurrentProducts.FindAsync(id);
            if (currentProducts == null)
            {
                return NotFound();
            }

            _context.CurrentProducts.Remove(currentProducts);
            await _context.SaveChangesAsync();

            return Ok(currentProducts);
        }
    }
}
