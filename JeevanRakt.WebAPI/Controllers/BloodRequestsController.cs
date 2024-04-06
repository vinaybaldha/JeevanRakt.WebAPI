using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JeevanRakt.Core.Domain.Entities;
using JeevanRakt.Infrastructure.DataBase;
using JeevanRakt.Core.DTO.ResponseDTO;
using AutoMapper;

namespace JeevanRakt.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BloodRequestsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public BloodRequestsController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/BloodRequests
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BloodRequest>>> GetBloodRequests()
        {
          if (_context.BloodRequests == null)
          {
              return NotFound();
          }
            List<BloodRequest> bloodRequestList = await _context.BloodRequests.Include(x => x.Recipient).ToListAsync();

            List<BloodRequestResponseDTO> bloodRequestResponseDTOs = _mapper.Map<List<BloodRequestResponseDTO>>(bloodRequestList);

            return Ok(bloodRequestResponseDTOs);
        }

        // GET: api/BloodRequests/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BloodRequest>> GetBloodRequest(Guid id)
        {
          if (_context.BloodRequests == null)
          {
              return NotFound();
          }
            var bloodRequest = await _context.BloodRequests.FindAsync(id);

            if (bloodRequest == null)
            {
                return NotFound();
            }

            return bloodRequest;
        }

        // PUT: api/BloodRequests/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBloodRequest(Guid id, BloodRequest bloodRequest)
        {
            if (id != bloodRequest.RequestId)
            {
                return BadRequest();
            }

            _context.Entry(bloodRequest).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BloodRequestExists(id))
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

        // POST: api/BloodRequests
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<BloodRequest>> PostBloodRequest(BloodRequest bloodRequest)
        {
          if (_context.BloodRequests == null)
          {
              return Problem("Entity set 'ApplicationDbContext.BloodRequests'  is null.");
          }
            _context.BloodRequests.Add(bloodRequest);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBloodRequest", new { id = bloodRequest.RequestId }, bloodRequest);
        }

        // DELETE: api/BloodRequests/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBloodRequest(Guid id)
        {
            if (_context.BloodRequests == null)
            {
                return NotFound();
            }
            var bloodRequest = await _context.BloodRequests.FindAsync(id);
            if (bloodRequest == null)
            {
                return NotFound();
            }

            _context.BloodRequests.Remove(bloodRequest);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BloodRequestExists(Guid id)
        {
            return (_context.BloodRequests?.Any(e => e.RequestId == id)).GetValueOrDefault();
        }
    }
}
