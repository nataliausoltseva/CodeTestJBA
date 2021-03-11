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
    public class MailingListsController : ControllerBase
    {
        private readonly FoodProductsContext _context;

        public MailingListsController(FoodProductsContext context)
        {
            _context = context;
        }

        // GET: api/MailingLists
        [HttpGet]
        public async Task<ActionResult> GetMailingLists()
        {
            return Ok(await _context.MailingList.ToListAsync());
        }

        // GET: api/MailingLists/5
        [HttpGet("{id}")]
        public async Task<ActionResult> GetMailingList(int id)
        {
            var mailingList = await _context.MailingList.FindAsync(id);

            if (mailingList == null)
            {
                return NotFound();
            }

            return Ok(mailingList);
        }

        // POST: api/MailingLists
        [HttpPost]
        public async Task<ActionResult> PostMailingList(MailingList mailingList)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _context.MailingList.Add(mailingList);
            await _context.SaveChangesAsync();

            return Ok(mailingList);
        }

        // DELETE: api/MailingLists/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteMailingList(int id)
        {
            var mailingList = await _context.MailingList.FindAsync(id);
            if (mailingList == null)
            {
                return NotFound();
            }

            _context.MailingList.Remove(mailingList);
            await _context.SaveChangesAsync();

            return Ok(mailingList);
        }
    }
}
