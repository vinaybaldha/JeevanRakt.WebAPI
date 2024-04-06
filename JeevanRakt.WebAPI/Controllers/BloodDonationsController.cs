using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JeevanRakt.Core.Domain.Entities;
using JeevanRakt.Infrastructure.DataBase;

namespace JeevanRakt.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BloodDonationsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public BloodDonationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/BloodDonations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BloodDonation>>> GetBloodDonations()
        {
          if (_context.BloodDonations == null)
          {
              return NotFound();
          }
            return await _context.BloodDonations.ToListAsync();
        }

        // GET: api/BloodDonations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BloodDonation>> GetBloodDonation(Guid id)
        {
          if (_context.BloodDonations == null)
          {
              return NotFound();
          }
            var bloodDonation = await _context.BloodDonations.FindAsync(id);

            if (bloodDonation == null)
            {
                return NotFound();
            }

            return bloodDonation;
        }

        // PUT: api/BloodDonations/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBloodDonation(Guid id, BloodDonation bloodDonation)
        {
            if (id != bloodDonation.DonationId)
            {
                return BadRequest();
            }

            _context.Entry(bloodDonation).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BloodDonationExists(id))
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

        // POST: api/BloodDonations
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<BloodDonation>> PostBloodDonation(BloodDonation bloodDonation)
        {
          if (_context.BloodDonations == null)
          {
              return Problem("Entity set 'ApplicationDbContext.BloodDonations'  is null.");
          }
            _context.BloodDonations.Add(bloodDonation);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBloodDonation", new { id = bloodDonation.DonationId }, bloodDonation);
        }

        // DELETE: api/BloodDonations/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBloodDonation(Guid id)
        {
            if (_context.BloodDonations == null)
            {
                return NotFound();
            }
            var bloodDonation = await _context.BloodDonations.FindAsync(id);
            if (bloodDonation == null)
            {
                return NotFound();
            }

            _context.BloodDonations.Remove(bloodDonation);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BloodDonationExists(Guid id)
        {
            return (_context.BloodDonations?.Any(e => e.DonationId == id)).GetValueOrDefault();
        }
    }
}
