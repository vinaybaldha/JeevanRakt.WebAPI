using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JeevanRakt.Core.Domain.Entities;
using JeevanRakt.Infrastructure.DataBase;
using Microsoft.AspNetCore.Authorization;

namespace JeevanRakt.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BloodsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public BloodsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Bloods
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Blood>>> GetBloods()
        {
          if (_context.Bloods == null)
          {
              return NotFound();
          }
            return await _context.Bloods.ToListAsync();
        }

        // GET: api/Bloods/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Blood>> GetBlood(string id)
        {
          if (_context.Bloods == null)
          {
              return NotFound();
          }
            var blood = await _context.Bloods.FindAsync(id);

            if (blood == null)
            {
                return NotFound();
            }

            return blood;
        }

        // PUT: api/Bloods/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBlood(string id, Blood blood)
        {
            if (id != blood.BloodGroup)
            {
                return BadRequest();
            }

            _context.Entry(blood).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BloodExists(id))
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

        // POST: api/Bloods
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Blood>> PostBlood(Blood blood)
        {
          if (_context.Bloods == null)
          {
              return Problem("Entity set 'ApplicationDbContext.Bloods'  is null.");
          }
            _context.Bloods.Add(blood);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (BloodExists(blood.BloodGroup))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetBlood", new { id = blood.BloodGroup }, blood);
        }

        // DELETE: api/Bloods/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBlood(string id)
        {
            if (_context.Bloods == null)
            {
                return NotFound();
            }
            var blood = await _context.Bloods.FindAsync(id);
            if (blood == null)
            {
                return NotFound();
            }

            _context.Bloods.Remove(blood);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("totalbloodgroup")]
        public async Task<ActionResult<int>> GetTotalBloodGroupsCount()
        {
            var totalBloodGroupCount = await _context.Bloods.CountAsync();
            return totalBloodGroupCount;
        }

        private bool BloodExists(string id)
        {
            return (_context.Bloods?.Any(e => e.BloodGroup == id)).GetValueOrDefault();
        }
    }
}
