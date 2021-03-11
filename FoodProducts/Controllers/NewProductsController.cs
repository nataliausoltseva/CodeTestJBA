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
    public class NewProductsController : ControllerBase
    {
        private readonly FoodProductsContext _context;

        public NewProductsController(FoodProductsContext context)
        {
            _context = context;
        }

        // GET: api/NewProducts
        [HttpGet]
        public async Task<ActionResult> GetNewProducts()
        {
            return Ok(await _context.NewProducts.ToListAsync());
        }

        // GET: api/NewProducts/5
        [HttpGet("{id}")]
        public async Task<ActionResult> GetNewProducts(int id)
        {
            var newProducts = await _context.NewProducts.FindAsync(id);

            if (newProducts == null)
            {
                return NotFound();
            }

            return Ok(newProducts);
        }
        // POST: api/NewProducts
        [HttpPost]
        public async Task<ActionResult> PostNewProducts(NewProducts newProducts)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _context.NewProducts.Add(newProducts);
            await _context.SaveChangesAsync();

            return Ok(newProducts);
        }

        // DELETE: api/NewProducts/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteNewProducts(int id)
        {
            var newProducts = await _context.NewProducts.FindAsync(id);
            if (newProducts == null)
            {
                return NotFound();
            }

            _context.NewProducts.Remove(newProducts);
            await _context.SaveChangesAsync();

            return Ok(newProducts);
        }
    }
}
