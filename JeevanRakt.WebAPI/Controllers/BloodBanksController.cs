using JeevanRakt.Core.Domain.Entities;
using JeevanRakt.Core.Domain.RepositoryContracts;
using JeevanRakt.Core.DTO;
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

        public BloodBanksController(IBloodBankService bloodBankService)
        {
            _bloodBankService = bloodBankService;
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

            bloodbankResponse bloodBanks = await _bloodBankService.GetBloodBanksAsync(page,pageSize,filterOn,filterQuery,sortBy,isAccending);
           
            return Ok(bloodBanks);
        }

        // GET: api/BloodBanks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BloodBank>> GetBloodBank(Guid id)
        {
           BloodBank bloodBank = await _bloodBankService.GetBloodBankAsync(id);

            return Ok(bloodBank);
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

            bool result = await _bloodBankService.UpdateBloodBankAsync(bloodBank);

            if (result)
            {
                return NoContent();
            }

            else
            {
                return BadRequest();
            }

            
        }

        // POST: api/BloodBanks
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<BloodBank>> PostBloodBank(BloodBank bloodBank)
        {
            BloodBank bloodbank = await _bloodBankService.AddBloodBankAsync(bloodBank);

            return Ok(bloodbank);
        }

        // DELETE: api/BloodBanks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBloodBank(Guid id)
        {
            bool result = await _bloodBankService.DeleteBloodBankAsync(id);

            if (result)
            {
                return NoContent();
            }

            else
            {
                return BadRequest("error occured");
            }

            
        }


        [HttpGet("totalbloodbanks")]
        [AllowAnonymous]

        public async Task<IActionResult> TotalBloodBanks()
        {
            if(_bloodBankService == null) { return BadRequest(); }

            int count = await _bloodBankService.GetTotalBloodBankAsync();

            return Ok(count);
        }


        [HttpGet("approve")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> ApproveReq(Guid bloodBankId)
        {
            if (_bloodBankService == null) { return BadRequest(); }

           BloodBank bloodBank = await _bloodBankService.GetBloodBankAsync(bloodBankId);

            if(bloodBank == null)
            {
                return BadRequest("bloodbank not found");
            }

            bool result = await _bloodBankService.ApproveRequest(bloodBank);

            if(result == false) { return BadRequest("bloodbank approve fail"); }

            return Ok(bloodBank);
        }

        [HttpGet("getpendingbloodbanks")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetPendingBloodBanks()
        {
            if (_bloodBankService == null) { return BadRequest(); }

            List<BloodBank> bloodBanks = await _bloodBankService.GetPendingBloodBank();


            return Ok(bloodBanks);
        }

    }
}
