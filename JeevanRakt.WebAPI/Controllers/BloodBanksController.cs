using JeevanRakt.Core.Domain.Entities;
using JeevanRakt.Core.Domain.RepositoryContracts;
using JeevanRakt.Infrastructure.DataBase;
using JeevanRakt.Infrastructure.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        [AllowAnonymous]
        public IActionResult GetNearestBloodBank(double userLatitude, double userLongitude)
        {
            var nearestBloodBank = _bloodBankService.FindNearestBloodBank(userLatitude, userLongitude);

            if (nearestBloodBank == null)
            {
                return NotFound();
            }

            return Ok(nearestBloodBank);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BloodBank>>> GetBloodBanks()
        {
            if (_context.BloodBanks == null)
            {
                return NotFound();
            }
            return await _context.BloodBanks.ToListAsync();
        }
    }
}
