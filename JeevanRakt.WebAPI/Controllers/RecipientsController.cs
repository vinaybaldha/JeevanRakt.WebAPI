using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JeevanRakt.Core.Domain.Entities;
using JeevanRakt.Infrastructure.DataBase;
using Microsoft.AspNetCore.Authorization;
using System.Drawing.Printing;
using System.Globalization;
using JeevanRakt.Core.Services;

namespace JeevanRakt.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RecipientsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly DataGeneraterService _dataGeneraterService;

        public RecipientsController(ApplicationDbContext context, DataGeneraterService dataGeneraterService)
        {
            _context = context;
            _dataGeneraterService = dataGeneraterService;
        }

        // GET: api/Recipients
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Recipient>>> GetRecipients(int page = 1, int pageSize = 10, string? filterOn = null, string? filterQuery = null, string? sortBy = null, bool isAccending = true)
        {
          if (_context.Recipients == null)
          {
              return NotFound();
          }

            //filtering
            var Recipients = _context.Recipients.AsQueryable();

            if (string.IsNullOrWhiteSpace(filterOn) == false && string.IsNullOrWhiteSpace(filterQuery) == false)
            {
                if (filterOn.Equals("RecipientName", StringComparison.OrdinalIgnoreCase))
                {
                    Recipients = Recipients.Where(x => x.RecipientName.Contains(filterQuery));
                }

                else if (filterOn.Equals("RecipientAge", StringComparison.OrdinalIgnoreCase))
                {
                    Recipients = Recipients.Where(x => x.RecipientAge.Equals(filterQuery));
                }

                else if (filterOn.Equals("RecipientGender", StringComparison.OrdinalIgnoreCase))
                {
                    Recipients = Recipients.Where(x => x.RecipientGender.Contains(filterQuery));
                }

                else if (filterOn.Equals("RecipientAddress", StringComparison.OrdinalIgnoreCase))
                {
                    Recipients = Recipients.Where(x => x.RecipientAddress.Contains(filterQuery));
                }

                else if (filterOn.Equals("RecipientBloodType", StringComparison.OrdinalIgnoreCase))
                {
                    Recipients = Recipients.Where(x => x.RecipientBloodType.Contains(filterQuery));
                }

                else if (filterOn.Equals("RecipientContactNumber", StringComparison.OrdinalIgnoreCase))
                {
                    Recipients = Recipients.Where(x => x.RecipientContactNumber.Contains(filterQuery));
                }

            }

            //sorting
            if (string.IsNullOrWhiteSpace(sortBy) == false)
            {
                if (sortBy.Equals("RecipientName", StringComparison.OrdinalIgnoreCase))
                {
                    Recipients = isAccending ? Recipients.OrderBy(x => x.RecipientName) : Recipients.OrderByDescending(x => x.RecipientName);
                }

                if (sortBy.Equals("RecipientAge", StringComparison.OrdinalIgnoreCase))
                {
                    Recipients = isAccending ? Recipients.OrderBy(x => x.RecipientAge) : Recipients.OrderByDescending(x => x.RecipientAge);
                }

                if (sortBy.Equals("RecipientGender", StringComparison.OrdinalIgnoreCase))
                {
                    Recipients = isAccending ? Recipients.OrderBy(x => x.RecipientGender) : Recipients.OrderByDescending(x => x.RecipientGender);
                }

                if (sortBy.Equals("RecipientAddress", StringComparison.OrdinalIgnoreCase))
                {
                    Recipients = isAccending ? Recipients.OrderBy(x => x.RecipientAddress) : Recipients.OrderByDescending(x => x.RecipientAddress);
                }

                if (sortBy.Equals("RecipientBloodType", StringComparison.OrdinalIgnoreCase))
                {
                    Recipients = isAccending ? Recipients.OrderBy(x => x.RecipientBloodType) : Recipients.OrderByDescending(x => x.RecipientBloodType);
                }

                if (sortBy.Equals("RecipientContactNumber", StringComparison.OrdinalIgnoreCase))
                {
                    Recipients = isAccending ? Recipients.OrderBy(x => x.RecipientContactNumber) : Recipients.OrderByDescending(x => x.RecipientContactNumber);
                }

            }

            //pagination

            return await Recipients.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
        }

        // GET: api/Recipients/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Recipient>> GetRecipient(Guid id)
        {
          if (_context.Recipients == null)
          {
              return NotFound();
          }
            var recipient = await _context.Recipients.FindAsync(id);

            if (recipient == null)
            {
                return NotFound();
            }

            return recipient;
        }

        // PUT: api/Recipients/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRecipient(Guid id, Recipient recipient)
        {
            if (id != recipient.RecipientId)
            {
                return BadRequest();
            }

            _context.Entry(recipient).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RecipientExists(id))
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

        // POST: api/Recipients
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Recipient>> PostRecipient(Recipient recipient)
        {
          if (_context.Recipients == null)
          {
              return Problem("Entity set 'ApplicationDbContext.Recipients'  is null.");
          }
            _context.Recipients.Add(recipient);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRecipient", new { id = recipient.RecipientId }, recipient);
        }

        // DELETE: api/Recipients/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRecipient(Guid id)
        {
            if (_context.Recipients == null)
            {
                return NotFound();
            }
            var recipient = await _context.Recipients.FindAsync(id);
            if (recipient == null)
            {
                return NotFound();
            }

            _context.Recipients.Remove(recipient);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RecipientExists(Guid id)
        {
            return (_context.Recipients?.Any(e => e.RecipientId == id)).GetValueOrDefault();
        }

        // GET: api/Donors/bloodbank
        [HttpGet("bloodbank")]
        public async Task<ActionResult<IEnumerable<Recipient>>> GetRecipientsById([FromQuery] Guid bloodbankId, int page = 1, int pageSize = 10, string? filterOn = null, string? filterQuery = null, string? sortBy = null, bool isAccending = true)
        {
            if (_context.Recipients == null)
            {
                return NotFound();
            }

            //filtering
            var Recipients = _context.Recipients.Where(x => x.BloodBankId == bloodbankId).AsQueryable();

            if (string.IsNullOrWhiteSpace(filterOn) == false && string.IsNullOrWhiteSpace(filterQuery) == false)
            {
                if (filterOn.Equals("RecipientName", StringComparison.OrdinalIgnoreCase))
                {
                    Recipients = Recipients.Where(x => x.RecipientName.Contains(filterQuery));
                }

                else if (filterOn.Equals("RecipientAge", StringComparison.OrdinalIgnoreCase))
                {
                    Recipients = Recipients.Where(x => x.RecipientAge.Equals(filterQuery));
                }

                else if (filterOn.Equals("RecipientGender", StringComparison.OrdinalIgnoreCase))
                {
                    Recipients = Recipients.Where(x => x.RecipientGender.Contains(filterQuery));
                }

                else if (filterOn.Equals("RecipientAddress", StringComparison.OrdinalIgnoreCase))
                {
                    Recipients = Recipients.Where(x => x.RecipientAddress.Contains(filterQuery));
                }

                else if (filterOn.Equals("RecipientBloodType", StringComparison.OrdinalIgnoreCase))
                {
                    Recipients = Recipients.Where(x => x.RecipientBloodType.Contains(filterQuery));
                }

                else if (filterOn.Equals("RecipientContactNumber", StringComparison.OrdinalIgnoreCase))
                {
                    Recipients = Recipients.Where(x => x.RecipientContactNumber.Contains(filterQuery));
                }

            }

            //sorting
            if (string.IsNullOrWhiteSpace(sortBy) == false)
            {
                if (sortBy.Equals("RecipientName", StringComparison.OrdinalIgnoreCase))
                {
                    Recipients = isAccending ? Recipients.OrderBy(x => x.RecipientName) : Recipients.OrderByDescending(x => x.RecipientName);
                }

                if (sortBy.Equals("RecipientAge", StringComparison.OrdinalIgnoreCase))
                {
                    Recipients = isAccending ? Recipients.OrderBy(x => x.RecipientAge) : Recipients.OrderByDescending(x => x.RecipientAge);
                }

                if (sortBy.Equals("RecipientGender", StringComparison.OrdinalIgnoreCase))
                {
                    Recipients = isAccending ? Recipients.OrderBy(x => x.RecipientGender) : Recipients.OrderByDescending(x => x.RecipientGender);
                }

                if (sortBy.Equals("RecipientAddress", StringComparison.OrdinalIgnoreCase))
                {
                    Recipients = isAccending ? Recipients.OrderBy(x => x.RecipientAddress) : Recipients.OrderByDescending(x => x.RecipientAddress);
                }

                if (sortBy.Equals("RecipientBloodType", StringComparison.OrdinalIgnoreCase))
                {
                    Recipients = isAccending ? Recipients.OrderBy(x => x.RecipientBloodType) : Recipients.OrderByDescending(x => x.RecipientBloodType);
                }

                if (sortBy.Equals("RecipientContactNumber", StringComparison.OrdinalIgnoreCase))
                {
                    Recipients = isAccending ? Recipients.OrderBy(x => x.RecipientContactNumber) : Recipients.OrderByDescending(x => x.RecipientContactNumber);
                }
            }

            //pagination

            return await Recipients.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

        }

        [HttpGet("generate")]
        [AllowAnonymous]
        public async Task<IActionResult> GenerateData()
        {
            List<BloodBank> bloodBanks = _context.BloodBanks.ToList();

            foreach (var bloodBank in bloodBanks)
            {
                for (int i = 0; i < 100; i++)
                {
                    Recipient recipient = _dataGeneraterService.GenerateRecipient();

                    recipient.BloodBankId = bloodBank.BloodBankId;
                    _context.Recipients.Add(recipient);
                }
            }
            await _context.SaveChangesAsync();

            List<Recipient> recipients = await _context.Recipients.ToListAsync();

            return Ok(recipients);
        }
    }
}
