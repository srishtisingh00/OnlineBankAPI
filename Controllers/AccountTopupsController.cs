using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineBankAPI.Models;

namespace OnlineBankAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountTopupsController : ControllerBase
    {
        private readonly AppDBContext _context;

        public AccountTopupsController(AppDBContext context)
        {
            _context = context;
        }

        // GET: api/AccountTopups
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AccountTopup>>> GetAccountTopup()
        {
            return await _context.AccountTopup.ToListAsync();
        }

        // GET: api/AccountTopups/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AccountTopup>> GetAccountTopup(int id)
        {
            var accountTopup = await _context.AccountTopup.FindAsync(id);

            if (accountTopup == null)
            {
                return NotFound();
            }

            return accountTopup;
        }

        // PUT: api/AccountTopups/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAccountTopup(int id, AccountTopup accountTopup)
        {
            if (id != accountTopup.Id)
            {
                return BadRequest();
            }

            _context.Entry(accountTopup).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AccountTopupExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/AccountTopups
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AccountTopup>> PostAccountTopup(AccountTopup accountTopup)
        {
            _context.AccountTopup.Add(accountTopup);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAccountTopup", new { id = accountTopup.Id }, accountTopup);
        }

        // DELETE: api/AccountTopups/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccountTopup(int id)
        {
            var accountTopup = await _context.AccountTopup.FindAsync(id);
            if (accountTopup == null)
            {
                return NotFound();
            }

            _context.AccountTopup.Remove(accountTopup);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AccountTopupExists(int id)
        {
            return _context.AccountTopup.Any(e => e.Id == id);
        }
    }
}
