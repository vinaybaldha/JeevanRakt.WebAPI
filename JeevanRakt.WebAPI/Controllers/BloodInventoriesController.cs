﻿using JeevanRakt.Core.Domain.Entities;
using JeevanRakt.Infrastructure.DataBase;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JeevanRakt.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BloodInventoriesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public BloodInventoriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/BloodInventories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BloodInventory>>> GetBloodInventories()
        {
            if (_context.BloodInventories == null)
            {
                return NotFound();
            }
            return await _context.BloodInventories.ToListAsync();
        }

        // GET: api/BloodInventories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BloodInventory>> GetBloodInventory(Guid id)
        {
            if (_context.BloodInventories == null)
            {
                return NotFound();
            }
            var bloodInventory = await _context.BloodInventories.FindAsync(id);

            if (bloodInventory == null)
            {
                return NotFound();
            }

            return bloodInventory;
        }

        // PUT: api/BloodInventories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBloodInventory(Guid id, BloodInventory bloodInventory)
        {
            if (id != bloodInventory.BloodInventoryId)
            {
                return BadRequest();
            }

            _context.Entry(bloodInventory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BloodInventoryExists(id))
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

        // POST: api/BloodInventories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<BloodInventory>> PostBloodInventory(BloodInventory bloodInventory)
        {
            if (_context.BloodInventories == null)
            {
                return Problem("Entity set 'ApplicationDbContext.BloodInventories'  is null.");
            }
            _context.BloodInventories.Add(bloodInventory);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBloodInventory", new { id = bloodInventory.BloodInventoryId }, bloodInventory);
        }

        // DELETE: api/BloodInventories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBloodInventory(Guid id)
        {
            if (_context.BloodInventories == null)
            {
                return NotFound();
            }
            var bloodInventory = await _context.BloodInventories.FindAsync(id);
            if (bloodInventory == null)
            {
                return NotFound();
            }

            _context.BloodInventories.Remove(bloodInventory);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // total bloodstock count
        [HttpGet("totalbloodstocks")]
        [AllowAnonymous]
        public async Task<IActionResult> TotalBloodStockCount()
        {
            if (_context.BloodInventories == null)
            {
                return NotFound();
            }

            int totalcount = 0;
            
             totalcount = await _context.BloodInventories.SumAsync(x=>
             
                  x.A1 + x.A2 + x.B1 + x.B2 + x.O1 + x.O2 + x.AB1 + x.AB2
             );

            return Ok(totalcount);
        }

        private bool BloodInventoryExists(Guid id)
        {
            return (_context.BloodInventories?.Any(e => e.BloodInventoryId == id)).GetValueOrDefault();
        }
    }
}
