using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JeevanRakt.Core.Domain.Entities;
using JeevanRakt.Infrastructure.DataBase;
using Microsoft.AspNetCore.Authorization;
using JeevanRakt.Core.Services;
using JeevanRakt.Core.Domain.RepositoryContracts;
using System.Drawing;
using JeevanRakt.Core.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Org.BouncyCastle.Cms;

namespace JeevanRakt.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DonorsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly DataGeneraterService _dataGeneraterService;
        private readonly IDonorRepository _donorRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public DonorsController(ApplicationDbContext context, DataGeneraterService dataGeneraterService, IDonorRepository donorRepository, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _dataGeneraterService = dataGeneraterService;
            _donorRepository = donorRepository;
            _userManager = userManager;
        }

        // GET: api/Donors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Donor>>> GetDonors(int page = 1, int pageSize = 10, string? filterOn = null, string? filterQuery = null, string? sortBy = null, bool isAccending = true)
        {
          IEnumerable<Donor> donors = await _donorRepository.GetDonorsAsync(page, pageSize, filterOn, filterQuery, sortBy, isAccending);

            return Ok(donors);
        }

        // GET: api/Donors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Donor>> GetDonor(Guid id)
        {
          Donor donor = await _donorRepository.GetDonorAsync(id);

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

            bool result = await _donorRepository.UpdateDonorAsync(donor);

            if(result)
            {
                return NoContent();
            }

            else
            {
                return BadRequest("update fail");
            }
           
        }

        // POST: api/Donors
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Donor>> PostDonor(Donor donor)
        {
            ApplicationUser? user = await _userManager.GetUserAsync(User);
            if (user == null) { BadRequest("user not found"); }

            donor.UserId = user.Id;

            Donor donor1 = await _donorRepository.AddDonorAsync(donor);

            if(donor1 == null)
            {
                BadRequest("data is not added");
            }

            return Ok(donor1);
        }

        // DELETE: api/Donors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDonor(Guid id)
        {
            bool result = await _donorRepository.DeleteDonorAsync(id);

            if (result)
            {
                return NoContent();
              
            }
            else
            {
                return BadRequest("delete fail");
            }
          
        }

        [HttpGet("totaldonors")]
        [AllowAnonymous]
        public async Task<ActionResult<int>> GetTotalDonorsCount()
        {
            int totalDonorsCount = await _donorRepository.GetTotalDonorsCountAsync();
            return Ok(totalDonorsCount);
        }

        // GET: api/Donors/bloodbank
        [HttpGet("bloodbank")]
        public async Task<ActionResult<IEnumerable<Donor>>> GetDonorsById([FromQuery] Guid bloodbankId, int page = 1, int pageSize = 10, string? filterOn = null, string? filterQuery = null, string? sortBy = null, bool isAccending = true)
       {
            IEnumerable<Donor> donors = await _donorRepository.GetDonorsByBloodBankIdAsync(bloodbankId, page, pageSize, filterOn, filterQuery, sortBy, isAccending);

            if(donors == null)
            {
                return BadRequest("get request fail");
            }

            return Ok(donors);

        }

        [HttpGet("generate")]
        public async Task<IActionResult> GenerateData()
        {
            await _donorRepository.GenerateTestDataAsync();

            List<Donor> donors = await _context.Donors.ToListAsync();

            return Ok(donors);
        }

        [HttpGet("bloodbank/totaldonor")]
        [AllowAnonymous]
        public async Task<ActionResult<int>> GetTotalDonorsById([FromQuery] Guid bloodbankId)
        {
            int totaldonors = await _donorRepository.GetTotalDonor(bloodbankId);

            return Ok(totaldonors);

        }

    }
}
