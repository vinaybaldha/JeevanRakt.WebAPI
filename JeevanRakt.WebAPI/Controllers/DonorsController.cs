using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JeevanRakt.Core.Domain.Entities;
using JeevanRakt.Infrastructure.DataBase;
using Microsoft.AspNetCore.Authorization;
using System.Drawing.Printing;
using System.Globalization;

namespace JeevanRakt.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DonorsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public DonorsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Donors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Donor>>> GetDonors(int page = 1, int pageSize = 10, string? filterOn = null, string? filterQuery = null, string? sortBy = null, bool isAccending = true)
        {
          if (_context.Donors == null)
          {
              return NotFound();
          }

            //filtering
            var Donors = _context.Donors.AsQueryable();

            if (string.IsNullOrWhiteSpace(filterOn) == false && string.IsNullOrWhiteSpace(filterQuery) == false)
            {
                if (filterOn.Equals("DonorName", StringComparison.OrdinalIgnoreCase))
                {
                    Donors = Donors.Where(x => x.DonorName.Contains(filterQuery));
                }

                else if (filterOn.Equals("DonorAge", StringComparison.OrdinalIgnoreCase))
                {
                    Donors = Donors.Where(x => x.DonorAge.Equals(filterQuery));
                }

                else if (filterOn.Equals("DonorGender", StringComparison.OrdinalIgnoreCase))
                {
                    Donors = Donors.Where(x => x.DonorGender.Contains(filterQuery));
                }

                else if (filterOn.Equals("DonorAddress", StringComparison.OrdinalIgnoreCase))
                {
                    Donors = Donors.Where(x => x.DonorAddress.Contains(filterQuery));
                }

                else if (filterOn.Equals("DonorBloodType", StringComparison.OrdinalIgnoreCase))
                {
                    Donors = Donors.Where(x => x.DonorBloodType.Contains(filterQuery));
                }

                else if (filterOn.Equals("DonorContactNumber", StringComparison.OrdinalIgnoreCase))
                {
                    Donors = Donors.Where(x => x.DonorContactNumber.Contains(filterQuery));
                }

            }

            //sorting
            if (string.IsNullOrWhiteSpace(sortBy) == false)
            {
                if (sortBy.Equals("DonorName", StringComparison.OrdinalIgnoreCase))
                {
                    Donors = isAccending ? Donors.OrderBy(x => x.DonorName) : Donors.OrderByDescending(x => x.DonorName);
                }

                if (sortBy.Equals("DonorAge", StringComparison.OrdinalIgnoreCase))
                {
                    Donors = isAccending ? Donors.OrderBy(x => x.DonorAge) : Donors.OrderByDescending(x => x.DonorAge);
                }

                if (sortBy.Equals("DonorGender", StringComparison.OrdinalIgnoreCase))
                {
                    Donors = isAccending ? Donors.OrderBy(x => x.DonorGender) : Donors.OrderByDescending(x => x.DonorGender);
                }

                if (sortBy.Equals("DonorAddress", StringComparison.OrdinalIgnoreCase))
                {
                    Donors = isAccending ? Donors.OrderBy(x => x.DonorAddress) : Donors.OrderByDescending(x => x.DonorAddress);
                }

                if (sortBy.Equals("DonorBloodType", StringComparison.OrdinalIgnoreCase))
                {
                    Donors = isAccending ? Donors.OrderBy(x => x.DonorBloodType) : Donors.OrderByDescending(x => x.DonorBloodType);
                }

                if (sortBy.Equals("DonorContactNumber", StringComparison.OrdinalIgnoreCase))
                {
                    Donors = isAccending ? Donors.OrderBy(x => x.DonorContactNumber) : Donors.OrderByDescending(x => x.DonorContactNumber);
                }

            }

            //pagination

            return await Donors.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
        }

        // GET: api/Donors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Donor>> GetDonor(Guid id)
        {
          if (_context.Donors == null)
          {
              return NotFound();
          }
            var donor = await _context.Donors.FindAsync(id);

            if (donor == null)
            {
                return NotFound();
            }

            return donor;
        }

        // PUT: api/Donors/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDonor(Guid id, Donor donor)
        {
            if (id != donor.DonorId)
            {
                return BadRequest();
            }

            _context.Entry(donor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DonorExists(id))
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

        // POST: api/Donors
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Donor>> PostDonor(Donor donor)
        {
          if (_context.Donors == null)
          {
              return Problem("Entity set 'ApplicationDbContext.Donors'  is null.");
          }
            _context.Donors.Add(donor);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDonor", new { id = donor.DonorId }, donor);
        }

        // DELETE: api/Donors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDonor(Guid id)
        {
            if (_context.Donors == null)
            {
                return NotFound();
            }
            var donor = await _context.Donors.FindAsync(id);
            if (donor == null)
            {
                return NotFound();
            }

            _context.Donors.Remove(donor);
            await _context.SaveChangesAsync();

            return Ok(donor);
        }

        [HttpGet("totaldonors")]
        public async Task<ActionResult<int>> GetTotalDonorsCount()
        {
            var totalDonorsCount = await _context.Donors.CountAsync();
            return Ok(totalDonorsCount);
        }

        private bool DonorExists(Guid id)
        {
            return (_context.Donors?.Any(e => e.DonorId == id)).GetValueOrDefault();
        }

        // GET: api/Donors/bloodbank
        [HttpGet("bloodbank")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<Donor>>> GetDonorsById([FromQuery] Guid bloodbankId, int page = 1, int pageSize = 10, string? filterOn = null, string? filterQuery = null, string? sortBy = null, bool isAccending = true)
        {
            if (_context.Donors == null)
            {
                return NotFound();
            }

            //filtering
            var Donors = _context.Donors.Where(x => x.BloodBankId == bloodbankId).AsQueryable();

            if (string.IsNullOrWhiteSpace(filterOn) == false && string.IsNullOrWhiteSpace(filterQuery) == false)
            {
                if (filterOn.Equals("DonorName", StringComparison.OrdinalIgnoreCase))
                {
                    Donors = Donors.Where(x => x.DonorName.Contains(filterQuery));
                }

                else if (filterOn.Equals("DonorAge", StringComparison.OrdinalIgnoreCase))
                {
                    Donors = Donors.Where(x => x.DonorAge.Equals(filterQuery));
                }

                else if (filterOn.Equals("DonorGender", StringComparison.OrdinalIgnoreCase))
                {
                    Donors = Donors.Where(x => x.DonorGender.Contains(filterQuery));
                }

                else if (filterOn.Equals("DonorAddress", StringComparison.OrdinalIgnoreCase))
                {
                    Donors = Donors.Where(x => x.DonorAddress.Contains(filterQuery));
                }

                else if (filterOn.Equals("DonorBloodType", StringComparison.OrdinalIgnoreCase))
                {
                    Donors = Donors.Where(x => x.DonorBloodType.Contains(filterQuery));
                }

                else if (filterOn.Equals("DonorContactNumber", StringComparison.OrdinalIgnoreCase))
                {
                    Donors = Donors.Where(x => x.DonorContactNumber.Contains(filterQuery));
                }

            }

            //sorting
            if (string.IsNullOrWhiteSpace(sortBy) == false)
            {
                if (sortBy.Equals("DonorName", StringComparison.OrdinalIgnoreCase))
                {
                    Donors = isAccending ? Donors.OrderBy(x => x.DonorName) : Donors.OrderByDescending(x => x.DonorName);
                }

                if (sortBy.Equals("DonorAge", StringComparison.OrdinalIgnoreCase))
                {
                    Donors = isAccending ? Donors.OrderBy(x => x.DonorAge) : Donors.OrderByDescending(x => x.DonorAge);
                }

                if (sortBy.Equals("DonorGender", StringComparison.OrdinalIgnoreCase))
                {
                    Donors = isAccending ? Donors.OrderBy(x => x.DonorGender) : Donors.OrderByDescending(x => x.DonorGender);
                }

                if (sortBy.Equals("DonorAddress", StringComparison.OrdinalIgnoreCase))
                {
                    Donors = isAccending ? Donors.OrderBy(x => x.DonorAddress) : Donors.OrderByDescending(x => x.DonorAddress);
                }

                if (sortBy.Equals("DonorBloodType", StringComparison.OrdinalIgnoreCase))
                {
                    Donors = isAccending ? Donors.OrderBy(x => x.DonorBloodType) : Donors.OrderByDescending(x => x.DonorBloodType);
                }

                if (sortBy.Equals("DonorContactNumber", StringComparison.OrdinalIgnoreCase))
                {
                    Donors = isAccending ? Donors.OrderBy(x => x.DonorContactNumber) : Donors.OrderByDescending(x => x.DonorContactNumber);
                }
            }

            //pagination

            return await Donors.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

        }


    }
}
