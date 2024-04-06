using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JeevanRakt.Core.Domain.Entities;
using JeevanRakt.Infrastructure.DataBase;
using AutoMapper;
using JeevanRakt.Core.DTO.ResponseDTO;

namespace JeevanRakt.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BloodInventoriesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public BloodInventoriesController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/BloodInventories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BloodInventory>>> GetBloodInventories()
        {
          if (_context.BloodInventories == null)
          {
              return NotFound();
          }
            List<BloodInventory> inventoryList = await _context.BloodInventories.Include(x => x.Donor).ToListAsync();

            var bloodInventoryResponseDTOs = _mapper.Map<IEnumerable<BloodInventoryResponseDTO>>(inventoryList);

            return Ok(bloodInventoryResponseDTOs);
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
            if (id != bloodInventory.InventoryId)
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

            return CreatedAtAction("GetBloodInventory", new { id = bloodInventory.InventoryId }, bloodInventory);
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

        private bool BloodInventoryExists(Guid id)
        {
            return (_context.BloodInventories?.Any(e => e.InventoryId == id)).GetValueOrDefault();
        }
    }
}
