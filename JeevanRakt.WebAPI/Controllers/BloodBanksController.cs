using JeevanRakt.Core.Domain.Entities;
using JeevanRakt.Core.Domain.RepositoryContracts;
using JeevanRakt.Infrastructure.DataBase;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace JeevanRakt.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BloodBanksController : ControllerBase
    {
        private readonly IBloodBankService _bloodBankService;
        private readonly ApplicationDbContext _context;

        public BloodBanksController(IBloodBankService bloodBankService, ApplicationDbContext context)
        {
            _bloodBankService = bloodBankService;
            _context = context;
        }

        [HttpGet("nearest")]
        
        public IActionResult GetNearestBloodBank(double userLatitude, double userLongitude)
        {
            var nearestBloodBank = _bloodBankService.FindNearestBloodBank(userLatitude, userLongitude);

            if (nearestBloodBank == null)
            {
                return NotFound();
            }

            return Ok(nearestBloodBank);
        }

        // GET: api/BloodBanks
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<BloodBank>>> GetBloodBanks(int page =1, int pageSize =10, string? filterOn=null, string? filterQuery=null, string? sortBy = null, bool isAccending = true)
        {
            if (_context.BloodBanks == null)
            {
                return NotFound();
            }

            //filtering
            var BloodBanks = _context.BloodBanks.AsQueryable();

            if(string.IsNullOrWhiteSpace(filterOn) == false && string.IsNullOrWhiteSpace(filterQuery) == false)
            {
                if (filterOn.Equals("BloodBankName", StringComparison.OrdinalIgnoreCase))
                {
                    BloodBanks = BloodBanks.Where(x=>x.BloodBankName.Contains(filterQuery));
                }

                else if (filterOn.Equals("Address", StringComparison.OrdinalIgnoreCase))
                {
                    BloodBanks = BloodBanks.Where(x => x.Address.Contains(filterQuery));
                }

            }

            //sorting
            if(string.IsNullOrWhiteSpace(sortBy) == false)
            {
                if (sortBy.Equals("BloodBankName", StringComparison.OrdinalIgnoreCase))
                {
                    BloodBanks = isAccending ? BloodBanks.OrderBy(x=>x.BloodBankName) : BloodBanks.OrderByDescending(x => x.BloodBankName);
                }

                if (sortBy.Equals("Address", StringComparison.OrdinalIgnoreCase))
                {
                    BloodBanks = isAccending ? BloodBanks.OrderBy(x => x.Address) : BloodBanks.OrderByDescending(x => x.Address);
                }

            }

            //pagination

            return await BloodBanks.Skip((page-1)*pageSize).Take(pageSize).ToListAsync();
        }

        // GET: api/BloodBanks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BloodBank>> GetBloodBank(Guid id)
        {
            if (_context.BloodBanks == null)
            {
                return NotFound();
            }
            var bloodBank = await _context.BloodBanks.Include(x=>x.Donors).Include(x => x.Recipients).Include(x=>x.BloodInventory).FirstOrDefaultAsync(x=>x.BloodBankId==id);

            if (bloodBank == null)
            {
                return NotFound();
            }

            return bloodBank;
        }

        // PUT: api/BloodBanks/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBloodBank(Guid id, BloodBank bloodBank)
        {
            if (id != bloodBank.BloodBankId)
            {
                return BadRequest();
            }

            _context.Entry(bloodBank).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BloodBankExists(id))
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

        // POST: api/BloodBanks
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<BloodBank>> PostBloodBank(BloodBank bloodBank)
        {
            if (_context.BloodBanks == null)
            {
                return Problem("Entity set 'ApplicationDbContext.BloodBanks'  is null.");
            }
            _context.BloodBanks.Add(bloodBank);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBloodBank", new { id = bloodBank.BloodBankId }, bloodBank);
        }

        // DELETE: api/BloodBanks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBloodBank(Guid id)
        {
            if (_context.BloodBanks == null)
            {
                return NotFound();
            }
            var bloodBank = await _context.BloodBanks.FindAsync(id);
            if (bloodBank == null)
            {
                return NotFound();
            }

            _context.BloodBanks.Remove(bloodBank);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BloodBankExists(Guid id)
        {
            return (_context.BloodBanks?.Any(e => e.BloodBankId == id)).GetValueOrDefault();
        }
    }
}
